using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contractual
{
	internal class ArrayTypeContract : TypeContract
	{
		internal static TypeContract Create(Type type)
		{
			if (type.IsArray)
			{
				Type innerType;
				if (type == WellKnownTypes.TypeOfArray)
				{
					innerType = WellKnownTypes.TypeOfObject;
				}
				else
				{
					innerType = type.GetElementType();
				}
				return new ArrayTypeContract(type, TypeContract.GetContract(innerType));
			}

			return null;
		}

		private readonly TypeContract _innerType;

		protected ArrayTypeContract(Type type, TypeContract innerType)
			: base(type)
		{
			_innerType = innerType;
		}

		internal override Expression Init(ParameterExpression sourceParam, TypeContract source)
		{
			ArrayTypeContract sourceArray = source as ArrayTypeContract;

			var sourceType = sourceArray._innerType.Type;
			var resultType = _innerType.Type;
			var innerPairing = TypePairingContract.GetContract(sourceType, resultType);
			var innerConvert = innerPairing.Convert();

			////var selectResultType = typeof(IEnumerable<>).MakeGenericType(resultType);
			//MethodCallExpression selectExp = Contractual.Linq.Select(sourceType, resultType, sourceParam, innerConvert);
			//MethodCallExpression toArrayExp = Contractual.Linq.ToArray(resultType, selectExp);
			//return toArrayExp;

			var convertParam = Expression.Parameter(typeof(Func<,>).MakeGenericType(sourceType, resultType));
			var select = innerPairing.LinqAccess.SelectSourceSelector;
			var toarray = innerPairing.LinqAccess.ToArraySource;

			var compose = select.Compose(toarray, sourceParam, convertParam);
			var invoke = Expression.Invoke(compose, sourceParam, innerConvert);
			return invoke;
		}
	}
}
