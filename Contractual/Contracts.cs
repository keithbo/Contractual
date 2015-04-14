namespace Contractual
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using System.Text;
	using System.Threading.Tasks;

	internal static class Contracts
	{
		public static TypeContract Get<T>(Action<ITypeConfiguration> configure = null)
		{
			return Get(typeof(T), configure);
		}

		public static TypeContract Get(Type type, Action<ITypeConfiguration> configure = null)
		{
			TypeContract contract = TypeContract.GetContract(type);
			if (configure != null)
			{
				configure(contract);
			}

			return contract;
		}

		public static TypePairingContract Get<TSource, TResult>()
		{
			return Get(typeof(TSource), typeof(TResult));
		}

		public static TypePairingContract Get(Type sourceType, Type resultType)
		{
			TypePairingContract contract = TypePairingContract.GetContract(sourceType, resultType);

			return contract;
		}

		public static PropertyContract Get(PropertyInfo property)
		{
			PropertyContract contract = PropertyContract.GetContract(property);

			return contract;
		}

		public static PropertyPairingContract Get(PropertyInfo sourceProperty, PropertyInfo resultProperty)
		{
			PropertyPairingContract contract = PropertyPairingContract.GetContract(sourceProperty, resultProperty);

			return contract;
		}

		//public static bool Exists<T>()
		//{
		//	return Exists(typeof(T));
		//}

		//public static bool Exists(Type type)
		//{
		//	return _types.ContainsKey(type);
		//}

		//public static bool Exists<TSource, TResult>()
		//{
		//	return Exists(typeof(TSource), typeof(TResult));
		//}

		//public static bool Exists(Type sourceType, Type resultType)
		//{
		//	var key = Tuple.Create(sourceType, resultType);
		//	return _typePairs.ContainsKey(key);
		//}

		//public static bool Exists(PropertyInfo property)
		//{
		//	return _properties.ContainsKey(property);
		//}

	}
}
