using System;
using System.Linq.Expressions;
using DepartmentAutomation.Application.Common.Extensions;
using DepartmentAutomation.Application.Contracts.Requests.Filters;
using DepartmentAutomation.Application.Features.Teachers.Queries.GetTeachersWithPagination;
using DepartmentAutomation.Domain.Entities.TeacherInformation;

namespace DepartmentAutomation.Application.PredicateFactories
{
    public class TeacherPredicateFactory : IExpressionBuilder<Teacher, TeacherFilterDto>
    {
        public Expression<Func<Teacher, bool>> Build(TeacherFilterDto expressionInfo)
        {
            var expression = PredicateBuilder.True<Teacher>();
            
            if (!string.IsNullOrEmpty(expressionInfo.Surname))
            {
                expression = expression.And(_ => _.ApplicationUser.UserName.Contains(expressionInfo.Surname)
                                                 || _.ApplicationUser.Surname.Contains(expressionInfo.Surname)
                                                 || _.ApplicationUser.Patronymic.Contains(expressionInfo.Surname));
            }

            if (expressionInfo.DepartmentId is not null)
            {
                expression = expression.And(_ => _.Department.Id == expressionInfo.DepartmentId);
            }

            if (expressionInfo.PositionId is not null)
            {
                expression = expression.And(_ => _.Position.Id == expressionInfo.PositionId);
            }

            return expression;
        }
    }
}