using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Utterance.Linq
{
	public static class All
	{
		internal class Access<TSource>
		{
			public LambdaExpression SourcePredicate { get { return All.Cache<TSource>.SourcePredicate; } }
		}
		public static class Cache<TSource>
		{
			public static readonly Expression<Func<IEnumerable<TSource>, Func<TSource, bool>, bool>> SourcePredicate = (source, predicate) => source.All(predicate);
		}
	}
}
