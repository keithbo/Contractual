namespace Contractual
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Linq.Expressions;
	using System.Reflection;
	using System.Linq;

	public interface ITypeConfiguration
	{
		/// <summary>
		/// Applies a different constructor to the type creation for the current type.
		/// If the type is being substituted the constructor must be for the substitution type
		/// instead of the base type.
		/// </summary>
		/// <param name="constructor"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		ITypeConfiguration ConstructWith(ConstructorInfo constructor, params Expression[] parameters);

		/// <summary>
		/// Applies a different type to be used when instantiating this configuration.
		/// The provided type must be assignable to the origin type for substitution to be allowed.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		ITypeConfiguration Substitute(Type type);
	}

	internal abstract class TypeContract : ITypeConfiguration
	{
		private static IDictionary<Type, TypeContract> _typeContractCache;

		static TypeContract()
		{
			_typeContractCache = new Dictionary<Type, TypeContract>();
		}

		internal static TypeContract GetContract(Type type)
		{
			TypeContract contract;
			if (_typeContractCache.TryGetValue(type, out contract))
			{
				return contract;
			}

			contract = PrimitiveTypeContract.Create(type) ??
				ArrayTypeContract.Create(type) ??
				EnumTypeContract.Create(type) ??
				CollectionTypeContract.Create(type) ??
				ClassTypeContract.Create(type);
			if (contract == null)
			{
				throw new ArgumentException();
			}
			_typeContractCache.Add(type, contract);

			return contract;
		}
		
		private Type _type;
		private Type _substitute;
		private ConstructorInfo _constructor;
		private Expression[] _constructorArgs;

		private IDictionary<string, PropertyContract> _properties;
		private bool _propertiesFlagged;
		private bool _simple;
		private bool _interface;

		internal Type Type
		{
			get { return _type; }
		}

		internal virtual bool IsSimple
		{
			get { return false; }
		}

		protected TypeContract(Type type)
		{
			_type = type;
			_simple = type.IsValueType || type.IsPrimitive || type.IsEnum || type == typeof(string);
			_interface = type.IsInterface;
			_propertiesFlagged = true;
		}

		internal abstract Expression Init(ParameterExpression sourceParam, TypeContract source);

		internal ParameterExpression Parameter()
		{
			return Expression.Parameter(_type);
		}

		ITypeConfiguration ITypeConfiguration.ConstructWith(ConstructorInfo constructor, params Expression[] arguments)
		{
			_constructor = constructor;
			_constructorArgs = arguments;

			return this;
		}

		ITypeConfiguration ITypeConfiguration.Substitute(Type type)
		{
			_substitute = type;

			return this;
		}
	}
}
