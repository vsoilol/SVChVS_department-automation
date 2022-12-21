using DepartmentAutomation.Application.Common.Mappings;
using DepartmentAutomation.Domain.Entities.EvaluationToolInfo;

namespace DepartmentAutomation.Application.Contracts.Responses
{
    public class EvaluationToolTypeDto : IMapFrom<EvaluationToolType>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}