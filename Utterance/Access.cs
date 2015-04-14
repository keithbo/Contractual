namespace Utterance
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Linq;
	using System.Linq.Expressions;
	using Utterance.Linq;

	public static class Access
	{
		public interface IAccess
		{
			LambdaExpression this[string name] { get; }
		}

		internal abstract class AccessBase : IAccess
		{
			private readonly Dictionary<string, Func<LambdaExpression>> map;
			protected AccessBase()
			{
				map = new Dictionary<string, Func<LambdaExpression>>();
			}

			protected void Register(Expression<Func<LambdaExpression>> ex)
			{
				var body = ex.Body as MemberExpression;
				if (body == null)
				{
					throw new ArgumentException("Register expects a Member accessor expression as input");
				}
				map.Add(body.Member.Name, ex.Compile());

			}

			public LambdaExpression this[string name]
			{
				get
				{
					Func<LambdaExpression> getter;
					if (map.TryGetValue(name, out getter))
					{
						return getter();
					}
					return null;
				}
			}
		}

		public enum MethodGroup
		{
			Aggregate,
			All,
			Any,
			AsEnumerable,
			Average,
			Cast,
			Concat,
			Contains,
			Count,
			DefaultIfEmpty,
			Distinct,
			ElementAt,
			ElementAtOrDefault,
			Empty,
			Except,
			First,
			FirstOrDefault,
			GroupBy,
			GroupJoin,
			Intersect,
			Join,
			Last,
			LastOrDefault,
			LongCount,
			Max,
			Min,
			OfType,
			OrderBy,
			OrderByDescending,
			Range,
			Repeat,
			Reverse,
			Select,
			SelectMany,
			SequenceEqual,
			Single,
			SingleOrDefault,
			Skip,
			SkipWhile,
			Sum,
			Take,
			TakeWhile,
			ThenBy,
			ThenByDescending,
			ToArray,
			ToDictionary,
			ToList,
			ToLookup,
			Union,
			Where,
			Zip
		}

		private static readonly IReadOnlyDictionary<MethodGroup, IReadOnlyCollection<Type>> Definitions =
			new ReadOnlyDictionary<MethodGroup, IReadOnlyCollection<Type>>(new Dictionary<MethodGroup, IReadOnlyCollection<Type>>
			{
				{MethodGroup.Aggregate, Pack(typeof(Aggregate.Access<>), typeof(Aggregate.Access<,>), typeof(Aggregate.Access<,,>))},
				{MethodGroup.All, Pack(typeof(All.Access<>))},
				{MethodGroup.Any, Pack(typeof(Any.Access<>))},
				{MethodGroup.AsEnumerable, Pack(typeof(AsEnumerable.Access<>))},
				{MethodGroup.Average, Pack(typeof(Average.Access), typeof(Average.Access<>))},
				{MethodGroup.Cast, Pack(typeof(Cast.Access<>))},
				{MethodGroup.Concat, Pack(typeof(Concat.Access<>))},
				{MethodGroup.Contains, Pack(typeof(Contains.Access<>))},
				{MethodGroup.Count, Pack(typeof(Linq.Count.Access<>))},
				{MethodGroup.DefaultIfEmpty, Pack(typeof(DefaultIfEmpty.Access<>))},
				{MethodGroup.Distinct, Pack(typeof(Distinct.Access<>))},
				{MethodGroup.ElementAt, Pack(typeof(ElementAt.Access<>))},
				{MethodGroup.ElementAtOrDefault, Pack(typeof(ElementAtOrDefault.Access<>))},
				{MethodGroup.Empty, Pack(typeof(Empty.Access<>))},
				{MethodGroup.Except, Pack(typeof(Except.Access<>))},
				{MethodGroup.First, Pack(typeof(First.Access<>))},
				{MethodGroup.FirstOrDefault, Pack(typeof(FirstOrDefault.Access<>))},
				{MethodGroup.GroupBy, Pack(typeof(GroupBy.Access<,>), typeof(GroupBy.AccessByElement<,,>), typeof(GroupBy.AccessByResult<,,>), typeof(GroupBy.Access<,,,>))},
				{MethodGroup.GroupJoin, Pack(typeof(GroupJoin.Access<,,,>))},
				{MethodGroup.Intersect, Pack(typeof(Intersect.Access<>))},
				{MethodGroup.Join, Pack(typeof(Join.Access<,,,>))},
				{MethodGroup.Last, Pack(typeof(Last.Access<>))},
				{MethodGroup.LastOrDefault, Pack(typeof(LastOrDefault.Access<>))},
				{MethodGroup.LongCount, Pack(typeof(LongCount.Access<>))},
				{MethodGroup.Max, Pack(typeof(Max.Access), typeof(Max.Access<>), typeof(Max.Access<,>))},
				{MethodGroup.Min, Pack(typeof(Min.Access), typeof(Min.Access<>), typeof(Min.Access<,>))},
				{MethodGroup.OfType, Pack(typeof(OfType.Access<>))},
				{MethodGroup.OrderBy, Pack(typeof(OrderBy.Access<,>))},
				{MethodGroup.OrderByDescending, Pack(typeof(OrderByDescending.Access<,>))},
				{MethodGroup.Range, Pack(typeof(Range.Access))},
				{MethodGroup.Repeat, Pack(typeof(Repeat.Access<>))},
				{MethodGroup.Reverse, Pack(typeof(Reverse.Access<>))},
				{MethodGroup.Select, Pack(typeof(Select.Access<,>))},
				{MethodGroup.SelectMany, Pack(typeof(SelectMany.Access<,>), typeof(SelectMany.Access<,,>))},
				{MethodGroup.SequenceEqual, Pack(typeof(SequenceEqual.Access<>))},
				{MethodGroup.Single, Pack(typeof(Linq.Single.Access<>))},
				{MethodGroup.SingleOrDefault, Pack(typeof(SingleOrDefault.Access<>))},
				{MethodGroup.Skip, Pack(typeof(Skip.Access<>))},
				{MethodGroup.SkipWhile, Pack(typeof(SkipWhile.Access<>))},
				{MethodGroup.Sum, Pack(typeof(Sum.Access), typeof(Sum.Access<>))},
				{MethodGroup.Take, Pack(typeof(Take.Access<>))},
				{MethodGroup.TakeWhile, Pack(typeof(TakeWhile.Access<>))},
				{MethodGroup.ThenBy, Pack(typeof(ThenBy.Access<,>))},
				{MethodGroup.ThenByDescending, Pack(typeof(ThenByDescending.Access<,>))},
				{MethodGroup.ToArray, Pack(typeof(ToArray.Access<>))},
				{MethodGroup.ToDictionary, Pack(typeof(ToDictionary.Access<,>), typeof(ToDictionary.Access<,,>))},
				{MethodGroup.ToList, Pack(typeof(ToList.Access<>))},
				{MethodGroup.ToLookup, Pack(typeof(ToLookup.Access<,>), typeof(ToLookup.Access<,,>))},
				{MethodGroup.Union, Pack(typeof(Union.Access<>))},
				{MethodGroup.Where, Pack(typeof(Where.Access<>))},
				{MethodGroup.Zip, Pack(typeof(Zip.Access<,,>))}
			});

		public static Type MakeType(MethodGroup group, params Type[] parameterTypes)
		{
			IReadOnlyCollection<Type> groupTypes;
			if (Definitions.TryGetValue(group, out groupTypes))
			{
				int paramCount = parameterTypes.Length;
				bool searchForGeneric = paramCount > 0;
				Type[] foundParams = new Type[0];
				Type found = groupTypes.FirstOrDefault(t =>
				{
					if (searchForGeneric)
					{
						if (t.IsGenericTypeDefinition)
						{
							var args = t.GetGenericArguments();
							if (args.Length == paramCount)
							{
								foundParams = args;
								return true;
							}
						}

						return false;
					}

					return !t.IsGenericTypeDefinition;
				});
				if (found == null)
				{
					throw new TypeLoadException();
				}
				return found.IsGenericTypeDefinition ? found.MakeGenericType(parameterTypes) : found;
			}

			throw new ArgumentException(string.Format("No group definition for {0} found", group));
		}

		public static ObjectFactory<IAccess> Create(MethodGroup group, params Type[] parameterTypes)
		{
			return Utterance.Factory.New<IAccess>(MakeType(group, parameterTypes));
		}

		private static IReadOnlyCollection<T> Pack<T>(params T[] items)
		{
			return new ReadOnlyCollection<T>(items ?? new T[0]);
		}
	}
}
