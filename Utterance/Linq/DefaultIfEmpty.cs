using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Utterance.Linq
{
	public static class DefaultIfEmpty
	{
		internal class Access<TSource>
		{
			public LambdaExpression Source { get { return DefaultIfEmpty.Cache<TSource>.Source; } }
			public LambdaExpression SourceDefaultValue { get { return DefaultIfEmpty.Cache<TSource>.SourceDefaultValue; } }
		}
		public static class Cache<TSource>
		{
			public static readonly Expression<Func<IEnumerable<TSource>, IEnumerable<TSource>>> Source = (source) => source.DefaultIfEmpty();
			public static readonly Expression<Func<IEnumerable<TSource>, TSource, IEnumerable<TSource>>> SourceDefaultValue = (source, defaultValue) => source.DefaultIfEmpty(defaultValue);
		}
	}
}
