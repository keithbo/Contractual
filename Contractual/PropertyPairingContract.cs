using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Contractual
{
	internal class PropertyPairingContract
	{
		private static IDictionary<Tuple<PropertyInfo, PropertyInfo>, PropertyPairingContract> _propertyPairingContractCache;

		static PropertyPairingContract()
		{
			_propertyPairingContractCache = new Dictionary<Tuple<PropertyInfo, PropertyInfo>, PropertyPairingContract>();
		}

		internal static PropertyPairingContract GetContract(PropertyInfo sourceProperty, PropertyInfo resultProperty)
		{
			var key = Tuple.Create(sourceProperty, resultProperty);
			PropertyPairingContract contract;
			if (_propertyPairingContractCache.TryGetValue(key, out contract))
			{
				return contract;
			}

			contract = new PropertyPairingContract(PropertyContract.GetContract(sourceProperty), PropertyContract.GetContract(resultProperty));
			_propertyPairingContractCache.Add(key, contract);

			return contract;
		}

		private PropertyContract _source;
		private PropertyContract _result;

		protected PropertyPairingContract(PropertyContract source, PropertyContract result)
		{
			_source = source;
			_result = result;
		}

		public MemberBinding Bind(ParameterExpression param)
		{
			return _result.Set(param, _source);
		}
	}
}
