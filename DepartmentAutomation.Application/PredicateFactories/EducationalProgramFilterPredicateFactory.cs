using System;
using System.Linq;
using System.Linq.Expressions;
using DepartmentAutomation.Application.Common.Extensions;
using DepartmentAutomation.Application.Contracts.Requests.Filters;
using DepartmentAutomation.Domain.Entities;
using DepartmentAutomation.Domain.Enums;

namespace DepartmentAutomation.Application.PredicateFactories
{
    public class
        EducationalProgramFilterPredicateFactory : IExpressionBuilder<EducationalProgram, EducationalProgramsFilterDto>
    {
        public Expression<Func<EducationalProgram, bool>> Build(EducationalProgramsFilterDto expressionInfo)
        {
            var expression = PredicateBuilder.True<EducationalProgram>();

            if (!string.IsNullOrEmpty(expressionInfo.DisciplineName))
            {
                expression = expression.And(_ =>
                    _.Discipline.Name.ToLower().Contains(expressionInfo.DisciplineName.ToLower()));
            }

            expression = expression.And(_ =>
                _.Discipline.Teachers.Select(teacher => teacher.ApplicationUserId).Contains(expressionInfo.UserId));

            expression = expression.And(_ => _ != null);
            
            expression = expression.And(_ => _.Discipline.Status != Status.NotExist);

            return expression;
        }
    }
}