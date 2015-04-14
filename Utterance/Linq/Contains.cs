using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Utterance.Linq
{
	public static class Contains
	{
		internal class Access<TSource>
		{
			public LambdaExpression SourceValue { get { return Contains.Cache<TSource>.SourceValue; } }
			public LambdaExpression SourceValueComparer { get { return Contains.Cache<TSource>.SourceValueComparer; } }
		}
		public static class Cache<TSource>
		{
			public static readonly Expression<Func<IEnumerable<TSource>, TSource, bool>> SourceValue = (source, value) => source.Contains(value);
			public static readonly Expression<Func<IEnumerable<TSource>, TSource, IEqualityComparer<TSource>, bool>> SourceValueComparer = (source, value, comparer) => source.Contains(value, comparer);
		}
	}
}
