using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Utterance.Linq
{
	public static class Average
	{
		internal class Access
		{
			public LambdaExpression IntSource { get { return Average.Cache.IntSource; } }
			public LambdaExpression NullableIntSource { get { return Average.Cache.NullableIntSource; } }
			public LambdaExpression LongSource { get { return Average.Cache.LongSource; } }
			public LambdaExpression NullableLongSource { get { return Average.Cache.NullableLongSource; } }
			public LambdaExpression FloatSource { get { return Average.Cache.FloatSource; } }
			public LambdaExpression NullableFloatSource { get { return Average.Cache.NullableFloatSource; } }
			public LambdaExpression DoubleSource { get { return Average.Cache.DoubleSource; } }
			public LambdaExpression NullableDoubleSource { get { return Average.Cache.NullableDoubleSource; } }
			public LambdaExpression DecimalSource { get { return Average.Cache.DecimalSource; } }
			public LambdaExpression NullableDecimalSource { get { return Average.Cache.NullableDecimalSource; } }
		}
		internal class Access<TSource>
		{
			public LambdaExpression IntSourceSelector { get { return Average.Cache<TSource>.IntSourceSelector; } }
			public LambdaExpression NullableIntSourceSelector { get { return Average.Cache<TSource>.NullableIntSourceSelector; } }
			public LambdaExpression LongSourceSelector { get { return Average.Cache<TSource>.LongSourceSelector; } }
			public LambdaExpression NullableLongSourceSelector { get { return Average.Cache<TSource>.NullableLongSourceSelector; } }
			public LambdaExpression FloatSourceSelector { get { return Average.Cache<TSource>.FloatSourceSelector; } }
			public LambdaExpression NullableFloatSourceSelector { get { return Average.Cache<TSource>.NullableFloatSourceSelector; } }
			public LambdaExpression DoubleSourceSelector { get { return Average.Cache<TSource>.DoubleSourceSelector; } }
			public LambdaExpression NullableDoubleSourceSelector { get { return Average.Cache<TSource>.NullableDoubleSourceSelector; } }
			public LambdaExpression DecimalSourceSelector { get { return Average.Cache<TSource>.DecimalSourceSelector; } }
			public LambdaExpression NullableDecimalSourceSelector { get { return Average.Cache<TSource>.NullableDecimalSourceSelector; } }
		}
		public static class Cache
		{
			public static readonly Expression<Func<IEnumerable<int>, double>> IntSource = (source) => source.Average();
			public static readonly Expression<Func<IEnumerable<int?>, double?>> NullableIntSource = (source) => source.Average();
			public static readonly Expression<Func<IEnumerable<long>, double>> LongSource = (source) => source.Average();
			public static readonly Expression<Func<IEnumerable<long?>, double?>> NullableLongSource = (source) => source.Average();
			public static readonly Expression<Func<IEnumerable<float>, float>> FloatSource = (source) => source.Average();
			public static readonly Expression<Func<IEnumerable<float?>, float?>> NullableFloatSource = (source) => source.Average();
			public static readonly Expression<Func<IEnumerable<double>, double>> DoubleSource = (source) => source.Average();
			public static readonly Expression<Func<IEnumerable<double?>, double?>> NullableDoubleSource = (source) => source.Average();
			public static readonly Expression<Func<IEnumerable<decimal>, decimal>> DecimalSource = (source) => source.Average();
			public static readonly Expression<Func<IEnumerable<decimal?>, decimal?>> NullableDecimalSource = (source) => source.Average();
		}
		public static class Cache<TSource>
		{
			public static readonly Expression<Func<IEnumerable<TSource>, Func<TSource, int>, double>> IntSourceSelector = (source, selector) => source.Average(selector);
			public static readonly Expression<Func<IEnumerable<TSource>, Func<TSource, int?>, double?>> NullableIntSourceSelector = (source, selector) => source.Average(selector);
			public static readonly Expression<Func<IEnumerable<TSource>, Func<TSource, long>, double>> LongSourceSelector = (source, selector) => source.Average(selector);
			public static readonly Expression<Func<IEnumerable<TSource>, Func<TSource, long?>, double?>> NullableLongSourceSelector = (source, selector) => source.Average(selector);
			public static readonly Expression<Func<IEnumerable<TSource>, Func<TSource, float>, float>> FloatSourceSelector = (source, selector) => source.Average(selector);
			public static readonly Expression<Func<IEnumerable<TSource>, Func<TSource, float?>, float?>> NullableFloatSourceSelector = (source, selector) => source.Average(selector);
			public static readonly Expression<Func<IEnumerable<TSource>, Func<TSource, double>, double>> DoubleSourceSelector = (source, selector) => source.Average(selector);
			public static readonly Expression<Func<IEnumerable<TSource>, Func<TSource, double?>, double?>> NullableDoubleSourceSelector = (source, selector) => source.Average(selector);
			public static readonly Expression<Func<IEnumerable<TSource>, Func<TSource, decimal>, decimal>> DecimalSourceSelector = (source, selector) => source.Average(selector);
			public static readonly Expression<Func<IEnumerable<TSource>, Func<TSource, decimal?>, decimal?>> NullableDecimalSourceSelector = (source, selector) => source.Average(selector);
		}
	}
}
