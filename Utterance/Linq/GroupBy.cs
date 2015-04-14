using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Utterance.Linq
{
	public static class GroupBy
	{
		internal class Access<TSource, TKey>
		{
			public LambdaExpression SourceKeySelector { get { return Cache<TSource, TKey>.SourceKeySelector; } }
			public LambdaExpression SourceKeySelectorComparer { get { return Cache<TSource, TKey>.SourceKeySelectorComparer; } }
		}
		internal class AccessByElement<TSource, TKey, TElement>
		{
			public LambdaExpression SourceKeySelectorElementSelector { get { return CacheByElement<TSource, TKey, TElement>.SourceKeySelectorElementSelector; } }
			public LambdaExpression SourceKeySelectorElementSelectorComparer { get { return CacheByElement<TSource, TKey, TElement>.SourceKeySelectorElementSelectorComparer; } }
		}
		internal class AccessByResult<TSource, TKey, TResult>
		{
			public LambdaExpression SourceKeySelectorResultSelector { get { return CacheByResult<TSource, TKey, TResult>.SourceKeySelectorResultSelector; } }
			public LambdaExpression SourceKeySelectorResultSelectorComparer { get { return CacheByResult<TSource, TKey, TResult>.SourceKeySelectorResultSelectorComparer; } }
		}
		internal class Access<TSource, TKey, TElement, TResult>
		{
			public LambdaExpression SourceKeySelectorElementSelectorResultSelector { get { return Cache<TSource, TKey, TElement, TResult>.SourceKeySelectorElementSelectorResultSelector; } }
			public LambdaExpression SourceKeySelectorElementSelectorResultSelectorComparer { get { return Cache<TSource, TKey, TElement, TResult>.SourceKeySelectorElementSelectorResultSelectorComparer; } }
		}
		public static class Cache<TSource, TKey>
		{
			public static readonly Expression<Func<IEnumerable<TSource>, Func<TSource, TKey>, IEnumerable<IGrouping<TKey, TSource>>>> SourceKeySelector = (source, keySelector) => source.GroupBy(keySelector);
			public static readonly Expression<Func<IEnumerable<TSource>, Func<TSource, TKey>, IEqualityComparer<TKey>, IEnumerable<IGrouping<TKey, TSource>>>> SourceKeySelectorComparer = (source, keySelector, comparer) => source.GroupBy(keySelector, comparer);
		}
		public static class CacheByElement<TSource, TKey, TElement>
		{
			public static readonly Expression<Func<IEnumerable<TSource>, Func<TSource, TKey>, Func<TSource, TElement>, IEnumerable<IGrouping<TKey, TElement>>>> SourceKeySelectorElementSelector = (source, keySelector, elementSelector) => source.GroupBy(keySelector, elementSelector);
			public static readonly Expression<Func<IEnumerable<TSource>, Func<TSource, TKey>, Func<TSource, TElement>, IEqualityComparer<TKey>, IEnumerable<IGrouping<TKey, TElement>>>> SourceKeySelectorElementSelectorComparer = (source, keySelector, elementSelector, comparer) => source.GroupBy(keySelector, elementSelector, comparer);
		}
		public static class CacheByResult<TSource, TKey, TResult>
		{
			public static readonly Expression<Func<IEnumerable<TSource>, Func<TSource, TKey>, Func<TKey, IEnumerable<TSource>, TResult>, IEnumerable<TResult>>> SourceKeySelectorResultSelector = (source, keySelector, resultSelector) => source.GroupBy(keySelector, resultSelector);
			public static readonly Expression<Func<IEnumerable<TSource>, Func<TSource, TKey>, Func<TKey, IEnumerable<TSource>, TResult>, IEqualityComparer<TKey>, IEnumerable<TResult>>> SourceKeySelectorResultSelectorComparer = (source, keySelector, resultSelector, comparer) => source.GroupBy(keySelector, resultSelector, comparer);
		}
		public static class Cache<TSource, TKey, TElement, TResult>
		{
			public static readonly Expression<Func<IEnumerable<TSource>, Func<TSource, TKey>, Func<TSource, TElement>, Func<TKey, IEnumerable<TElement>, TResult>, IEnumerable<TResult>>> SourceKeySelectorElementSelectorResultSelector = (source, keySelector, elementSelector, resultSelector) => source.GroupBy(keySelector, elementSelector, resultSelector);
			public static readonly Expression<Func<IEnumerable<TSource>, Func<TSource, TKey>, Func<TSource, TElement>, Func<TKey, IEnumerable<TElement>, TResult>, IEqualityComparer<TKey>, IEnumerable<TResult>>> SourceKeySelectorElementSelectorResultSelectorComparer = (source, keySelector, elementSelector, resultSelector, comparer) => source.GroupBy(keySelector, elementSelector, resultSelector, comparer);
		}
	}
}
