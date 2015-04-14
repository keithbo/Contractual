using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Utterance.Linq
{
	public static class Skip
	{
		internal class Access<TSource>
		{
			public LambdaExpression SourceCount { get { return Cache<TSource>.SourceCount; } }
		}
		public static class Cache<TSource>
		{
			public static readonly Expression<Func<IEnumerable<TSource>, int, IEnumerable<TSource>>> SourceCount = (source, count) => source.Skip(count);
		}
	}
}
