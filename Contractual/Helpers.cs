using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contractual
{
	internal static class Helpers
	{
		public static LambdaExpression Compose(LambdaExpression first, LambdaExpression second, params ParameterExpression[] parameters)
		{
			Stack<Expression> paramStack = new Stack<Expression>(parameters.Reverse().Cast<Expression>());

			if (first.Parameters.Count > paramStack.Count)
			{
				throw new InvalidOperationException();
			}

			var pairs = first.Parameters.ToDictionary(p => (Expression)p, p => paramStack.Pop());
			var newFirst = ReplaceAll(first.Body, pairs);
			paramStack.Push(newFirst);

			if (second.Parameters.Count != paramStack.Count)
			{
				throw new InvalidOperationException();
			}

			pairs = second.Parameters.ToDictionary(p => (Expression)p, p => paramStack.Pop());

			var newSecond = ReplaceAll(second.Body, pairs);

			return Expression.Lambda(newSecond, parameters);
		}

		public static Expression ReplaceAll(Expression expression, IDictionary<Expression, Expression> pairs)
		{
			return new ReplaceExpressionVisitor(pairs).Visit(expression);
		}
	}

	internal class ReplaceExpressionVisitor : ExpressionVisitor
	{
		private IDictionary<Expression, Expression> _pairs;

		public ReplaceExpressionVisitor(IDictionary<Expression, Expression> pairs)
		{
			_pairs = pairs.ToDictionary(p => p.Key, p => p.Value);
		}

		public override Expression Visit(Expression node)
		{
			Expression to;
			return (node != null && _pairs.TryGetValue(node, out to)) ? to : base.Visit(node);
		}
	}
}
