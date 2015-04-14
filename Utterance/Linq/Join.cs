using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Utterance.Linq
{
	public static class Join
	{
		internal class Access<TOuter, TInner, TKey, TResult>
		{
			public LambdaExpression OuterInnerOuterKeySelectorInnerKeySelectorResultSelector { get { return Cache<TOuter, TInner, TKey, TResult>.OuterInnerOuterKeySelectorInnerKeySelectorResultSelector; } }
			public LambdaExpression OuterInnerOuterKeySelectorInnerKeySelectorResultSelectorComparer { get { return Cache<TOuter, TInner, TKey, TResult>.OuterInnerOuterKeySelectorInnerKeySelectorResultSelectorComparer; } }
		}
		public static class Cache<TOuter, TInner, TKey, TResult>
		{
			public static readonly Expression<Func<IEnumerable<TOuter>, IEnumerable<TInner>, Func<TOuter, TKey>, Func<TInner, TKey>, Func<TOuter, TInner, TResult>, IEnumerable<TResult>>> OuterInnerOuterKeySelectorInnerKeySelectorResultSelector = (outer, inner, outerKeySelector, innerKeySelector, resultSelector) => outer.Join(inner, outerKeySelector, innerKeySelector, resultSelector);
			public static readonly Expression<Func<IEnumerable<TOuter>, IEnumerable<TInner>, Func<TOuter, TKey>, Func<TInner, TKey>, Func<TOuter, TInner, TResult>, IEqualityComparer<TKey>, IEnumerable<TResult>>> OuterInnerOuterKeySelectorInnerKeySelectorResultSelectorComparer = (outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer) => outer.Join(inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
		}
	}
}
