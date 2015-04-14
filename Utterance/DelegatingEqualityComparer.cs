namespace Utterance
{
	using System;
	using System.Collections.Generic;

	public class DelegatingEqualityComparer<T> : IEqualityComparer<T>
	{
		private static IEqualityComparer<T> Default { get { return EqualityComparer<T>.Default; } }

		private readonly Func<T, T, bool> _equals;
		private readonly Func<T, int> _getHashCode;

		public DelegatingEqualityComparer(Func<T, T, bool> equals)
			: this(equals, Default.GetHashCode)
		{
			_equals = equals;
		}

		public DelegatingEqualityComparer(Func<T, T, bool> equals, Func<T, int> getHashCode)
		{
			_equals = equals;
			_getHashCode = getHashCode;
		}

		public bool Equals(T x, T y)
		{
			return _equals(x, y);
		}

		public int GetHashCode(T obj)
		{
			return _getHashCode(obj);
		}
	}
}
