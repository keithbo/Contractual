namespace Utterance
{
	using System.Collections.Generic;
	using System.Linq.Expressions;

	public class ExpressionEnumerator : IEnumerator<Expression>
	{
		private Visitor _visitor;
		private Expression _source;

		public Expression Current
		{
			get;
			private set;
		}

		public ExpressionEnumerator(Expression source)
		{
			_visitor = new Visitor();
			_source = source;
			_visitor.Nodes.Push(_source);
		}

		public void Dispose()
		{
			_visitor = null;
			_source = null;
			Current = null;
		}

		object System.Collections.IEnumerator.Current
		{
			get { return Current; }
		}

		public bool MoveNext()
		{
			if (Current != null)
			{
				// explicitly use the underlying Visit behavior
				_visitor.Step(Current);
			}
			Current = _visitor.Nodes.Count > 0 ? _visitor.Nodes.Pop() : null;
			return Current != null;
		}

		public void Reset()
		{
			_visitor.Nodes.Clear();
			_visitor.Nodes.Push(_source);
			Current = null;
		}

		private class Visitor : ExpressionVisitor
		{
			public Stack<Expression> Nodes { get; private set; }

			public Visitor()
			{
				Nodes = new Stack<Expression>();
			}

			public Expression Step(Expression node)
			{
				return base.Visit(node);
			}

			public override Expression Visit(Expression node)
			{
				if (node != null)
				{
					Nodes.Push(node);
				}
				return node;
			}
		}
	}
}
