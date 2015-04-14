using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Utterance.Linq
{
	public static class Distinct
	{
		internal class Access<TSource>
		{
			public LambdaExpression Source { get { return Cache<TSource>.Source; } }
			public LambdaExpression SourceComparer { get { return Cache<TSource>.SourceComparer; } }
		}
		public static class Cache<TSource>
		{
			public static readonly Expression<Func<IEnumerable<TSource>, IEnumerable<TSource>>> Source = (source) => source.Distinct();
			public static readonly Expression<Func<IEnumerable<TSource>, IEqualityComparer<TSource>, IEnumerable<TSource>>> SourceComparer = (source, comparer) => source.Distinct(comparer);
		}
	}
}
