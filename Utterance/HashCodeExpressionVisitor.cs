namespace Utterance
{
	using System;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Threading;

	public class HashCodeExpressionVisitor : ExpressionVisitor
	{
		private int _visitCount = 0;
		private readonly FNV1AHash _hash;

		public int HashCode
		{
			get { return _hash.Value; }
		}

		public HashCodeExpressionVisitor()
		{
			_hash = new FNV1AHash();
		}

		public override Expression Visit(Expression node)
		{
			try
			{
				// first visit on this HashCode visitor will reset the base hashcode
				if (Interlocked.Increment(ref _visitCount) == 1)
				{
					Reset();
				}

				return base.Visit(node);
			}
			finally
			{
				Interlocked.Decrement(ref _visitCount);
			}
		}

		protected void Reset()
		{
			_hash.Reset();
		}

		protected void HashStep(params IConvertible[] steps)
		{
			_hash.Step(steps.Select(s => Convert.ToInt32(s)).ToArray());
		}

		protected override Expression VisitBinary(BinaryExpression node)
		{
			HashStep(node.NodeType, node.IsLifted, node.IsLiftedToNull, node.Method.GetHashCode());
			return base.VisitBinary(node);
		}

		protected override Expression VisitBlock(BlockExpression node)
		{
			HashStep(node.NodeType);
			return base.VisitBlock(node);
		}

		protected override CatchBlock VisitCatchBlock(CatchBlock node)
		{
			HashStep(node.GetType().GetHashCode(), SafeHashCode(node.Test));
			return base.VisitCatchBlock(node);
		}

		protected override Expression VisitConditional(ConditionalExpression node)
		{
			HashStep(node.NodeType);
			return base.VisitConditional(node);
		}

		protected override Expression VisitConstant(ConstantExpression node)
		{
			HashStep(node.NodeType, SafeHashCode(node.Value));
			return base.VisitConstant(node);
		}

		protected override Expression VisitDebugInfo(DebugInfoExpression node)
		{
			HashStep(node.NodeType, SafeHashCode(node.Document));
			return base.VisitDebugInfo(node);
		}

		protected override Expression VisitDefault(DefaultExpression node)
		{
			HashStep(node.NodeType);
			return base.VisitDefault(node);
		}

		protected override Expression VisitDynamic(DynamicExpression node)
		{
			HashStep(node.NodeType);
			return base.VisitDynamic(node);
		}

		protected override ElementInit VisitElementInit(ElementInit node)
		{
			HashStep(SafeHashCode(node.AddMethod));
			return base.VisitElementInit(node);
		}

		protected override Expression VisitExtension(Expression node)
		{
			HashStep(node.NodeType);
			return base.VisitExtension(node);
		}

		protected override Expression VisitGoto(GotoExpression node)
		{
			HashStep(node.NodeType, node.Kind);
			return base.VisitGoto(node);
		}

		protected override Expression VisitIndex(IndexExpression node)
		{
			HashStep(node.NodeType, SafeHashCode(node.Indexer));
			return base.VisitIndex(node);
		}

		protected override Expression VisitInvocation(InvocationExpression node)
		{
			HashStep(node.NodeType);
			return base.VisitInvocation(node);
		}

		protected override Expression VisitLabel(LabelExpression node)
		{
			HashStep(node.NodeType);
			return base.VisitLabel(node);
		}

		protected override LabelTarget VisitLabelTarget(LabelTarget node)
		{
			HashStep(SafeHashCode(node.Type), SafeHashCode(node.Name));
			return base.VisitLabelTarget(node);
		}

		protected override Expression VisitLambda<T>(Expression<T> node)
		{
			HashStep(node.NodeType, SafeHashCode(node.Name), SafeHashCode(node.ReturnType));
			return base.VisitLambda<T>(node);
		}

		protected override Expression VisitListInit(ListInitExpression node)
		{
			HashStep(node.NodeType);
			return base.VisitListInit(node);
		}

		protected override Expression VisitLoop(LoopExpression node)
		{
			HashStep(node.NodeType);
			return base.VisitLoop(node);
		}

		protected override Expression VisitMember(MemberExpression node)
		{
			HashStep(node.NodeType, SafeHashCode(node.Member));
			return base.VisitMember(node);
		}

		protected override MemberAssignment VisitMemberAssignment(MemberAssignment node)
		{
			HashStep(node.BindingType, SafeHashCode(node.Member));
			return base.VisitMemberAssignment(node);
		}

		protected override MemberBinding VisitMemberBinding(MemberBinding node)
		{
			HashStep(node.BindingType, SafeHashCode(node.Member));
			return base.VisitMemberBinding(node);
		}

		protected override Expression VisitMemberInit(MemberInitExpression node)
		{
			HashStep(node.NodeType);
			return base.VisitMemberInit(node);
		}

		protected override MemberListBinding VisitMemberListBinding(MemberListBinding node)
		{
			HashStep(node.BindingType, SafeHashCode(node.Member));
			return base.VisitMemberListBinding(node);
		}

		protected override MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding node)
		{
			HashStep(node.BindingType, SafeHashCode(node.Member));
			return base.VisitMemberMemberBinding(node);
		}

		protected override Expression VisitMethodCall(MethodCallExpression node)
		{
			HashStep(node.NodeType, SafeHashCode(node.Method));
			return base.VisitMethodCall(node);
		}

		protected override Expression VisitNew(NewExpression node)
		{
			HashStep(node.NodeType, SafeHashCode(node.Constructor));
			if (node.Members.Count > 0)
			{
				HashStep(node.Members.Select(m => SafeHashCode(m)).Cast<IConvertible>().ToArray());
			}
			return base.VisitNew(node);
		}

		protected override Expression VisitNewArray(NewArrayExpression node)
		{
			HashStep(node.NodeType);
			return base.VisitNewArray(node);
		}

		protected override Expression VisitParameter(ParameterExpression node)
		{
			HashStep(node.NodeType, SafeHashCode(node.Type), SafeHashCode(node.Name), node.IsByRef);
			return base.VisitParameter(node);
		}

		protected override Expression VisitRuntimeVariables(RuntimeVariablesExpression node)
		{
			HashStep(node.NodeType);
			return base.VisitRuntimeVariables(node);
		}

		protected override Expression VisitSwitch(SwitchExpression node)
		{
			HashStep(node.NodeType, SafeHashCode(node.Comparison));
			return base.VisitSwitch(node);
		}

		protected override SwitchCase VisitSwitchCase(SwitchCase node)
		{
			HashStep(SafeHashCode(node.GetType()));
			return base.VisitSwitchCase(node);
		}

		protected override Expression VisitTry(TryExpression node)
		{
			HashStep(node.NodeType);
			return base.VisitTry(node);
		}

		protected override Expression VisitTypeBinary(TypeBinaryExpression node)
		{
			HashStep(node.NodeType, SafeHashCode(node.TypeOperand));
			return base.VisitTypeBinary(node);
		}

		protected override Expression VisitUnary(UnaryExpression node)
		{
			HashStep(node.NodeType, node.IsLifted, node.IsLiftedToNull, SafeHashCode(node.Method));
			return base.VisitUnary(node);
		}

		protected static int SafeHashCode(object o)
		{
			return o != null ? o.GetHashCode() : 31;
		}

	}
}
