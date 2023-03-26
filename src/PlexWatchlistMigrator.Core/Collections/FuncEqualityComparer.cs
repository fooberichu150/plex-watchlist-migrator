namespace PlexWatchlistMigrator.Collections
{
	// Stack overflow: http://stackoverflow.com/questions/98033/wrap-a-delegate-in-an-iequalitycomparer
	public class FuncEqualityComparer<T> : IEqualityComparer<T>
	{
		readonly Func<T, T, bool> _comparer;
		readonly Func<T, int> _hash;

		/// <summary>
		/// Constructs a FuncEqualityComparer
		/// </summary>
		/// <param name="comparer"></param>
		/// <example>
		/// FuncEqualityComparer<string> fieldComparer = new FuncEqualityComparer<string>((lhs, rhs) => lhs.CompareTo(rhs) == 0);
		/// </example>
		public FuncEqualityComparer(Func<T, T, bool> comparer)
			: this(comparer, t => 0) // NB Cannot assume anything about how e.g., t.GetHashCode() interacts with the comparer's behavior
		{
		}

		public FuncEqualityComparer(Func<T, T, bool> comparer, Func<T, int> hash)
		{
			_comparer = comparer;
			_hash = hash;
		}

		public bool Equals(T x, T y)
		{
			return _comparer(x, y);
		}

		public int GetHashCode(T obj)
		{
			return _hash(obj);
		}
	}
}
