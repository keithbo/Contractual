using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Contractual
{
	internal class ClassTypeContract : TypeContract
	{
		internal static TypeContract Create(Type type)
		{
			if (!(type.IsArray || PrimitiveTypeContract.IsPrimitive(type)))
			{
				return new ClassTypeContract(type);
			}

			return null;
		}

		private static List<PropertyPairingContract> GetPropertyPairings(TypeContract source, TypeContract result)
		{
			var resultProperties = GetProperties(result.Type);
			var sourceProperties = GetProperties(source.Type);

			var list = new List<PropertyPairingContract>();
			foreach (var pair in sourceProperties)
			{
				PropertyContract resultProp;
				if (resultProperties.TryGetValue(pair.Key, out resultProp))
				{
					list.Add(Contracts.Get(pair.Value.Property, resultProp.Property));
				}
			}
			return list;
		}

		private static IDictionary<string, PropertyContract> GetProperties(Type type)
		{
			return type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
				.Where(p => p.CanRead && p.CanWrite)
				.Select(p => Contracts.Get(p))
				.ToDictionary<PropertyContract, string>(p => p.Name);
		}

		protected ClassTypeContract(Type type)
			: base(type)
		{

		}

		internal override Expression Init(ParameterExpression sourceParam, TypeContract source)
		{
			var mappedEx = GetPropertyPairings(source, this).Select(m => m.Bind(sourceParam));

			var init = Expression.MemberInit(
				Expression.New(this.Type),
				mappedEx.ToArray());

			return init;
		}

	}
}
