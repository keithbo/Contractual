using Contractual.App;
using NLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Contractual.Tests
{
	public class Tests
	{
		[Fact]
		public void LinqSelectTest()
		{

			MyClass source = new MyClass
			{
				Name = "Trinity",
				Other = "Jones",
				Sub = new MyClassSub
				{
					Value1 = "Roast",
					Value2 = "Beef",
					Value3 = "Sandwich"
				}
			};

			var param = Expression.Parameter(typeof(MyClass[]));
			var contract = TypePairingContract.GetContract(typeof(MyClass), typeof(MyClassDto));
			var contractConvert = contract.Convert();
			Expression select = null;//Linq.Factory.Create<MyClass, MyClassDto>().SelectSourceSelector;
			var ex = Expression.Invoke(select, param, contractConvert);

			var lambda = Expression.Lambda(ex, param);
			var compiled = lambda.Compile();
			var result = compiled.DynamicInvoke(new object[] { new MyClass[] { source } });
			
			System.Console.WriteLine();
		}

		class MyClassWithArray : MyClass
		{
			public MyClassSub[] SubArray { get; set; }
		}

		class MyClassWithArrayDto : MyClassDto
		{
			public MyClassSubDto[] SubArray { get; set; }
		}

		[Fact]
		public void ObjectWithNestedArrayTest()
		{
			MyClassWithArray source = new MyClassWithArray
			{
				Name = "Trinity",
				Other = "Jones",
				Sub = new MyClassSub
				{
					Value1 = "Roast",
					Value2 = "Beef",
					Value3 = "Sandwich"
				},
				SubArray = new MyClassSub[] 
				{
					new MyClassSub
					{
						Value1 = "Old",
						Value2 = "Stinky",
						Value3 = "Socks"
					}
				}
			};

			var contract = TypePairingContract.GetContract(typeof(MyClassWithArray), typeof(MyClassWithArrayDto));
			var convert = contract.Convert();
			var compiled = convert.Compile();
			var result = compiled.DynamicInvoke(new object[] { source });

			System.Console.WriteLine();
		}

		[Fact]
		public void ArrayOfStringsTest()
		{
			string[] myStrings = new string[] { "First", "Second", "Blah" };

			var contract = TypePairingContract.GetContract(typeof(string[]), typeof(string[]));
			var convert = contract.Convert();
			var compiled = convert.Compile();
			var result = compiled.DynamicInvoke(new object[] { myStrings });

			System.Console.WriteLine();
		}

		[Fact]
		public void TryFindInterfaceTypeInfoTest()
		{
			var type = typeof(IList<string>);

			Type interfaceType;
			Type itemType;
			CollectionKind interfaceKind;
			MethodInfo addMethod;
			MethodInfo enumeratorMethod;
			CollectionTypeContract.TryFindInterfaceTypeInfo(type, out interfaceType, out interfaceKind, out itemType, out addMethod, out enumeratorMethod);

			System.Console.WriteLine();
		}

		[Fact]
		public void LinqAccessTest()
		{
			MyClass source = new MyClass
			{
				Name = "Trinity",
				Other = "Jones",
				Sub = new MyClassSub
				{
					Value1 = "Roast",
					Value2 = "Beef",
					Value3 = "Sandwich"
				}
			};

			//var selectDef = typeof(Linq).GetMethod("SelectSourceSelector", BindingFlags.NonPublic | BindingFlags.Static);
			//var select = selectDef.MakeGenericMethod(typeof(int), typeof(string));

			//var selectTDef = typeof(Linq.Select<,>);
			//var selectT = selectTDef.MakeGenericType(typeof(int), typeof(string));

			//selectTDef.GetProperty("SourceSelector").

			var param1 = Expression.Parameter(typeof(MyClass[]), "source");
			var param2 = Expression.Parameter(typeof(Func<MyClass,MyClassDto>), "selector");

			var contract = TypePairingContract.GetContract(typeof(MyClass), typeof(MyClassDto));
			var select = contract.LinqAccess.SelectSourceSelector;
			var toarray = contract.LinqAccess.ToArraySource;

			var compose = select.Compose(toarray, param1, param2);
			var invoke = Expression.Invoke(compose, param1, contract.Convert());
			var lambda = Expression.Lambda(invoke, param1);

			var result = lambda.Compile().DynamicInvoke(new object[] { new MyClass[] { source } });

			//innerPairing.LinqAccess.SelectSourceSelector.Compose(innerPairing.LinqAccess.ToArraySource, )

			System.Console.WriteLine();
		}

		[Fact]
		public void BuildCollectionConverterTest()
		{
			var sourceType = typeof(MyClass);
			var sourceListType = typeof(List<MyClass>);
			var sourceEnumeratorType = typeof(IEnumerator<MyClass>);
			var sourceEnumerableType = typeof(IEnumerable<MyClass>);
			var resultType = typeof(MyClassDto);
			var resultListType = typeof(List<MyClassDto>);

			var sourceParamEx = Expression.Parameter(sourceEnumerableType, "source");

			var constructor = resultListType.GetConstructor(new Type[0]);
			var method = resultListType.GetMethod("Add");

			var contract = TypePairingContract.GetContract(sourceType, resultType);
			var convertEx = contract.Convert();// TSource -> TResult

			var resultNewExp = Expression.New(constructor); // IEnumerable<TResult>
			var resultVarEx = Expression.Variable(constructor.DeclaringType, "result");
			var resultAssignEx = Expression.Assign(resultVarEx, resultNewExp);

			//var sourceItemEx = Expression.Variable(sourceType, "item");

			var enumeratorVarEx = Expression.Variable(sourceEnumeratorType, "enumerator");
			var moveNextEx = Expression.Call(enumeratorVarEx, typeof(IEnumerator).GetMethod("MoveNext"));
			var assignEnumeratorEx = Expression.Assign(enumeratorVarEx, Expression.Call(sourceParamEx, sourceEnumerableType.GetMethod("GetEnumerator")));
			//var assignCurrentEx = Expression.Assign(sourceItemEx, );
			var callAddExp = Expression.Call(resultVarEx, method, Expression.Invoke(convertEx, Expression.Property(enumeratorVarEx, "Current")));

			var breakTarget = Expression.Label();
			var blockEx = Expression.Block(
				new ParameterExpression[] { resultVarEx, enumeratorVarEx },
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
				Expression.Label(Expression.Label(constructor.DeclaringType), resultVarEx)
			);

			var lambdaEx = Expression.Lambda(blockEx, sourceParamEx);

			var source = new List<MyClass>
			{
				new MyClass
				{
					Name = "Trinity",
					Other = "Jones",
					Sub = new MyClassSub
					{
						Value1 = "Roast",
						Value2 = "Beef",
						Value3 = "Sandwich"
					}
				}
			};

			var result = lambdaEx.Compile().DynamicInvoke(new object[] { source });

			System.Console.WriteLine();

		}

		[Fact]
		public void BuildCollectionConverterHelperTest()
		{
			var sourceType = typeof(List<MyClass>);
			var resultType = typeof(List<MyClassDto>);
			var constructor = resultType.GetConstructor(new Type[0]);
			var method = resultType.GetMethod("Add");

			var lambda = Contractual.CollectionTypeContract.GenericCollectionHelper<MyClass, MyClassDto>.ConvertEnumerable(constructor, method);

			var source = new List<MyClass>
			{
				new MyClass
				{
					Name = "Trinity",
					Other = "Jones",
					Sub = new MyClassSub
					{
						Value1 = "Roast",
						Value2 = "Beef",
						Value3 = "Sandwich"
					}
				}
			};

			var result = lambda.Compile().DynamicInvoke(new object[] { source });

			System.Console.WriteLine();
		}

		[Fact]
		public void ListTypeContractTest()
		{
			var contract = TypePairingContract.GetContract(typeof(List<MyClass>), typeof(List<MyClassDto>));

			var lambda = contract.Convert();

			var source = new List<MyClass>
			{
				new MyClass
				{
					Name = "Trinity",
					Other = "Jones",
					Sub = new MyClassSub
					{
						Value1 = "Roast",
						Value2 = "Beef",
						Value3 = "Sandwich"
					}
				}
			};

			var result = lambda.Compile().DynamicInvoke(new object[] { source });


			System.Console.WriteLine();
		}
	}
}
