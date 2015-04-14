using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Utterance.Linq
{
	public static class Min
	{
		internal class Access
		{
			public LambdaExpression IntSource { get { return Cache.IntSource; } }
			public LambdaExpression NullableIntSource { get { return Cache.NullableIntSource; } }
			public LambdaExpression LongSource { get { return Cache.LongSource; } }
			public LambdaExpression NullableLongSource { get { return Cache.NullableLongSource; } }
			public LambdaExpression FloatSource { get { return Cache.FloatSource; } }
			public LambdaExpression NullableFloatSource { get { return Cache.NullableFloatSource; } }
			public LambdaExpression DoubleSource { get { return Cache.DoubleSource; } }
			public LambdaExpression NullableDoubleSource { get { return Cache.NullableDoubleSource; } }
			public LambdaExpression DecimalSource { get { return Cache.DecimalSource; } }
			public LambdaExpression NullableDecimalSource { get { return Cache.NullableDecimalSource; } }
		}
		internal class Access<TSource>
		{
			public LambdaExpression Source { get { return Cache<TSource>.Source; } }
			public LambdaExpression IntSourceSelector { get { return Cache<TSource>.IntSourceSelector; } }
			public LambdaExpression NullableIntSourceSelector { get { return Cache<TSource>.NullableIntSourceSelector; } }
			public LambdaExpression LongSourceSelector { get { return Cache<TSource>.LongSourceSelector; } }
			public LambdaExpression NullableLongSourceSelector { get { return Cache<TSource>.NullableLongSourceSelector; } }
			public LambdaExpression FloatSourceSelector { get { return Cache<TSource>.FloatSourceSelector; } }
			public LambdaExpression NullableFloatSourceSelector { get { return Cache<TSource>.NullableFloatSourceSelector; } }
			public LambdaExpression DoubleSourceSelector { get { return Cache<TSource>.DoubleSourceSelector; } }
			public LambdaExpression NullableDoubleSourceSelector { get { return Cache<TSource>.NullableDoubleSourceSelector; } }
			public LambdaExpression DecimalSourceSelector { get { return Cache<TSource>.DecimalSourceSelector; } }
			public LambdaExpression NullableDecimalSourceSelector { get { return Cache<TSource>.NullableDecimalSourceSelector; } }
		}
		internal class Access<TSource, TResult>
		{
			public LambdaExpression SourceSelector { get { return Cache<TSource, TResult>.SourceSelector; } }
		}
		public static class Cache
		{
			public static readonly Expression<Func<IEnumerable<int>, int>> IntSource = (source) => source.Min();
			public static readonly Expression<Func<IEnumerable<int?>, int?>> NullableIntSource = (source) => source.Min();
			public static readonly Expression<Func<IEnumerable<long>, long>> LongSource = (source) => source.Min();
			public static readonly Expression<Func<IEnumerable<long?>, long?>> NullableLongSource = (source) => source.Min();
			public static readonly Expression<Func<IEnumerable<float>, float>> FloatSource = (source) => source.Min();
			public static readonly Expression<Func<IEnumerable<float?>, float?>> NullableFloatSource = (source) => source.Min();
			public static readonly Expression<Func<IEnumerable<double>, double>> DoubleSource = (source) => source.Min();
			public static readonly Expression<Func<IEnumerable<double?>, double?>> NullableDoubleSource = (source) => source.Min();
			public static readonly Expression<Func<IEnumerable<decimal>, decimal>> DecimalSource = (source) => source.Min();
			public static readonly Expression<Func<IEnumerable<decimal?>, decimal?>> NullableDecimalSource = (source) => source.Min();
		}
		public static class Cache<TSource>
		{
			public static readonly Expression<Func<IEnumerable<TSource>, TSource>> Source = (source) => source.Min();
			public static readonly Expression<Func<IEnumerable<TSource>, Func<TSource, int>, int>> IntSourceSelector = (source, selector) => source.Min(selector);
			public static readonly Expression<Func<IEnumerable<TSource>, Func<TSource, int?>, int?>> NullableIntSourceSelector = (source, selector) => source.Min(selector);
			public static readonly Expression<Func<IEnumerable<TSource>, Func<TSource, long>, long>> LongSourceSelector = (source, selector) => source.Min(selector);
			public static readonly Expression<Func<IEnumerable<TSource>, Func<TSource, long?>, long?>> NullableLongSourceSelector = (source, selector) => source.Min(selector);
			public static readonly Expression<Func<IEnumerable<TSource>, Func<TSource, float>, float>> FloatSourceSelector = (source, selector) => source.Min(selector);
			public static readonly Expression<Func<IEnumerable<TSource>, Func<TSource, float?>, float?>> NullableFloatSourceSelector = (source, selector) => source.Min(selector);
			public static readonly Expression<Func<IEnumerable<TSource>, Func<TSource, double>, double>> DoubleSourceSelector = (source, selector) => source.Min(selector);
			public static readonly Expression<Func<IEnumerable<TSource>, Func<TSource, double?>, double?>> NullableDoubleSourceSelector = (source, selector) => source.Min(selector);
			public static readonly Expression<Func<IEnumerable<TSource>, Func<TSource, decimal>, decimal>> DecimalSourceSelector = (source, selector) => source.Min(selector);
			public static readonly Expression<Func<IEnumerable<TSource>, Func<TSource, decimal?>, decimal?>> NullableDecimalSourceSelector = (source, selector) => source.Min(selector);
		}
		public static class Cache<TSource, TResult>
		{
			public static readonly Expression<Func<IEnumerable<TSource>, Func<TSource, TResult>, TResult>> SourceSelector = (source, selector) => source.Min(selector);
		}
	}
}
