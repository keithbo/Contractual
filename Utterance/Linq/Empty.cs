using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Utterance.Linq
{
	public static class Empty
	{
		internal class Access<TResult>
		{
			public LambdaExpression _ { get { return Cache<TResult>._; } }
		}
		public static class Cache<TResult>
		{
			public static readonly Expression<Func<IEnumerable<TResult>>> _ = () => Enumerable.Empty<TResult>();
		}
	}
}
