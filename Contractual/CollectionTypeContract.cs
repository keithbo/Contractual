using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Contractual
{
	internal class CollectionTypeContract : TypeContract
	{
		private static Type[] _knownInterfaces;
		private static Type[] KnownInterfaces
		{
			get
			{
				if (_knownInterfaces == null)
				{
					_knownInterfaces = new Type[]
					{
						WellKnownTypes.TypeOfIDictionaryGeneric,
						WellKnownTypes.TypeOfIDictionary,
						WellKnownTypes.TypeOfIListGeneric,
						WellKnownTypes.TypeOfICollectionGeneric,
						WellKnownTypes.TypeOfIList,
						WellKnownTypes.TypeOfIEnumerableGeneric,
						WellKnownTypes.TypeOfICollection,
						WellKnownTypes.TypeOfIEnumerable
					};
				}
				return _knownInterfaces;
			}
		}

		internal static bool IsCollectionInterface(Type type)
		{
			if (type.IsGenericType)
			{
				type = type.GetGenericTypeDefinition();
			}

			return ((IList<Type>)KnownInterfaces).Contains(type);
		}

		internal static TypeContract Create(Type type)
		{
			// no point in being here otherwise
			if (WellKnownTypes.TypeOfIEnumerable.IsAssignableFrom(type))
			{
				return CreateInterfaceCollectionContract(type) ??
					CreateImplementationCollectionContract(type);
			}

			return null;
		}


		private static TypeContract CreateInterfaceCollectionContract(Type type)
		{
			// accessible type is an interface
			if (type.IsInterface)
			{
				Type interfaceType;
				CollectionKind interfaceKind;
				Type itemType;
				MethodInfo addMethod;
				MethodInfo enumeratorMethod;
				if (TryFindInterfaceTypeInfo(type, out interfaceType, out interfaceKind, out itemType, out addMethod, out enumeratorMethod))
				{
					return new CollectionTypeContract(type, interfaceKind, itemType, null, addMethod, enumeratorMethod);
				}
			}

			return null;
		}

		internal static bool TryFindInterfaceTypeInfo(Type type, out Type interfaceType, out CollectionKind interfaceKind, out Type itemType, out MethodInfo addMethod, out MethodInfo enumeratorMethod)
		{
			if (TryFindInterfaceTypeInfo(type, out interfaceType, out interfaceKind))
			{
				return TryGetMethodInfo(type, interfaceType, interfaceKind, out itemType, out addMethod, out enumeratorMethod);
			}

			itemType = null;
			addMethod = null;
			enumeratorMethod = null;
			return false;
		}

		internal static bool TryFindInterfaceTypeInfo(Type type, out Type interfaceType, out CollectionKind interfaceKind)
		{
			Type derivedType = type.IsGenericType ? type.GetGenericTypeDefinition() : type;
			Type[] knownInterfaces = CollectionTypeContract.KnownInterfaces;
			int kindOffset = 0;
			interfaceType = knownInterfaces.FirstOrDefault(t =>
			{
				kindOffset++;
				return t == derivedType;
			});
			interfaceKind = (CollectionKind)(interfaceType == null ? 0 : kindOffset);
			return interfaceType != null;
		}

		private static TypeContract CreateImplementationCollectionContract(Type type)
		{
			// accessible type is an implementation
			Type bestCommonType = null;
			CollectionKind bestCommonKind = CollectionKind.None;
			bool duplicateImpl = false;
			Type[] interfaces = type.GetInterfaces();
			Type[] array = interfaces;
			foreach (var searchInterfaceType in array)
			{
				Type interfaceType;
				CollectionKind interfaceKind;
				if (TryFindInterfaceTypeInfo(searchInterfaceType, out interfaceType, out interfaceKind))
				{
					if (bestCommonKind == CollectionKind.None || interfaceKind < bestCommonKind)
					{
						bestCommonKind = interfaceKind;
						bestCommonType = interfaceType;
						duplicateImpl = false;
					}
					if ((bestCommonKind & interfaceKind) == interfaceKind)
					{
						duplicateImpl = true;
					}
				}
			}

			if (bestCommonKind == CollectionKind.None)
			{
				throw new ArgumentException();
			}
			ConstructorInfo constructor = null;
			if (!type.IsValueType)
			{
				constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[0], null);
				if (constructor == null)
				{
					throw new ArgumentException();
				}
			}
			Type itemType;
			MethodInfo addMethod;
			MethodInfo enumeratorMethod;
			if (!TryGetMethodInfo(type, bestCommonType, bestCommonKind, out itemType, out addMethod, out enumeratorMethod))
			{
				throw new ArgumentException();
			}

			if (bestCommonKind == CollectionKind.Enumerable || bestCommonKind == CollectionKind.Collection || bestCommonKind == CollectionKind.GenericEnumerable)
			{
				throw new NotImplementedException();
			}

			return new CollectionTypeContract(type, bestCommonKind, itemType, constructor, addMethod, enumeratorMethod);
		}

		internal static bool TryGetMethodInfo(Type type, Type interfaceType, CollectionKind kind, out Type itemType, out MethodInfo addMethod, out MethodInfo enumerateMethod)
		{
			itemType = null;
			addMethod = null;
			enumerateMethod = null;
			if (type.IsGenericType)
			{
				Type[] genericArguments = type.GetGenericArguments();
				switch (kind)
				{
					case CollectionKind.GenericDictionary:
						itemType = WellKnownTypes.TypeOfKeyValuePair.MakeGenericType(genericArguments);
						addMethod = type.GetMethod("Add");
						break;
					case CollectionKind.GenericList:
					case CollectionKind.GenericCollection:
						itemType = genericArguments[0];
						addMethod = WellKnownTypes.TypeOfICollectionGeneric.MakeGenericType(new Type[]
								{
									itemType
								}).GetMethod("Add");
						break;
					case CollectionKind.GenericEnumerable:
						itemType = genericArguments[0];
						break;
					default:
						return false;
				}

				enumerateMethod = WellKnownTypes.TypeOfIEnumerableGeneric.MakeGenericType(new Type[]
								{
									itemType
								}).GetMethod("GetEnumerator");
			}
			else
			{
				switch (kind)
				{
					case CollectionKind.Dictionary:
						itemType = typeof(KeyValuePair<object, object>);
						addMethod = type.GetMethod("Add");
						break;
					case CollectionKind.List:
						itemType = WellKnownTypes.TypeOfObject;
						addMethod = WellKnownTypes.TypeOfIList.GetMethod("Add");
						break;
					case CollectionKind.Collection:
					case CollectionKind.Array:
					case CollectionKind.Enumerable:
						itemType = WellKnownTypes.TypeOfObject;
						break;
					default:
						return false;
				}

				enumerateMethod = WellKnownTypes.TypeOfIEnumerable.GetMethod("GetEnumerator");
			}

			return true;
		}

		protected CollectionTypeContract(Type type, CollectionKind kind, Type itemType, ConstructorInfo constructor, MethodInfo addMethod, MethodInfo enumeratorMethod)
			: base(type)
		{
			_kind = kind;
			_itemType = itemType;
			_constructor = constructor;
			_addMethod = addMethod;
		}

		private CollectionKind _kind;
		private Type _itemType;
		private ConstructorInfo _constructor;
		private MethodInfo _addMethod;
		private MethodInfo enumeratorMethod;

		internal override System.Linq.Expressions.Expression Init(System.Linq.Expressions.ParameterExpression sourceParam, TypeContract source)
		{
			var sourceCollectionType = source as CollectionTypeContract;
			if (sourceCollectionType == null)
			{
				throw new ArgumentException();
			}

			var helperAccess = (ICollectionHelperAccess)Activator.CreateInstance(typeof(CollectionHelperAccess<,>).MakeGenericType(sourceCollectionType._itemType, _itemType));

			var lambda = helperAccess.ConvertEnumerable(_constructor, _addMethod);

			return Expression.Invoke(lambda, sourceParam);
		}

		//private IEnumerable<TResult> Make<TSource, TResult>(IEnumerable<TSource> source, ConstructorInfo constructor, MethodInfo method)
		//{
		//	var contract = TypePairingContract.GetContract(typeof(TSource), typeof(TResult));

		//	var resultNewExp = Expression.New(constructor); // IEnumerable<TResult>
		//	var resultVarEx = Expression.Variable(constructor.DeclaringType, "result");
		//	var resultAssignEx = Expression.Assign(resultVarEx, resultNewExp);
		//	var convertEx = contract.Convert();// TSource -> TResult

		//	var sourceItemEx = Expression.Variable(typeof(TSource), "item");

		//	var enumeratorEx = Expression.Variable(typeof(IEnumerator<TSource>), "enumerator");
		//	var sourceParamEx = Expression.Parameter(typeof(IEnumerable<TSource>), "source");
		//	var moveNextEx = Expression.Call(enumeratorEx, typeof(IEnumerable<TSource>).GetMethod("MoveNext"));
		//	var assignEnumeratorEx = Expression.Assign(enumeratorEx, Expression.Call(sourceParamEx, typeof(IEnumerable<TSource>).GetMethod("GetEnumerator")));
		//	//var assignCurrentEx = Expression.Assign(sourceItemEx, );
		//	var callAddExp = Expression.Call(resultVarEx, method, Expression.Property(enumeratorEx, "Current"));

		//	var breakTarget = Expression.Label();
		//	var blockEx = Expression.Block(
		//		resultAssignEx,
		//		assignEnumeratorEx,
		//		Expression.Loop(
		//			Expression.IfThenElse(
		//				Expression.NotEqual(moveNextEx, Expression.Constant(false)),
		//				callAddExp,
		//				Expression.Break(breakTarget)
		//			),
		//			breakTarget
		//		),
		//		Expression.Label(Expression.Label(constructor.DeclaringType), resultVarEx)
		//	);

		//	var resultParam = Expression.Parameter(constructor.DeclaringType, "result");
		//	var methodParams = method.GetParameters().Select(p => Expression.Parameter(p.ParameterType, p.Name)).ToArray();
		//	var callExp = Expression.Call(resultParam, method, methodParams);
		//	var callLambdaExp = Expression.Lambda(callExp, resultParam, methodParams[0]);


		//	var breakTarget = Expression.Label();
		//	var loop = Expression.Loop()

		//	var block = Expression.Block()

		//	return null;
		//}

		public interface ICollectionHelperAccess
		{
			LambdaExpression ConvertEnumerable(ConstructorInfo constructor, MethodInfo addMethod);
		}

		internal class CollectionHelperAccess<TSource, TResult> : ICollectionHelperAccess
		{
			public LambdaExpression ConvertEnumerable(ConstructorInfo constructor, MethodInfo addMethod)
			{
				return GenericCollectionHelper<TSource, TResult>.ConvertEnumerable(constructor, addMethod);
			}
		}

		internal static class GenericCollectionHelper<TSource, TResult>
		{
			private static readonly Type SourceType = typeof(TSource);
			//private readonly Type EnumeratorType = typeof(IEnumerator);
			private static readonly MethodInfo EnumeratorMoveNextMethod = typeof(IEnumerator).GetMethod("MoveNext");
			private static readonly Type SourceGenericEnumeratorType = typeof(IEnumerator<TSource>);
			private static readonly Type SourceGenericEnumerableType = typeof(IEnumerable<TSource>);
			private static readonly MethodInfo SourceGetEnumeratorMethod = typeof(IEnumerable<TSource>).GetMethod("GetEnumerator");
			private static readonly Type ResultType = typeof(TResult);
			private static readonly Type ResultGenericEnumerableType = typeof(IEnumerable<TResult>);

			private static ParameterExpression _sourceParameter;
			private static ParameterExpression _enumeratorVariable;

			private static ParameterExpression SourceParameter
			{
				get
				{
					if (_sourceParameter == null)
					{
						_sourceParameter = Expression.Parameter(SourceGenericEnumerableType, "source");
					}
					return _sourceParameter;
				}
			}

			private static ParameterExpression EnumeratorVariable
			{
				get
				{
					if (_enumeratorVariable == null)
					{
						_enumeratorVariable = Expression.Variable(SourceGenericEnumeratorType, "enumerator");
					}
					return _enumeratorVariable;
				}
			}

			public static Expression<Func<IEnumerable<TSource>, IEnumerable<TResult>>> ConvertEnumerable(ConstructorInfo constructor, MethodInfo addMethod)
			{
				var contract = TypePairingContract.GetContract(SourceType, ResultType);
				var convertEx = contract.Convert();// TSource -> TResult

				var resultNewExp = Expression.New(constructor); // IEnumerable<TResult>
				var resultVarEx = Expression.Variable(constructor.DeclaringType, "result");
				var resultAssignEx = Expression.Assign(resultVarEx, resultNewExp);

				var moveNextEx = Expression.Call(EnumeratorVariable, EnumeratorMoveNextMethod);
				var assignEnumeratorEx = Expression.Assign(EnumeratorVariable, Expression.Call(SourceParameter, SourceGetEnumeratorMethod));
				var callAddExp = Expression.Call(resultVarEx, addMethod, Expression.Invoke(convertEx, Expression.Property(EnumeratorVariable, "Current")));

				var breakTarget = Expression.Label();
				var blockEx = Expression.Block(
					new ParameterExpression[] { resultVarEx, EnumeratorVariable },
					resultAssignEx,
					assignEnumeratorEx,
					Expression.Loop(
						Expression.IfThenElse(
							Expression.NotEqual(moveNextEx, Expression.Constant(false)),
							callAddExp,
							Expression.Break(breakTarget)
						),
						breakTarget
					),
					Expression.Label(Expression.Label(ResultGenericEnumerableType), resultVarEx)
				);

				var lambdaEx = Expression.Lambda(blockEx, SourceParameter);

				return (Expression<Func<IEnumerable<TSource>, IEnumerable<TResult>>>)lambdaEx;
			}
		}
	}
}
