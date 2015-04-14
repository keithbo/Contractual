using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Utterance.Linq
{
	public static class GroupJoin
	{
		internal class Access<TOuter, TInner, TKey, TResult>
		{
			public LambdaExpression OuterInnerOuterKeySelectorInnerKeySelectorResultSelector { get { return Cache<TOuter, TInner, TKey, TResult>.OuterInnerOuterKeySelectorInnerKeySelectorResultSelector; } }
			public LambdaExpression OuterInnerOuterKeySelectorInnerKeySelectorResultSelectorComparer { get { return Cache<TOuter, TInner, TKey, TResult>.OuterInnerOuterKeySelectorInnerKeySelectorResultSelectorComparer; } }
		}
		public static class Cache<TOuter, TInner, TKey, TResult>
		{
			public static readonly Expression<Func<IEnumerable<TOuter>, IEnumerable<TInner>, Func<TOuter, TKey>, Func<TInner, TKey>, Func<TOuter, IEnumerable<TInner>, TResult>, IEnumerable<TResult>>> OuterInnerOuterKeySelectorInnerKeySelectorResultSelector = (outer, inner, outerKeySelector, innerKeySelector, resultSelector) => outer.GroupJoin(inner, outerKeySelector, innerKeySelector, resultSelector);
			public static readonly Expression<Func<IEnumerable<TOuter>, IEnumerable<TInner>, Func<TOuter, TKey>, Func<TInner, TKey>, Func<TOuter, IEnumerable<TInner>, TResult>, IEqualityComparer<TKey>, IEnumerable<TResult>>> OuterInnerOuterKeySelectorInnerKeySelectorResultSelectorComparer = (outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer) => outer.GroupJoin(inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
		}
	}
}
