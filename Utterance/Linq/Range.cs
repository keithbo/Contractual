using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Utterance.Linq
{
	public static class Range
	{
		internal class Access
		{
			public LambdaExpression StartCount { get { return Cache.StartCount; } }
		}
		public static class Cache
		{
			public static readonly Expression<Func<int, int, IEnumerable<int>>> StartCount = (start, count) => Enumerable.Range(start, count);
		}
	}
}
