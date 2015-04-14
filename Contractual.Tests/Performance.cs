using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Contractual.Tests
{
	public class Performance
	{
		[Fact]
		public void PerformanceActivatorTest()
		{
			int n = 1000000;
			Stopwatch watch = new Stopwatch();
			watch.Start();

			for (int i = 0; i < n; i++)
			{
				//Activator.CreateInstance(typeof(Linq.LinqAccess<,>).MakeGenericType(typeof(string), typeof(string)));
			}

			watch.Stop();

			var averageMs = watch.ElapsedMilliseconds / (decimal)n;
			var averageTicks = watch.ElapsedTicks / (decimal)n;
			Logger logger = LogManager.GetLogger("MyLog");
			logger.Info("Activator (ms) => Total {0} Average: {1}", watch.ElapsedMilliseconds, averageMs);
			logger.Info("Activator (ticks) => Total {0} Average: {1}", watch.ElapsedTicks, averageTicks);
		}

		[Fact]
		public void PerformanceActivatorStaticTest()
		{
			int n = 1;
			Stopwatch watch = new Stopwatch();
			watch.Start();

			//Type type = typeof(Linq.LinqAccess<,>).MakeGenericType(typeof(string), typeof(string));

			for (int i = 0; i < n; i++)
			{
				//Activator.CreateInstance(type);
			}

			watch.Stop();

			var averageMs = watch.ElapsedMilliseconds / (decimal)n;
			var averageTicks = watch.ElapsedTicks / (decimal)n;
			Logger logger = LogManager.GetLogger("MyLog");
			logger.Info("Activator [static] (ms) => Total {0} Average: {1}", watch.ElapsedMilliseconds, averageMs);
			logger.Info("Activator [static] (ticks) => Total {0} Average: {1}", watch.ElapsedTicks, averageTicks);
		}

		[Fact]
		public void PerformanceInstantiateTest()
		{
			int n = 1000000;
			Stopwatch watch = new Stopwatch();
			watch.Start();

			for (int i = 0; i < n; i++)
			{
				//new Linq.LinqAccess<string, string>();
			}

			watch.Stop();

			var averageMs = watch.ElapsedMilliseconds / (decimal)n;
			var averageTicks = watch.ElapsedTicks / (decimal)n;
			Logger logger = LogManager.GetLogger("MyLog");
			logger.Info("Instance (ms) => Total {0} Average: {1}", watch.ElapsedMilliseconds, averageMs);
			logger.Info("Instance (ticks) => Total {0} Average: {1}", watch.ElapsedTicks, averageTicks);
		}

		[Fact]
		public void PerformanceMethodInvokeTest()
		{
			int n = 1000000;
			Stopwatch watch = new Stopwatch();
			watch.Start();

			for (int i = 0; i < n; i++)
			{
				//typeof(Linq.Factory).GetMethod("Create", new Type[0]).MakeGenericMethod(typeof(string), typeof(string)).Invoke(null, new object[0]);
			}

			watch.Stop();

			var averageMs = watch.ElapsedMilliseconds / (decimal)n;
			var averageTicks = watch.ElapsedTicks / (decimal)n;
			Logger logger = LogManager.GetLogger("MyLog");
			logger.Info("MethodInvoke (ms) => Total {0} Average: {1}", watch.ElapsedMilliseconds, averageMs);
			logger.Info("MethodInvoke (ticks) => Total {0} Average: {1}", watch.ElapsedTicks, averageTicks);
		}

		[Fact]
		public void PerformanceMethodInvokeStaticTest()
		{
			int n = 1000000;
			Stopwatch watch = new Stopwatch();
			watch.Start();

			//MethodInfo methodDef = typeof(Linq.Factory).GetMethod("Create", new Type[0]);
			//MethodInfo method = methodDef.MakeGenericMethod(typeof(string), typeof(string));

			for (int i = 0; i < n; i++)
			{
				//method.Invoke(null, new object[0]);
			}

			watch.Stop();

			var averageMs = watch.ElapsedMilliseconds / (decimal)n;
			var averageTicks = watch.ElapsedTicks / (decimal)n;
			Logger logger = LogManager.GetLogger("MyLog");
			logger.Info("MethodInvoke [static] (ms) => Total {0} Average: {1}", watch.ElapsedMilliseconds, averageMs);
			logger.Info("MethodInvoke [static] (ticks) => Total {0} Average: {1}", watch.ElapsedTicks, averageTicks);
		}

		[Fact]
		public void PerformanceExpressionTest()
		{
			int n = 1000000;
			Stopwatch watch = new Stopwatch();
			watch.Start();

			for (int i = 0; i < n; i++)
			{
				//Expression.Lambda(Expression.New(typeof(Linq.LinqAccess<,>).MakeGenericType(typeof(string), typeof(string)))).Compile().DynamicInvoke();
			}

			watch.Stop();

			var averageMs = watch.ElapsedMilliseconds / (decimal)n;
			var averageTicks = watch.ElapsedTicks / (decimal)n;
			Logger logger = LogManager.GetLogger("MyLog");
			logger.Info("Expression (ms) => Total {0} Average: {1}", watch.ElapsedMilliseconds, averageMs);
			logger.Info("Expression (ticks) => Total {0} Average: {1}", watch.ElapsedTicks, averageTicks);
		}

		[Fact]
		public void PerformanceExpressionStaticDynamicTest()
		{
			int n = 1000000;
			Stopwatch watch = new Stopwatch();
			watch.Start();

			//var compile = Expression.Lambda(Expression.New(typeof(Linq.LinqAccess<,>).MakeGenericType(typeof(string), typeof(string)))).Compile();

			for (int i = 0; i < n; i++)
			{
				//compile.DynamicInvoke();
			}

			watch.Stop();

			var averageMs = watch.ElapsedMilliseconds / (decimal)n;
			var averageTicks = watch.ElapsedTicks / (decimal)n;
			Logger logger = LogManager.GetLogger("MyLog");
			logger.Info("Expression [static dynamic] (ms) => Total {0} Average: {1}", watch.ElapsedMilliseconds, averageMs);
			logger.Info("Expression [static dynamic] (ticks) => Total {0} Average: {1}", watch.ElapsedTicks, averageTicks);
		}

		[Fact]
		public void PerformanceExpressionStaticTest()
		{
			int n = 1;
			Stopwatch watch = new Stopwatch();
			watch.Start();
			//var compile = Expression.Lambda<Func<ILinqAccess>>(Expression.New(typeof(Linq.LinqAccess<,>).MakeGenericType(typeof(string), typeof(string)))).Compile();

			for (int i = 0; i < n; i++)
			{
				//compile();
			}

			watch.Stop();

			var averageMs = watch.ElapsedMilliseconds / (decimal)n;
			var averageTicks = watch.ElapsedTicks / (decimal)n;
			Logger logger = LogManager.GetLogger("MyLog");
			logger.Info("Expression [static] (ms) => Total {0} Average: {1}", watch.ElapsedMilliseconds, averageMs);
			logger.Info("Expression [static] (ticks) => Total {0} Average: {1}", watch.ElapsedTicks, averageTicks);
		}
	}
}
