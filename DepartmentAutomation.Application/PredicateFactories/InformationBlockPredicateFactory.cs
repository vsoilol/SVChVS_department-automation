using System;
using System.Linq.Expressions;
using DepartmentAutomation.Application.Common.Extensions;
using DepartmentAutomation.Domain.Entities;
using DepartmentAutomation.Domain.Entities.InformationBlockInfo;

namespace DepartmentAutomation.Application.PredicateFactories
{
    public class InformationBlockPredicateFactory : IExpressionBuilder<InformationBlock, Discipline>
    {
        public Expression<Func<InformationBlock, bool>> Build(Discipline expressionInfo)
        {
            var expression = PredicateBuilder.True<InformationBlock>();
            
            if (expressionInfo.CourseProjectSemester is null && expressionInfo.CourseWorkSemester is null)
            {
                expression = expression.And(_ => !_.Number.StartsWith("2.3"));
                expression = expression.And(_ => !_.Number.StartsWith("5.5"));
            }
            
            if (expressionInfo.LaboratoryClassesHours is null)
            {
                expression = expression.And(_ => !_.Number.StartsWith("5.3"));
            }
            
            if (expressionInfo.PracticalClassesHours is null)
            {
                expression = expression.And(_ => !_.Number.StartsWith("5.4"));
            }

            return expression;
        }
    }
}