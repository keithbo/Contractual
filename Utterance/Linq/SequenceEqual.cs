using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Utterance.Linq
{
	public static class SequenceEqual
	{
		internal class Access<TSource>
		{
			public LambdaExpression FirstSecond { get { return Cache<TSource>.FirstSecond; } }
			public LambdaExpression FirstSecondComparer { get { return Cache<TSource>.FirstSecondComparer; } }
		}
		public static class Cache<TSource>
		{
			public static readonly Expression<Func<IEnumerable<TSource>, IEnumerable<TSource>, bool>> FirstSecond = (first, second) => first.SequenceEqual(second);
			public static readonly Expression<Func<IEnumerable<TSource>, IEnumerable<TSource>, IEqualityComparer<TSource>, bool>> FirstSecondComparer = (first, second, comparer) => first.SequenceEqual(second, comparer);
		}
	}
}
