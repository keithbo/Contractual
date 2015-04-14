using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Utterance.Linq
{
	public static class Intersect
	{
		internal class Access<TSource>
		{
			public LambdaExpression FirstSecond { get { return Cache<TSource>.FirstSecond; } }
			public LambdaExpression FirstSecondComparer { get { return Cache<TSource>.FirstSecondComparer; } }
		}
		public static class Cache<TSource>
		{
			public static readonly Expression<Func<IEnumerable<TSource>, IEnumerable<TSource>, IEnumerable<TSource>>> FirstSecond = (first, second) => first.Intersect(second);
			public static readonly Expression<Func<IEnumerable<TSource>, IEnumerable<TSource>, IEqualityComparer<TSource>, IEnumerable<TSource>>> FirstSecondComparer = (first, second, comparer) => first.Intersect(second, comparer);
		}
	}
}
