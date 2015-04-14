using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Utterance.Linq
{
	public static class Select
	{
		internal class Access<TSource, TResult>
		{
			public LambdaExpression SourceSelector { get { return Cache<TSource, TResult>.SourceSelector; } }
			public LambdaExpression SourceIndexSelector { get { return Cache<TSource, TResult>.SourceIndexSelector; } }
		}
		public static class Cache<TSource, TResult>
		{
			public static readonly Expression<Func<IEnumerable<TSource>, Func<TSource, TResult>, IEnumerable<TResult>>> SourceSelector = (source, selector) => source.Select(selector);
			public static readonly Expression<Func<IEnumerable<TSource>, Func<TSource, int, TResult>, IEnumerable<TResult>>> SourceIndexSelector = (source, selector) => source.Select(selector);
		}
	}
}
