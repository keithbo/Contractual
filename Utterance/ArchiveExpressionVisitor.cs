namespace Utterance
{
	using System.Linq.Expressions;

	public class ArchiveExpressionVisitor : ArchiveExpressionVisitor<IExpressionArchive>
	{
		public ArchiveExpressionVisitor(IExpressionArchive archive)
			: base(archive)
		{
		}
	}

	public class ArchiveExpressionVisitor<TArchive> : ExpressionVisitor
		where TArchive : IExpressionArchive
	{
		private TArchive _archive;

		public TArchive Archive
		{
			get
			{
				return _archive;
			}
			set
			{
				_archive = value;
			}
		}

		public ArchiveExpressionVisitor(TArchive archive)
		{
			Archive = archive;
		}

		public override Expression Visit(Expression node)
		{
			Archive.Add(node);
			return base.Visit(node);
		}
	}
}
