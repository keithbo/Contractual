namespace Utterance.Cache
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Text;
	using System.Threading.Tasks;

	public class CacheItemExpression : CacheItemExpression<string, ExpressionCacheItem>
	{
		public CacheItemExpression(ExpressionCacheItem cacheItem)
			: base(cacheItem)
		{
		}
	}

	public abstract class CacheItemExpression<TKey, TCacheItem> : Expression
		where TCacheItem : ExpressionCacheItem<TKey>
	{
		public TCacheItem CacheItem
		{
			get;
			private set;
		}

		public override bool CanReduce
		{
			get
			{
				return true;
			}
		}

		protected CacheItemExpression(TCacheItem cacheItem)
		{
			if (cacheItem == null)
			{
				throw new ArgumentNullException();
			}
			CacheItem = cacheItem;
		}

		public override Expression Reduce()
		{
			return CacheItem.Value;
		}
	}
}
