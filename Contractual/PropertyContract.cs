namespace Contractual
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Reflection;
	using System.Text;
	using System.Threading.Tasks;

	internal abstract class PropertyContract
	{
		private static IDictionary<PropertyInfo, PropertyContract> _propertyContractCache;

		static PropertyContract()
		{
			_propertyContractCache = new Dictionary<PropertyInfo, PropertyContract>();
		}

		internal static PropertyContract GetContract(PropertyInfo property)
		{
			PropertyContract contract;
			if (_propertyContractCache.TryGetValue(property, out contract))
			{
				return contract;
			}

			TypeContract propertyType = TypeContract.GetContract(property.PropertyType);
			contract = SimplePropertyContract.Create(property, propertyType) ??
				ComplexPropertyContract.Create(property, propertyType);
			if (contract == null)
			{
				throw new ArgumentException();
			}
			_propertyContractCache.Add(property, contract);

			return contract;
		}

		private PropertyInfo _property;
		private TypeContract _typeContract;

		public PropertyInfo Property
		{
			get { return _property; }
		}

		public TypeContract TypeContract
		{
			get { return _typeContract; }
		}

		public string Name
		{
			get { return _property.Name; }
		}

		protected PropertyContract(PropertyInfo property, TypeContract propertyType)
		{
			_property = property;
			_typeContract = propertyType;
		}

		public abstract MemberBinding Set(ParameterExpression param, PropertyContract source);
	}
}
