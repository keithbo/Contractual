using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Utterance.Linq
{
	public static class SelectMany
	{
		internal class Access<TSource, TResult>
		{
			public LambdaExpression SourceSelector { get { return Cache<TSource, TResult>.SourceSelector; } }
			public LambdaExpression SourceIndexedSelector { get { return Cache<TSource, TResult>.SourceIndexedSelector; } }
		}
		internal class Access<TSource, TCollection, TResult>
		{
			public LambdaExpression SourceCollectionSelectorResultSelector { get { return Cache<TSource, TCollection, TResult>.SourceCollectionSelectorResultSelector; } }
			public LambdaExpression SourceIndexedCollectionSelectorResultSelector { get { return Cache<TSource, TCollection, TResult>.SourceIndexedCollectionSelectorResultSelector; } }
		}
		public static class Cache<TSource, TResult>
		{
			public static readonly Expression<Func<IEnumerable<TSource>, Func<TSource, IEnumerable<TResult>>, IEnumerable<TResult>>> SourceSelector = (source, selector) => source.SelectMany(selector);
			public static readonly Expression<Func<IEnumerable<TSource>, Func<TSource, int, IEnumerable<TResult>>, IEnumerable<TResult>>> SourceIndexedSelector = (source, selector) => source.SelectMany(selector);
		}
		public static class Cache<TSource, TCollection, TResult>
		{
			public static readonly Expression<Func<IEnumerable<TSource>, Func<TSource, IEnumerable<TCollection>>, Func<TSource, TCollection, TResult>, IEnumerable<TResult>>> SourceCollectionSelectorResultSelector = (source, collectionSelector, resultSelector) => source.SelectMany(collectionSelector, resultSelector);
			public static readonly Expression<Func<IEnumerable<TSource>, Func<TSource, int, IEnumerable<TCollection>>, Func<TSource, TCollection, TResult>, IEnumerable<TResult>>> SourceIndexedCollectionSelectorResultSelector = (source, collectionSelector, resultSelector) => source.SelectMany(collectionSelector, resultSelector);
		}
	}
}
