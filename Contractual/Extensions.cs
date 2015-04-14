using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Contractual
{
	public static class Extensions
	{
		//public static Expression<Func<TSource, TResult>> MapTo<TSource, TResult>()
		//{
		//	var sourceType = typeof(TSource);
		//	var resultType = typeof(TResult);
		//	return (Expression<Func<TSource, TResult>>)MapTo(sourceType, resultType);
		//}

		//public static LambdaExpression MapTo(Type sourceType, Type resultType)
		//{
		//	return Contracts.Get(sourceType, resultType).Lambda();
		//}

		public static TResult Convert<TSource, TResult>(TSource source)
		{
			return (TResult)Contracts.Get(typeof(TSource), typeof(TResult)).Invoke(source);
		}

		public static object Convert(object source, Type resultType)
		{
			return Contracts.Get(source.GetType(), resultType).Invoke(source);
		}

		//private static MemberInitExpression MapInit(ParameterExpression param, TypePairingContract contract)
		//{
		//	var mappedEx = contract.Mappings.Select(m => BindProperty(param, m.Item1, m.Item2));

		//	var initEx = Expression.MemberInit(
		//		contract.Result.New,
		//		mappedEx.ToArray());
		//	return initEx;
		//}

		//private static MemberBinding BindProperty(ParameterExpression parameter, PropertyInfo sourceProperty, PropertyInfo resultProperty)
		//{
		//	var sourceType = sourceProperty.PropertyType;
		//	var resultType = resultProperty.PropertyType;
		//	if (sourceType == resultType)
		//	{
		//		if (UtilityHelpers.IsSimpleType(resultType))
		//		{
		//			return Expression.Bind(resultProperty, Expression.MakeMemberAccess(parameter, sourceProperty));
		//		}
		//	}
		//	else
		//	{
		//		var sourceSimple = UtilityHelpers.IsSimpleType(sourceType);
		//		var resultSimple = UtilityHelpers.IsSimpleType(resultType);
		//		if (sourceSimple && resultSimple)
		//		{
		//			throw new Exception();
		//		}
		//		else if (sourceSimple || resultSimple)
		//		{
		//			throw new Exception();
		//		}
		//	}

		//	// [PropertyAccessorLambda].Compose([SourceToResultConverterLambda])
		//	var sourceAccessor = Expression.Lambda(parameter, parameter);
		//	var downstreamConverter = UtilityExtensions.Compose(sourceType, sourceAccessor, MapTo(sourceType, resultType));
		//	var invoke = Expression.Invoke(downstreamConverter, Expression.MakeMemberAccess(parameter, sourceProperty));
		//	return Expression.Bind(resultProperty, invoke);
		//}
	}
}
