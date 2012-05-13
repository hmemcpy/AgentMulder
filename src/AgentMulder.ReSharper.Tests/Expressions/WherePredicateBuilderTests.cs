using System;
using System.Linq.Expressions;
using AgentMulder.ReSharper.Domain.Expressions;
using NUnit.Framework;

namespace AgentMulder.ReSharper.Tests.Expressions
{
    public class WherePredicateBuilderTests : LambdaExpressionsTestsBase
    {
        [Test]
        public void TestMethodCallWithArguments()
        {
            RunTest("MethodCallWithArguments", lambdaExpression =>
            {
                Expression<Predicate<Type>> expression = WherePredicateBuilder.FromLambda<Type>(lambdaExpression);
            });
        }
    }
}