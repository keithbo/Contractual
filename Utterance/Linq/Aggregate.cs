using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Utterance.Linq
{
	public static class Aggregate
	{
		#region Interfaces
		public interface IAggregateAccess : Utterance.Access.IAccess
		{
			LambdaExpression SourceFunc { get; }
		}
		#endregion Interfaces
		#region Access
		internal class Access<TSource> : Utterance.Access.AccessBase, IAggregateAccess
		{
			public Access()
			{
				Register(() => this.SourceFunc);
			}

			public LambdaExpression SourceFunc { get { return Cache<TSource>.SourceFunc; } }
		}
		internal class Access<TSource, TAccumulate> : Utterance.Access.AccessBase
		{
			public Access()
			{
				Register(() => this.SourceSeedFunc);
			}

			public LambdaExpression SourceSeedFunc { get { return Cache<TSource, TAccumulate>.SourceSeedFunc; } }
		}
		internal class Access<TSource, TAccumulate, TResult> : Utterance.Access.AccessBase
		{
			public Access()
			{
				Register(() => this.SourceSeedFuncResultSelector);
			}

			public LambdaExpression SourceSeedFuncResultSelector { get { return Cache<TSource, TAccumulate, TResult>.SourceSeedFuncResultSelector; } }
		}
		#endregion Access
		#region Cache
		public static class Cache<TSource>
		{
			public static readonly Expression<Func<IEnumerable<TSource>, Func<TSource, TSource, TSource>, TSource>> SourceFunc = (source, func) => source.Aggregate(func);
		}

		public static class Cache<TSource, TAccumulate>
		{
			public static readonly Expression<Func<IEnumerable<TSource>, TAccumulate, Func<TAccumulate, TSource, TAccumulate>, TAccumulate>> SourceSeedFunc = (source, seed, func) => source.Aggregate(seed, func);
		}

		public static class Cache<TSource, TAccumulate, TResult>
		{
			public static readonly Expression<Func<IEnumerable<TSource>, TAccumulate, Func<TAccumulate, TSource, TAccumulate>, Func<TAccumulate, TResult>, TResult>> SourceSeedFuncResultSelector = (source, seed, func, resultSelector) => source.Aggregate(seed, func, resultSelector);
		}
		#endregion Cache
	}
}
