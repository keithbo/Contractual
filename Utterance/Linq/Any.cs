using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Utterance.Linq
{
	public static class Any
	{
		internal class Access<TSource>
		{
			public LambdaExpression Source { get { return Any.Cache<TSource>.Source; } }
			public LambdaExpression SourcePredicate { get { return Any.Cache<TSource>.SourcePredicate; } }
		}
		public static class Cache<TSource>
		{
			public static readonly Expression<Func<IEnumerable<TSource>, bool>> Source = (source) => source.Any();
			public static readonly Expression<Func<IEnumerable<TSource>, Func<TSource, bool>, bool>> SourcePredicate = (source, predicate) => source.Any(predicate);
		}
	}
}
