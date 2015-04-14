using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Contractual.App
{
	public class MyClass
	{
		public string Name { get; set; }
		public string Other { get; set; }

		public MyClassSub Sub { get; set; }

		//public List<MyClassSub> Subs { get; set; }
	}

	public class MyClassSub
	{
		public string Value1 { get; set; }
		public string Value2 { get; set; }
		public string Value3 { get; set; }

	}

	public class MyClassDto
	{
		public string Name { get; set; }
		public string Other { get; set; }

		public MyClassSubDto Sub { get; set; }

		//public List<MyClassSubDto> Subs { get; set; }
	}

	public class MyClassSubDto
	{
		public string Value1 { get; set; }
		public string Value2 { get; set; }
		public string Value3 { get; set; }

	}

	public static class Program
	{
		static void Main(string[] args)
		{
			//DoComposeCombine();

			//DoMemberInit<MyClassSub>();

			DoConvert();
		}

		static void DoConvert()
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
				},
				//Subs = new List<MyClassSub>
				//{
				//	new MyClassSub
				//	{
				//		Value1 = "Sub1_Broccoli",
				//		Value2 = "Sub1_Cheddar",
				//		Value3 = "Sub1_Soup"
				//	}
				//}
			};

			//MyClassDto dest = new MyClassDto
			//{
			//	Name = source.Name,
			//	Other = source.Other,
			//	Sub = ((Func<MyClassSub, MyClassSubDto>)((s) => new MyClassSubDto
			//	{
			//		Value1 = s.Value1,
			//		Value2 = s.Value2,
			//		Value3 = s.Value3
			//	}))(source.Sub),
			//	Subs = ((Func<List<MyClassSub>, List<MyClassSubDto>>)((list) =>
			//	{
			//		var output = new List<MyClassSubDto>();
			//		foreach (var item in list)
			//		{
			//			output.Add(((Func<MyClassSub, MyClassSubDto>)((s) => new MyClassSubDto
			//			{
			//				Value1 = s.Value1,
			//				Value2 = s.Value2,
			//				Value3 = s.Value3
			//			}))(item));
			//		}
			//		return output;
			//	}))(source.Subs)
			//};

			MyClassDto result = DoConvertInt<MyClass, MyClassDto>(source);

			System.Console.WriteLine();
		}

		static TResult DoConvertInt<TSource, TResult>(TSource source)
		{
			return Extensions.Convert<TSource, TResult>(source);
		}

		static void DoMemberInit<T>()
		{
			var targetType = typeof(T);
			var properties = targetType.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(p => p.CanRead && p.CanWrite);
			Expression expr = Expression.MemberInit(
				Expression.New(targetType),
				properties.Select(property => Expression.Bind(property, Expression.Constant("A Constant Value")))
			);

			var output = Expression.Lambda<Func<T>>(expr).Compile()();

			System.Console.WriteLine();
		}

		static void DoComposeCombine()
		{
			Expression<Func<MyClassSub, MyClassSubDto>> SubConverter = from => new MyClassSubDto
			{
				Value1 = from.Value1
			};

			Expression<Func<MyClass, MyClassSub>> SubSelector = source => source.Sub;

			//Expression<Func<MyClass, MyClassDto>> Converter = SubSelector.Compose(SubConverter).Combine((root, sub) => new MyClassDto
			//{
			//	Name = root.Name,
			//	Other = root.Other,
			//	Sub = sub
			//});

			//MyClass origin = new MyClass
			//{
			//	Name = "MyName",
			//	Other = "MyOther",
			//	Sub = new MyClassSub
			//	{
			//		Value1 = "MySubValue"
			//	}
			//};

			//MyClassDto originDto = Converter.Compile().Invoke(origin);

			//System.Console.WriteLine();
		}
	}
}
