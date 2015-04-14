using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Utterance.Linq
{
	public static class Zip
	{
		internal class Access<TFirst, TSecond, TResult>
		{
			public LambdaExpression FirstSecondResultSelector { get { return Cache<TFirst, TSecond, TResult>.FirstSecondResultSelector; } }
		}
		public static class Cache<TFirst, TSecond, TResult>
		{
			public static readonly Expression<Func<IEnumerable<TFirst>, IEnumerable<TSecond>, Func<TFirst, TSecond, TResult>, IEnumerable<TResult>>> FirstSecondResultSelector = (first, second, resultSelector) => first.Zip(second, resultSelector);
		}
	}
}
