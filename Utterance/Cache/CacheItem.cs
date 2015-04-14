using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utterance.Cache
{
	public abstract class CacheItem<TKey, TValue>
		where TKey : IEquatable<TKey>
	{
		protected CacheItem(TKey key, TValue value)
		{
			Key = key;
			Value = value;
		}

		public TKey Key { get; protected set; }
		public TValue Value { get; protected set; }
	}
}
