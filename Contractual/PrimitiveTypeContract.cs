using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contractual
{
	internal class PrimitiveTypeContract : TypeContract
	{
		internal static bool IsPrimitive(Type type)
		{
			return !type.IsArray && (type.IsValueType || type.IsPrimitive || type.IsEnum || type == typeof(string));
		}

		internal static TypeContract Create(Type type)
		{
			if (IsPrimitive(type))
			{
				return new PrimitiveTypeContract(type);
			}

			return null;
		}

		protected PrimitiveTypeContract(Type type)
			: base(type)
		{

		}

		internal override bool IsSimple
		{
			get { return true; }
		}

		internal override Expression Init(ParameterExpression sourceParam, TypeContract source)
		{
			return sourceParam;
		}
	}
}
