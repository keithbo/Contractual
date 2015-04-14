namespace Utterance
{
	using System.Linq;
	using System.Threading;

	/// <summary>
	/// This class computes FNV-1a integer hashes with the ability to carry out multi-part or disjoint
	/// permutation sets. As such the hash can be reset to its initial state for additional hashing.
	/// 
	/// Note: FNV1AHash is "thread-safe" but multi-threaded computation will likely have
	/// unexpected results as in hashing order matters. Hashing [a,b,c] will have a different
	/// compute than [b,a,c]
	/// </summary>
	public sealed class FNV1AHash
	{
		private const int OffsetBasis = unchecked((int)2166136261);
		private const int Prime = 16777619;

		private int _hash;

		public int Value
		{
			get { return _hash; }
		}

		public FNV1AHash()
		{
			Reset();
		}

		public void Reset()
		{
			Interlocked.Exchange(ref _hash, OffsetBasis);
		}

		public int Step(params int[] steps)
		{
			int initial, value;
			do
			{
				initial = _hash;
				value = steps.Aggregate(initial, (r, o) => (r ^ o) * Prime);
			} while (initial != Interlocked.CompareExchange(ref _hash, value, initial));
			return value;
		}
	}
}
