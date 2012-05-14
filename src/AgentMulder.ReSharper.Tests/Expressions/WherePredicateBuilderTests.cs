using System;
using System.Linq.Expressions;
using AgentMulder.ReSharper.Domain.Expressions;
using NUnit.Framework;

namespace AgentMulder.ReSharper.Tests.Expressions
{
    public class WherePredicateBuilderTests : LambdaExpressionsTestsBase
    {
        [TestCase("ConstantExpression", "t => True")]
        [TestCase("MethodCallWithArguments", "t => System.Int32.IsAssignableFrom(t)")]
        [TestCase("MemberReference", "t => t.IsClass")]
        [TestCase("Complex1", "t => Not((t.IsGenericType AndAlso (t.GetGenericTypeDefinition() == System.Collections.Generic.IEnumerable`1[T])))")]
        public void DoTest(string testName, string expectedExpression)
        {
            RunTest(testName, lambdaExpression =>
            {
                Expression<Predicate<Type>> expression = WherePredicateBuilder.FromLambda<Type>(lambdaExpression);
                
                Assert.AreEqual(expectedExpression, expression.ToString());

                Assert.DoesNotThrow(() => expression.Compile());
            });
        }
    }
}