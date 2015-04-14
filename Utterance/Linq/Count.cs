using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Utterance.Linq
{
	public static class Count
	{
		internal class Access<TSource>
		{
			public LambdaExpression Source { get { return Count.Cache<TSource>.Source; } }
			public LambdaExpression SourcePredicate { get { return Count.Cache<TSource>.SourcePredicate; } }
		}
		public static class Cache<TSource>
		{
			public static readonly Expression<Func<IEnumerable<TSource>, int>> Source = (source) => source.Count();
			public static readonly Expression<Func<IEnumerable<TSource>, Func<TSource, bool>, int>> SourcePredicate = (source, predicate) => source.Count(predicate);
		}
	}
}
