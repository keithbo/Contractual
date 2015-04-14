using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Utterance.Linq
{
	public static class Where
	{
		internal class Access<TSource>
		{
			public LambdaExpression SourcePredicate { get { return Cache<TSource>.SourcePredicate; } }
			public LambdaExpression SourceIndexedPredicate { get { return Cache<TSource>.SourceIndexedPredicate; } }
		}
		public static class Cache<TSource>
		{
			public static readonly Expression<Func<IEnumerable<TSource>, Func<TSource, bool>, IEnumerable<TSource>>> SourcePredicate = (source, predicate) => source.Where(predicate);
			public static readonly Expression<Func<IEnumerable<TSource>, Func<TSource, int, bool>, IEnumerable<TSource>>> SourceIndexedPredicate = (source, predicate) => source.Where(predicate);
		}
	}
}
