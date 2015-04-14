using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Utterance.Linq
{
	public static class Repeat
	{
		internal class Access<TResult>
		{
			public LambdaExpression ElementCount { get { return Cache<TResult>.ElementCount; } }
		}
		public static class Cache<TResult>
		{
			public static readonly Expression<Func<TResult, int, IEnumerable<TResult>>> ElementCount = (element, count) => Enumerable.Repeat(element, count);
		}
	}
}
