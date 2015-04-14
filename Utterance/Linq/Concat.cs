using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Utterance.Linq
{
	public static class Concat
	{
		internal class Access<TSource>
		{
			public LambdaExpression FirstSecond { get { return Concat.Cache<TSource>.FirstSecond; } }
		}
		public static class Cache<TSource>
		{
			public static readonly Expression<Func<IEnumerable<TSource>, IEnumerable<TSource>, IEnumerable<TSource>>> FirstSecond = (first, second) => first.Concat(second);
		}
	}
}
