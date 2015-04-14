namespace Utterance
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;

	public class ExpressionEqualityComparer : IEqualityComparer<Expression>
	{
		private static ExpressionEqualityComparer _default;
		public static IEqualityComparer<Expression> Default
		{
			get
			{
				if (_default == null)
				{
					_default = new ExpressionEqualityComparer();
				}
				return _default;
			}
		}

		private IEqualityComparer<MemberBinding> _memberBindingComparer;
		private IEqualityComparer<ElementInit> _elementInitComparer;
		private HashCodeExpressionVisitor _hashCodeVisitor;

		#region Internal EqualityComparer Instances

		protected IEqualityComparer<MemberBinding> MemberBindingComparer
		{
			get
			{
				if (_memberBindingComparer == null)
				{
					_memberBindingComparer =
						new DelegatingEqualityComparer<MemberBinding>((x, y) =>
						{
							// we are wrapping the physical call because it is a virtual method call and may change at
							// initialization
							return VisitMemberBinding(x, y);
						});
				}
				return _memberBindingComparer;
			}
		}

		protected IEqualityComparer<ElementInit> ElementInitComparer
		{
			get
			{
				if (_elementInitComparer == null)
				{
					_elementInitComparer =
						new DelegatingEqualityComparer<ElementInit>((x, y) =>
						{
							// we are wrapping the physical call because it is a virtual method call and may change at
							// initialization
							return VisitElementInit(x, y);
						});
				}
				return _elementInitComparer;
			}
		}

		protected HashCodeExpressionVisitor HashCodeVisitor
		{
			get
			{
				if (_hashCodeVisitor == null)
				{
					_hashCodeVisitor = new HashCodeExpressionVisitor();
				}
				return _hashCodeVisitor;
			}
		}

		#endregion Internal EqualityComparer Instances

		public bool RequirePropertyNameEquivalence
		{
			get;
			protected set;
		}

		public ExpressionEqualityComparer()
			: this(true)
		{
		}

		public ExpressionEqualityComparer(bool requirePropertyNameEquivalence)
		{
			RequirePropertyNameEquivalence = requirePropertyNameEquivalence;
		}

		public bool Equals(Expression x, Expression y)
		{
			if (x == y)
			{
				return true;
			}
			if (x == null || y == null)
			{
				return false;
			}

			bool equal = true;
			var xEnumerator = x.GetEnumerator();
			var yEnumerator = y.GetEnumerator();
			try
			{
				bool xNext = true;
				bool yNext = true;

				while (equal && xNext && yNext)
				{
					xNext = xEnumerator.MoveNext();
					yNext = yEnumerator.MoveNext();
					equal = !(xNext ^ yNext) && NodeEquals(xEnumerator.Current, yEnumerator.Current);
				}
			}
			finally
			{
				xEnumerator.Dispose();
				yEnumerator.Dispose();
			}
			return equal;
		}

		private bool NodeEquals(Expression x, Expression y)
		{
			return (x == null && y == null) || (x == y) || Visit(x, y);
		}

		protected virtual bool Visit(Expression x, Expression y)
		{
			if (x.NodeType != y.NodeType) return false;

			switch (x.NodeType)
			{
				//***** BinaryExpression *****//
				// Binary Arithmetic Operations
				case ExpressionType.Add:
				case ExpressionType.AddChecked:
				case ExpressionType.Divide:
				case ExpressionType.Modulo:
				case ExpressionType.Multiply:
				case ExpressionType.MultiplyChecked:
				case ExpressionType.Power:
				case ExpressionType.Subtract:
				case ExpressionType.SubtractChecked:
				// Bitwise Operations
				case ExpressionType.And:
				case ExpressionType.Or:
				case ExpressionType.ExclusiveOr:
				// Shift Operations
				case ExpressionType.LeftShift:
				case ExpressionType.RightShift:
				// Conditional Boolean Operations
				case ExpressionType.AndAlso:
				case ExpressionType.OrElse:
				// Comparison Operations
				case ExpressionType.Equal:
				case ExpressionType.NotEqual:
				case ExpressionType.GreaterThanOrEqual:
				case ExpressionType.GreaterThan:
				case ExpressionType.LessThan:
				case ExpressionType.LessThanOrEqual:
				// Coalescing Operations
				case ExpressionType.Coalesce:
				// Array Indexing Operations
				case ExpressionType.ArrayIndex:
					return VisitBinary((BinaryExpression)x, (BinaryExpression)y);

				//***** BlockExpression *****//
				case ExpressionType.Block:
					return VisitBlock((BlockExpression)x, (BlockExpression)y);

				//***** ConditionalExpression *****//
				case ExpressionType.Conditional:
					return VisitConditional((ConditionalExpression)x, (ConditionalExpression)y);

				//***** ConstantExpression *****//
				case ExpressionType.Constant:
					return VisitConstant((ConstantExpression)x, (ConstantExpression)y);

				//***** DebugInfoExpression *****//
				case ExpressionType.DebugInfo:
					return VisitDebugInfo((DebugInfoExpression)x, (DebugInfoExpression)y);

				//***** DefaultExpression *****//
				case ExpressionType.Default:
					return VisitDefault((DefaultExpression)x, (DefaultExpression)y);

				//***** DynamicExpression *****//
				case ExpressionType.Dynamic:
					return VisitDynamic((DynamicExpression)x, (DynamicExpression)y);

				//***** GotoExpression *****//
				case ExpressionType.Goto:
					return VisitGoto((GotoExpression)x, (GotoExpression)y);

				//***** IndexExpression *****//
				case ExpressionType.Index:
					return VisitIndex((IndexExpression)x, (IndexExpression)y);

				//***** InvocationExpression *****//
				case ExpressionType.Invoke:
					return VisitInvoke((InvocationExpression)x, (InvocationExpression)y);

				//***** LabelExpression *****//
				case ExpressionType.Label:
					return VisitLabel((LabelExpression)x, (LabelExpression)y);

				//***** LambdaExpression *****//
				case ExpressionType.Lambda:
					return VisitLambda((LambdaExpression)x, (LambdaExpression)y);

				//***** ListInitExpression *****//
				case ExpressionType.ListInit:
					return VisitListInit((ListInitExpression)x, (ListInitExpression)y);

				//***** LoopExpression *****//
				case ExpressionType.Loop:
					return VisitLoop((LoopExpression)x, (LoopExpression)y);

				//***** MemberExpression *****//
				case ExpressionType.MemberAccess:
					return VisitMemberAccess((MemberExpression)x, (MemberExpression)y);

				//***** MemberInitExpression *****//
				case ExpressionType.MemberInit:
					return VisitMemberInit((MemberInitExpression)x, (MemberInitExpression)y);

				//***** MethodCallExpression *****//
				case ExpressionType.Call:
					return VisitCall((MethodCallExpression)x, (MethodCallExpression)y);

				//***** NewArrayExpression *****//
				case ExpressionType.NewArrayBounds:
				case ExpressionType.NewArrayInit:
					return VisitNewArray((NewArrayExpression)x, (NewArrayExpression)y);

				//***** NewExpression *****//
				case ExpressionType.New:
					return VisitNew((NewExpression)x, (NewExpression)y);

				//***** ParameterExpression *****//
				case ExpressionType.Parameter:
					return VisitParameter((ParameterExpression)x, (ParameterExpression)y);

				//***** RuntimeVariablesExpression *****//
				case ExpressionType.RuntimeVariables:
					return VisitRuntimeVariables((RuntimeVariablesExpression)x, (RuntimeVariablesExpression)y);

				//***** SwitchExpression *****//
				case ExpressionType.Switch:
					return VisitSwitch((SwitchExpression)x, (SwitchExpression)y);

				//***** TryExpression *****//
				case ExpressionType.Try:
					return VisitTry((TryExpression)x, (TryExpression)y);

				//***** TypeBinaryExpression *****//
				case ExpressionType.TypeIs:
					return VisitTypeIs((TypeBinaryExpression)x, (TypeBinaryExpression)y);

				//***** UnaryExpression *****//
				case ExpressionType.ArrayLength:
				case ExpressionType.Convert:
				case ExpressionType.ConvertChecked:
				case ExpressionType.Negate:
				case ExpressionType.NegateChecked:
				case ExpressionType.Not:
				case ExpressionType.Quote:
				case ExpressionType.TypeAs:
				case ExpressionType.UnaryPlus:
					return VisitUnary((UnaryExpression)x, (UnaryExpression)y);
			}
			return true;
		}

		#region Visit Methods

		protected virtual bool VisitBinary(BinaryExpression x, BinaryExpression y)
		{
			return x.IsLifted == y.IsLifted &&
					x.IsLiftedToNull == y.IsLiftedToNull &&
					SameExistence(x.Conversion, y.Conversion) &&
					SameExistence(x.Left, y.Left) &&
					SameExistence(x.Right, y.Right) &&
					TypeSafeEquals(x.Method, y.Method);
		}

		protected virtual bool VisitBlock(BlockExpression x, BlockExpression y)
		{
			return x.Variables.Count == y.Variables.Count &&
					x.Expressions.Count == y.Expressions.Count &&
					SameExistence(x.Result, y.Result);
		}

		protected virtual bool VisitConditional(ConditionalExpression x, ConditionalExpression y)
		{
			// we don't care WHAT the expression is (that should come downline), just that both conditionals
			// have the same logic branching existence
			return SameExistence(x.Test, y.Test) &&
					SameExistence(x.IfTrue, y.IfTrue) &&
					SameExistence(x.IfFalse, y.IfFalse);
		}

		protected virtual bool VisitConstant(ConstantExpression x, ConstantExpression y)
		{
			return TypeSafeEquals(x.Value, y.Value);
		}

		protected virtual bool VisitDebugInfo(DebugInfoExpression x, DebugInfoExpression y)
		{
			return x.EndColumn == y.EndColumn &&
					x.EndLine == y.EndLine &&
					x.IsClear == y.IsClear &&
					x.StartColumn == y.StartColumn &&
					x.StartLine == y.StartLine &&
					TypeSafeEquals(x.Document, y.Document);
		}

		protected virtual bool VisitDefault(DefaultExpression x, DefaultExpression y)
		{
			return true;
		}

		protected virtual bool VisitDynamic(DynamicExpression x, DynamicExpression y)
		{
			return false;// TODO: how do we want to handle dynamic? Is it never equal?
		}

		protected virtual bool VisitGoto(GotoExpression x, GotoExpression y)
		{
			return VisitLabelTarget(x.Target, y.Target);
		}

		protected virtual bool VisitIndex(IndexExpression x, IndexExpression y)
		{
			return (x.Indexer == null && y.Indexer == null) || TypeSafeEquals(x.Indexer, y.Indexer);
		}

		protected virtual bool VisitInvoke(InvocationExpression x, InvocationExpression y)
		{
			return true;
		}

		protected virtual bool VisitLabel(LabelExpression x, LabelExpression y)
		{
			return VisitLabelTarget(x.Target, y.Target);
		}

		protected virtual bool VisitLambda(LambdaExpression x, LambdaExpression y)
		{
			return true;
		}

		protected virtual bool VisitListInit(ListInitExpression x, ListInitExpression y)
		{
			return x.Initializers.SequenceEqual(y.Initializers, ElementInitComparer);
		}

		protected virtual bool VisitLoop(LoopExpression x, LoopExpression y)
		{
			return SameExistence(x.Body, y.Body) &&
					VisitLabelTarget(x.BreakLabel, y.BreakLabel) &&
					VisitLabelTarget(x.ContinueLabel, y.ContinueLabel);
		}

		protected virtual bool VisitMemberAccess(MemberExpression x, MemberExpression y)
		{
			return TypeSafeEquals(x.Member, y.Member);
		}

		protected virtual bool VisitMemberInit(MemberInitExpression x, MemberInitExpression y)
		{
			return x.Bindings.SequenceEqual(y.Bindings, MemberBindingComparer);
		}

		protected virtual bool VisitCall(MethodCallExpression x, MethodCallExpression y)
		{
			return TypeSafeEquals(x.Method, y.Method);
		}

		protected virtual bool VisitNewArray(NewArrayExpression x, NewArrayExpression y)
		{
			return true;
		}

		protected virtual bool VisitNew(NewExpression x, NewExpression y)
		{
			return TypeSafeEquals(x.Constructor, y.Constructor) && x.Members.SequenceEqual(y.Members);
		}

		protected virtual bool VisitParameter(ParameterExpression x, ParameterExpression y)
		{
			return (!RequirePropertyNameEquivalence || TypeSafeEquals(x.Name, y.Name)) &&
					x.IsByRef == y.IsByRef &&
					TypeSafeEquals(x.Type, y.Type);
		}

		protected virtual bool VisitRuntimeVariables(RuntimeVariablesExpression x, RuntimeVariablesExpression y)
		{
			return true;
		}

		protected virtual bool VisitSwitch(SwitchExpression x, SwitchExpression y)
		{
			return true;
		}

		protected virtual bool VisitTry(TryExpression x, TryExpression y)
		{
			return true;
		}

		protected virtual bool VisitTypeIs(TypeBinaryExpression x, TypeBinaryExpression y)
		{
			return TypeSafeEquals(x.TypeOperand, y.TypeOperand);
		}

		protected virtual bool VisitUnary(UnaryExpression x, UnaryExpression y)
		{
			return x.IsLifted == y.IsLifted && x.IsLiftedToNull == y.IsLiftedToNull;
		}

		#endregion Visit Methods
		#region Non-Expression Visit Methods

		protected virtual bool VisitElementInit(ElementInit x, ElementInit y)
		{
			return TypeSafeEquals(x.AddMethod, y.AddMethod);
		}

		protected virtual bool VisitMemberBinding(MemberBinding x, MemberBinding y)
		{
			return x.BindingType == y.BindingType && TypeSafeEquals(x.Member, y.Member);
		}

		protected virtual bool VisitLabelTarget(LabelTarget x, LabelTarget y)
		{
			return TypeSafeEquals(x.Name, y.Name) && TypeSafeEquals(x.Type, y.Type);
		}

		#endregion Non-Expression Visit Methods
		#region Helpers

		protected static bool TypeSafeEquals<T>(T x, T y)
		{
			return EqualityComparer<T>.Default.Equals(x, y);
		}
		protected static bool SameExistence(Expression x, Expression y)
		{
			return (x != null) == (y != null);
		}

		#endregion Helpers

		public int GetHashCode(Expression obj)
		{
			HashCodeVisitor.Visit(obj);
			return HashCodeVisitor.HashCode;
		}
	}
}
