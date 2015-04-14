using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Utterance.Linq
{
	public static class OfType
	{
		internal class Access<TResult>
		{
			public LambdaExpression Source { get { return Cache<TResult>.Source; } }
		}
		public static class Cache<TResult>
		{
			public static readonly Expression<Func<IEnumerable, IEnumerable<TResult>>> Source = (source) => source.OfType<TResult>();
		}
	}
}
