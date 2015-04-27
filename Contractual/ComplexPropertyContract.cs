using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Contractual
{
	internal class ComplexPropertyContract : PropertyContract
	{
		protected ComplexPropertyContract(PropertyInfo property, TypeContract contract)
			: base(property, contract)
		{
		}

		public override MemberBinding Set(ParameterExpression param, PropertyContract source)
		{
			return Expression.Bind(Property, DownstreamInvoke(param, source));
		}

		private Expression DownstreamInvoke(ParameterExpression param, PropertyContract source)
		{
			// [PropertyAccessorLambda].Compose([SourceToResultConverterLambda])
			var sourceAccessor = Expression.Lambda(param, param);
			var pairedTypeContract = TypePairingContract.GetContract(source.TypeContract.Type, TypeContract.Type);
			var downstreamConverter = Helpers.Compose(sourceAccessor, pairedTypeContract.Convert(), source.TypeContract.Parameter());
			return Expression.Invoke(downstreamConverter, Expression.MakeMemberAccess(param, source.Property));
		}

		internal static PropertyContract Create(PropertyInfo property, TypeContract typeContract)
		{
			if (!typeContract.IsSimple)
			{
				return new ComplexPropertyContract(property, typeContract);
			}

			return null;
		}
	}
}
