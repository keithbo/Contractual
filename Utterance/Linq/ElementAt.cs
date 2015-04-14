using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Utterance.Linq
{
	public static class ElementAt
	{
		internal class Access<TSource>
		{
			public LambdaExpression SourceIndex { get { return Cache<TSource>.SourceIndex; } }
		}
		public static class Cache<TSource>
		{
			public static readonly Expression<Func<IEnumerable<TSource>, int, TSource>> SourceIndex = (source, index) => source.ElementAt(index);
		}
	}
}
