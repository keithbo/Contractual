using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Utterance.Linq
{
	public static class ToList
	{
		internal class Access<TSource>
		{
			public LambdaExpression Source { get { return Cache<TSource>.Source; } }
		}
		public static class Cache<TSource>
		{
			public static readonly Expression<Func<IEnumerable<TSource>, List<TSource>>> Source = (source) => source.ToList();
		}
	}
}
