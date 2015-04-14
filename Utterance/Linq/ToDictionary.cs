using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Utterance.Linq
{
	public static class ToDictionary
	{
		internal class Access<TSource, TKey>
		{
			public LambdaExpression SourceKeySelector { get { return Cache<TSource, TKey>.SourceKeySelector; } }
			public LambdaExpression SourceKeySelectorComparer { get { return Cache<TSource, TKey>.SourceKeySelectorComparer; } }
		}
		internal class Access<TSource, TKey, TElement>
		{
			public LambdaExpression SourceKeySelectorElementSelector { get { return Cache<TSource, TKey, TElement>.SourceKeySelectorElementSelector; } }
			public LambdaExpression SourceKeySelectorElementSelectorComparer { get { return Cache<TSource, TKey, TElement>.SourceKeySelectorElementSelectorComparer; } }
		}
		public static class Cache<TSource, TKey>
		{
			public static readonly Expression<Func<IEnumerable<TSource>, Func<TSource, TKey>, Dictionary<TKey, TSource>>> SourceKeySelector = (source, keySelector) => source.ToDictionary(keySelector);
			public static readonly Expression<Func<IEnumerable<TSource>, Func<TSource, TKey>, IEqualityComparer<TKey>, Dictionary<TKey, TSource>>> SourceKeySelectorComparer = (source, keySelector, comparer) => source.ToDictionary(keySelector, comparer);
		}
		public static class Cache<TSource, TKey, TElement>
		{
			public static readonly Expression<Func<IEnumerable<TSource>, Func<TSource, TKey>, Func<TSource, TElement>, Dictionary<TKey, TElement>>> SourceKeySelectorElementSelector = (source, keySelector, elementSelector) => source.ToDictionary(keySelector, elementSelector);
			public static readonly Expression<Func<IEnumerable<TSource>, Func<TSource, TKey>, Func<TSource, TElement>, IEqualityComparer<TKey>, Dictionary<TKey, TElement>>> SourceKeySelectorElementSelectorComparer = (source, keySelector, elementSelector, comparer) => source.ToDictionary(keySelector, elementSelector, comparer);
		}
	}
}
