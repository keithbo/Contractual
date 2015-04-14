namespace Utterance
{
	using System;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Reflection;

	public delegate object ObjectFactory(params object[] args);

	public delegate T ObjectFactory<T>(params object[] args);

	public static class Factory
	{
		private static readonly ParameterExpression ArgsParameter = Expression.Parameter(typeof(object[]), "args");

		#region Anonymous
		public static Expression<ObjectFactory> NewExpression(Type type, params Type[] argumentTypes)
		{
			Type[] args = argumentTypes ?? new Type[0];
			return args.Length == 0 ? NewExpressionNoArgs(type) : NewExpression(type.GetConstructor(args));
		}

		private static Expression<ObjectFactory> NewExpressionNoArgs(Type type)
		{
			return Expression.Lambda<ObjectFactory>(Expression.New(type), ArgsParameter);
		}

		public static ObjectFactory New(Type type, params Type[] argumentTypes)
		{
			return NewExpression(type, argumentTypes).Compile();
		}

		public static Expression<ObjectFactory> NewExpression(ConstructorInfo ctor)
		{
			NewExpression newExp = Expression.New(ctor, ctor.GetParameters().Select((p, i) => Expression.Convert(Expression.ArrayIndex(ArgsParameter, Expression.Constant(i)), p.ParameterType)));
			return Expression.Lambda<ObjectFactory>(newExp, ArgsParameter);
		}

		public static ObjectFactory New(ConstructorInfo ctor)
		{
			return NewExpression(ctor).Compile();
		}
		#endregion Anonymous
		#region Generic
		public static Expression<ObjectFactory<T>> NewExpression<T>(params Type[] argumentTypes)
		{
			return NewExpression<T>(typeof(T), argumentTypes);
		}

		public static Expression<ObjectFactory<T>> NewExpression<T>(Type type, params Type[] argumentTypes)
		{
			Type[] args = argumentTypes ?? new Type[0];
			return args.Length == 0 ? NewExpressionNoArgs<T>(type) : NewExpression<T>(typeof(T).GetConstructor(args));
		}

		public static Expression<ObjectFactory<T>> NewExpression<T>(ConstructorInfo ctor)
		{
			NewExpression newExp = Expression.New(ctor, ctor.GetParameters().Select((p, i) => Expression.Convert(Expression.ArrayIndex(ArgsParameter, Expression.Constant(i)), p.ParameterType)));
			return Expression.Lambda<ObjectFactory<T>>(newExp, ArgsParameter);
		}

		private static Expression<ObjectFactory<T>> NewExpressionNoArgs<T>(Type type)
		{
			return Expression.Lambda<ObjectFactory<T>>(Expression.New(type), ArgsParameter);
		}

		public static ObjectFactory<T> New<T>(params Type[] parameterTypes)
		{
			return NewExpression<T>(typeof(T), parameterTypes).Compile();
		}

		public static ObjectFactory<T> New<T>(Type type, params Type[] parameterTypes)
		{
			return NewExpression<T>(type, parameterTypes).Compile();
		}

		public static ObjectFactory<T> New<T>(ConstructorInfo ctor)
		{
			return NewExpression<T>(ctor).Compile();
		}

		#endregion Generic
	}
}
