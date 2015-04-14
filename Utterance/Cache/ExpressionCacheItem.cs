namespace Utterance.Cache
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Text;
	using System.Threading.Tasks;

	public class ExpressionCacheItem : ExpressionCacheItem<string>
	{
		public ExpressionCacheItem(string key, Expression expression)
			: base(key, expression)
		{
		}
	}

	public abstract class ExpressionCacheItem<TKey> : CacheItem<TKey, Expression>
	{
		protected ExpressionCacheItem(TKey key, Expression expression)
			: base(key, expression)
		{
		}
	}
}
