using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Utterance.Cache
{
	public abstract class Cache<TValue, TCacheItem> : Cache<string, TValue, TCacheItem>
		where TCacheItem : CacheItem<string, TValue>
	{

		protected Cache()
			: base(new StringCacheCore(), EqualityComparer<string>.Default)
		{
		}

		public class StringCacheCore : Cache<string, TValue, TCacheItem>.ICacheCore
		{
			private readonly string _root;
			private int _index;

			public StringCacheCore()
			{
				_root = Guid.NewGuid().ToString();
				_index = 0;
			}

			public string NewKey()
			{
				var next = Interlocked.Increment(ref _index);
				return _root + next;
			}
		}
	}

	public abstract class Cache<TKey, TValue, TCacheItem>
		where TKey : IEquatable<TKey>
		where TCacheItem : CacheItem<TKey, TValue>
	{
		private readonly IEqualityComparer<TKey> _keyEqualityComparer;
		private readonly Dictionary<TKey, TCacheItem> _cache;
		private readonly ICacheCore _core;
		private readonly object _synchronizationContext = new object();
		protected object SynchronizationContext
		{
			get { return _synchronizationContext; }
		}

		protected Cache()
			: this(null, null)
		{
		}

		protected Cache(ICacheCore core, IEqualityComparer<TKey> keyEqualityComparer)
		{
			_core = core ?? new DefaultCacheCore();
			_keyEqualityComparer = keyEqualityComparer ?? EqualityComparer<TKey>.Default;
			_cache = new Dictionary<TKey, TCacheItem>(_keyEqualityComparer);
		}

		public void AddOrUpdate(TKey key, TValue value, Func<TKey, TValue, TValue> update)
		{
			if (update == null)
			{
				throw new ArgumentNullException();
			}
			AddOrUpdateInternal(key, _ => value, update);
		}

		public void AddOrUpdate(TKey key, Func<TKey, TValue> add, Func<TKey, TValue, TValue> update)
		{
			if (add == null)
			{
				throw new ArgumentNullException();
			}
			if (update == null)
			{
				throw new ArgumentNullException();
			}
			AddOrUpdateInternal(key, add, update);
		}

		protected TCacheItem AddOrUpdateInternal(TKey key, Func<TKey, TValue> add, Func<TKey, TValue, TValue> update)
		{
			lock (SynchronizationContext)
			{
				TCacheItem item;
				if (_cache.TryGetValue(key, out item))
				{
					item = CreateCacheItem(key, update(key, item.Value));
				}
				else
				{
					item = CreateCacheItem(key, add(key));
				}

				_cache[key] = item;
				return item;
			}
		}

		public TValue GetOrAdd(TKey key, TValue value)
		{
			return GetOrAddInternal(key, _ => value).Value;
		}

		public TValue GetOrAdd(TKey key, Func<TKey, TValue> add)
		{
			if (add == null)
			{
				throw new ArgumentNullException();
			}
			return GetOrAddInternal(key, add).Value;
		}

		protected TCacheItem GetOrAddInternal(TKey key, Func<TKey, TValue> add)
		{
			lock (SynchronizationContext)
			{
				TCacheItem item;
				if (!_cache.TryGetValue(key, out item))
				{
					item = CreateCacheItem(key, add(key));
					_cache.Add(key, item);
				}
				return item;
			}
		}

		protected virtual TCacheItem CreateCacheItem(TKey key, TValue value)
		{
			return (TCacheItem)new CacheItem<TKey, TValue>(key, value);
		}

		public interface ICacheCore
		{
			TKey NewKey();
		}

		public class DefaultCacheCore : ICacheCore
		{
			public TKey NewKey()
			{
				throw new NotSupportedException("Automatic key generation not supported");
			}
		}
	}
}
