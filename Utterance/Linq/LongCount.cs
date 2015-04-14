using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Utterance.Linq
{
	public static class LongCount
	{
		internal class Access<TSource>
		{
			public LambdaExpression Source { get { return Cache<TSource>.Source; } }
			public LambdaExpression SourcePredicate { get { return Cache<TSource>.SourcePredicate; } }
		}
		public static class Cache<TSource>
		{
			public static readonly Expression<Func<IEnumerable<TSource>, long>> Source = (source) => source.LongCount();
			public static readonly Expression<Func<IEnumerable<TSource>, Func<TSource, bool>, long>> SourcePredicate = (source, predicate) => source.LongCount(predicate);
		}
	}
}
