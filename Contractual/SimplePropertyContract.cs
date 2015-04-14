using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Contractual
{
	internal class SimplePropertyContract : PropertyContract
	{
		protected SimplePropertyContract(PropertyInfo property, TypeContract contract)
			: base(property, contract)
		{

		}

		public override MemberBinding Set(ParameterExpression param, PropertyContract source)
		{
			return Expression.Bind(Property, Expression.MakeMemberAccess(param, source.Property));
		}

		internal static PropertyContract Create(PropertyInfo property, TypeContract typeContract)
		{
			if (typeContract.IsSimple)
			{
				return new SimplePropertyContract(property, typeContract);
			}

			return null;
		}
	}
}
