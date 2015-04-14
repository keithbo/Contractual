using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contractual
{
	internal static class WellKnownTypes
	{
		#region Fields
		private static Type typeOfObject;
		private static Type typeOfValueType;
		private static Type typeOfArray;
		private static Type typeOfString;
		private static Type typeOfInt;
		private static Type typeOfULong;
		private static Type typeOfVoid;
		private static Type typeOfByteArray;
		private static Type typeOfTimeSpan;
		private static Type typeOfGuid;
		private static Type typeOfDateTimeOffset;
		private static Type typeOfDateTimeOffsetAdapter;
		private static Type typeOfUri;
		private static Type typeOfTypeEnumerable;
		private static Type typeOfStreamingContext;
		private static Type typeOfISerializable;
		private static Type typeOfIDeserializationCallback;
		private static Type typeOfIObjectReference;
		private static Type typeOfXmlFormatClassWriterDelegate;
		private static Type typeOfXmlFormatCollectionWriterDelegate;
		private static Type typeOfXmlFormatClassReaderDelegate;
		private static Type typeOfXmlFormatCollectionReaderDelegate;
		private static Type typeOfXmlFormatGetOnlyCollectionReaderDelegate;
		private static Type typeOfKnownTypeAttribute;
		private static Type typeOfDataContractAttribute;
		private static Type typeOfContractNamespaceAttribute;
		private static Type typeOfDataMemberAttribute;
		private static Type typeOfEnumMemberAttribute;
		private static Type typeOfCollectionDataContractAttribute;
		private static Type typeOfOptionalFieldAttribute;
		private static Type typeOfObjectArray;
		private static Type typeOfOnSerializingAttribute;
		private static Type typeOfOnSerializedAttribute;
		private static Type typeOfOnDeserializingAttribute;
		private static Type typeOfOnDeserializedAttribute;
		private static Type typeOfFlagsAttribute;
		private static Type typeOfSerializableAttribute;
		private static Type typeOfNonSerializedAttribute;
		private static Type typeOfSerializationInfo;
		private static Type typeOfSerializationInfoEnumerator;
		private static Type typeOfSerializationEntry;
		private static Type typeOfIXmlSerializable;
		private static Type typeOfXmlSchemaProviderAttribute;
		private static Type typeOfXmlRootAttribute;
		private static Type typeOfXmlQualifiedName;
		private static Type typeOfXmlSchemaType;
		private static Type typeOfXmlSerializableServices;
		private static Type typeOfXmlNodeArray;
		private static Type typeOfXmlSchemaSet;
		private static Type typeOfIPropertyChange;
		private static Type typeOfIExtensibleDataObject;
		private static Type typeOfExtensionDataObject;
		private static Type typeOfISerializableDataNode;
		private static Type typeOfClassDataNode;
		private static Type typeOfCollectionDataNode;
		private static Type typeOfXmlDataNode;
		private static Type typeOfNullable;
		private static Type typeOfReflectionPointer;
		private static Type typeOfIDictionaryGeneric;
		private static Type typeOfIDictionary;
		private static Type typeOfIListGeneric;
		private static Type typeOfIList;
		private static Type typeOfICollectionGeneric;
		private static Type typeOfICollection;
		private static Type typeOfIEnumerableGeneric;
		private static Type typeOfIEnumerable;
		private static Type typeOfIEnumeratorGeneric;
		private static Type typeOfIEnumerator;
		private static Type typeOfKeyValuePair;
		private static Type typeOfKeyValue;
		private static Type typeOfIDictionaryEnumerator;
		private static Type typeOfDictionaryEnumerator;
		private static Type typeOfGenericDictionaryEnumerator;
		private static Type typeOfDictionaryGeneric;
		private static Type typeOfHashtable;
		private static Type typeOfListGeneric;
		private static Type typeOfXmlElement;
		private static Type typeOfDBNull;
		#endregion Fields

		#region Properties
		internal static Type TypeOfObject
		{
			get
			{
				if (WellKnownTypes.typeOfObject == null)
				{
					WellKnownTypes.typeOfObject = typeof(object);
				}
				return WellKnownTypes.typeOfObject;
			}
		}
		internal static Type TypeOfArray
		{
			get
			{
				if (WellKnownTypes.typeOfArray == null)
				{
					WellKnownTypes.typeOfArray = typeof(Array);
				}
				return WellKnownTypes.typeOfArray;
			}
		}
		internal static Type TypeOfIDictionaryGeneric
		{
			get
			{
				if (WellKnownTypes.typeOfIDictionaryGeneric == null)
				{
					WellKnownTypes.typeOfIDictionaryGeneric = typeof(IDictionary<,>);
				}
				return WellKnownTypes.typeOfIDictionaryGeneric;
			}
		}
		internal static Type TypeOfIDictionary
		{
			get
			{
				if (WellKnownTypes.typeOfIDictionary == null)
				{
					WellKnownTypes.typeOfIDictionary = typeof(IDictionary);
				}
				return WellKnownTypes.typeOfIDictionary;
			}
		}
		internal static Type TypeOfIListGeneric
		{
			get
			{
				if (WellKnownTypes.typeOfIListGeneric == null)
				{
					WellKnownTypes.typeOfIListGeneric = typeof(IList<>);
				}
				return WellKnownTypes.typeOfIListGeneric;
			}
		}
		internal static Type TypeOfIList
		{
			get
			{
				if (WellKnownTypes.typeOfIList == null)
				{
					WellKnownTypes.typeOfIList = typeof(IList);
				}
				return WellKnownTypes.typeOfIList;
			}
		}
		internal static Type TypeOfICollectionGeneric
		{
			get
			{
				if (WellKnownTypes.typeOfICollectionGeneric == null)
				{
					WellKnownTypes.typeOfICollectionGeneric = typeof(ICollection<>);
				}
				return WellKnownTypes.typeOfICollectionGeneric;
			}
		}
		internal static Type TypeOfICollection
		{
			get
			{
				if (WellKnownTypes.typeOfICollection == null)
				{
					WellKnownTypes.typeOfICollection = typeof(ICollection);
				}
				return WellKnownTypes.typeOfICollection;
			}
		}
		internal static Type TypeOfIEnumerableGeneric
		{
			get
			{
				if (WellKnownTypes.typeOfIEnumerableGeneric == null)
				{
					WellKnownTypes.typeOfIEnumerableGeneric = typeof(IEnumerable<>);
				}
				return WellKnownTypes.typeOfIEnumerableGeneric;
			}
		}
		internal static Type TypeOfIEnumerable
		{
			get
			{
				if (WellKnownTypes.typeOfIEnumerable == null)
				{
					WellKnownTypes.typeOfIEnumerable = typeof(IEnumerable);
				}
				return WellKnownTypes.typeOfIEnumerable;
			}
		}
		internal static Type TypeOfIEnumeratorGeneric
		{
			get
			{
				if (WellKnownTypes.typeOfIEnumeratorGeneric == null)
				{
					WellKnownTypes.typeOfIEnumeratorGeneric = typeof(IEnumerator<>);
				}
				return WellKnownTypes.typeOfIEnumeratorGeneric;
			}
		}
		internal static Type TypeOfIEnumerator
		{
			get
			{
				if (WellKnownTypes.typeOfIEnumerator == null)
				{
					WellKnownTypes.typeOfIEnumerator = typeof(IEnumerator);
				}
				return WellKnownTypes.typeOfIEnumerator;
			}
		}
		internal static Type TypeOfKeyValuePair
		{
			get
			{
				if (WellKnownTypes.typeOfKeyValuePair == null)
				{
					WellKnownTypes.typeOfKeyValuePair = typeof(KeyValuePair<,>);
				}
				return WellKnownTypes.typeOfKeyValuePair;
			}
		}
		//internal static Type TypeOfKeyValue
		//{
		//	get
		//	{
		//		if (WellKnownTypes.typeOfKeyValue == null)
		//		{
		//			WellKnownTypes.typeOfKeyValue = typeof(KeyValue<,>);
		//		}
		//		return WellKnownTypes.typeOfKeyValue;
		//	}
		//}
		internal static Type TypeOfIDictionaryEnumerator
		{
			get
			{
				if (WellKnownTypes.typeOfIDictionaryEnumerator == null)
				{
					WellKnownTypes.typeOfIDictionaryEnumerator = typeof(IDictionaryEnumerator);
				}
				return WellKnownTypes.typeOfIDictionaryEnumerator;
			}
		}
		//internal static Type TypeOfDictionaryEnumerator
		//{
		//	get
		//	{
		//		if (WellKnownTypes.typeOfDictionaryEnumerator == null)
		//		{
		//			WellKnownTypes.typeOfDictionaryEnumerator = typeof(CollectionDataContract.DictionaryEnumerator);
		//		}
		//		return WellKnownTypes.typeOfDictionaryEnumerator;
		//	}
		//}
		//internal static Type TypeOfGenericDictionaryEnumerator
		//{
		//	get
		//	{
		//		if (WellKnownTypes.typeOfGenericDictionaryEnumerator == null)
		//		{
		//			WellKnownTypes.typeOfGenericDictionaryEnumerator = typeof(CollectionDataContract.GenericDictionaryEnumerator<,>);
		//		}
		//		return WellKnownTypes.typeOfGenericDictionaryEnumerator;
		//	}
		//}
		internal static Type TypeOfDictionaryGeneric
		{
			get
			{
				if (WellKnownTypes.typeOfDictionaryGeneric == null)
				{
					WellKnownTypes.typeOfDictionaryGeneric = typeof(Dictionary<,>);
				}
				return WellKnownTypes.typeOfDictionaryGeneric;
			}
		}
		internal static Type TypeOfHashtable
		{
			get
			{
				if (WellKnownTypes.typeOfHashtable == null)
				{
					WellKnownTypes.typeOfHashtable = typeof(Hashtable);
				}
				return WellKnownTypes.typeOfHashtable;
			}
		}
		internal static Type TypeOfListGeneric
		{
			get
			{
				if (WellKnownTypes.typeOfListGeneric == null)
				{
					WellKnownTypes.typeOfListGeneric = typeof(List<>);
				}
				return WellKnownTypes.typeOfListGeneric;
			}
		}
		#endregion Properties
	}
}
