namespace Utterance
{
	using System.Collections.Generic;
	using System.Linq.Expressions;

	public interface IExpressionArchive
	{
		void Add(Expression expression);
	}
	public class QueueExpressionArchive : IExpressionArchive
	{
		public Queue<Expression> Queue
		{
			get;
			private set;
		}

		public QueueExpressionArchive()
		{
			Queue = new Queue<Expression>();
		}

		public void Add(Expression expression)
		{
			Queue.Enqueue(expression);
		}
	}
}
