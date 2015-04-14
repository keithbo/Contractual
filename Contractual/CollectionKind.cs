using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contractual
{
	internal enum CollectionKind : byte
	{
		None,
		GenericDictionary,
		Dictionary,
		GenericList,
		GenericCollection,
		List,
		GenericEnumerable,
		Collection,
		Enumerable,
		Array
	}
}
