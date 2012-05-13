using System.Linq.Expressions;

namespace AgentMulder.ReSharper.Domain.Expressions
{
    public interface IExpressionBuilder
    {
        Expression Build();
    }
}