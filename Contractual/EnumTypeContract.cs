using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contractual
{
	internal sealed class EnumTypeContract : TypeContract
	{
		protected EnumTypeContract(Type type)
			: base(type)
		{

		}

		internal static TypeContract Create(Type type)
		{
			if (type.IsEnum)
			{
				return new EnumTypeContract(type);
			}

			return null;
		}

		internal override Expression Init(ParameterExpression sourceParam, TypeContract source)
		{
			return sourceParam;
		}
	}
}
