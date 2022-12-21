using System;
using System.Linq.Expressions;

namespace DepartmentAutomation.Application.PredicateFactories
{
    public interface IExpressionBuilder<T, in P>
    {
        Expression<Func<T, bool>> Build(P expressionInfo);
    }
}