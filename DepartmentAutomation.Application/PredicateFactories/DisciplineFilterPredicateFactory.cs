using System;
using System.Linq.Expressions;
using DepartmentAutomation.Application.Common.Extensions;
using DepartmentAutomation.Application.Contracts.Requests.Filters;
using DepartmentAutomation.Domain.Entities;
using DepartmentAutomation.Domain.Enums;

namespace DepartmentAutomation.Application.PredicateFactories
{
    public class DisciplineFilterPredicateFactory : IExpressionBuilder<Discipline, DisciplinesFilterDto>
    {
        public Expression<Func<Discipline, bool>> Build(DisciplinesFilterDto expressionInfo)
        {
            var expression = PredicateBuilder.True<Discipline>();
            
            expression = expression.And(_ => _.DepartmentId == expressionInfo.DepartmentId);

            if (!string.IsNullOrEmpty(expressionInfo.DisciplineName))
            {
                expression = expression.And(_ =>
                    _.Name.ToLower().Contains(expressionInfo.DisciplineName.ToLower()));
            }

            if (expressionInfo.Status is not null)
            {
                expression = expression.And(_ => _.Status == expressionInfo.Status);
            }

            if (expressionInfo.StudyStartingYear is not null)
            {
                expression = expression.And(_ =>
                    _.Curriculum.StudyStartingYear.Year == expressionInfo.StudyStartingYear);
            }

            return expression;
        }
    }
}