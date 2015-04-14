namespace Contractual
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.ComponentModel;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Reflection;
	using System.Text;
	using System.Threading.Tasks;

	internal class TypePairingContract
	{
		private static IDictionary<Tuple<Type, Type>, TypePairingContract> _typePairingContractCache;

		static TypePairingContract()
		{
			_typePairingContractCache = new Dictionary<Tuple<Type, Type>, TypePairingContract>();
		}

		internal static TypePairingContract GetContract(Type source, Type result)
		{
			var key = Tuple.Create(source, result);
			TypePairingContract contract;
			if (_typePairingContractCache.TryGetValue(key, out contract))
			{
				return contract;
			}

			contract = new TypePairingContract(TypeContract.GetContract(source), TypeContract.GetContract(result));
			_typePairingContractCache.Add(key, contract);

			return contract;
		}

		private TypeContract _source;
		private TypeContract _result;
		private ILinqAccess _linqAccess;

		public TypeContract Source
		{
			get { return _source; }
		}

		public TypeContract Result
		{
			get { return _result; }
		}

		public ILinqAccess LinqAccess
		{
			get
			{
				if (_linqAccess == null)
				{
					_linqAccess = null;//Linq.Factory.Create(_source.Type, _result.Type);
				}
				return _linqAccess;
			}
		}

		protected TypePairingContract(TypeContract source, TypeContract result)
		{
			_source = source;
			_result = result;
		}

		public LambdaExpression Convert()
		{
			var param = _source.Parameter();
			return Expression.Lambda(_result.Init(param, _source), param);
		}

		public object Invoke(object source)
		{
			return Convert().Compile().DynamicInvoke(source);
		}

		public TResult Invoke<TSource, TResult>(TSource source)
		{
			return ((Expression<Func<TSource, TResult>>)Convert()).Compile()(source);
		}
	}
}
