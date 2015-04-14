using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Utterance
{
	public interface ILinqAccess
	{
		LambdaExpression SelectSourceSelector { get; }

		LambdaExpression ToArraySource { get; }
	}

	public interface ILinqAccess<TSource, TResult>
	{
		Expression<Func<IEnumerable<TSource>, Func<TSource, TResult>, IEnumerable<TResult>>> SelectSourceSelector { get; }

		Expression<Func<IEnumerable<TResult>, TResult[]>> ToArraySource { get; }
	}
}
