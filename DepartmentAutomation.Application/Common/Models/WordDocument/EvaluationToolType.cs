using DepartmentAutomation.Application.Common.Mappings;

namespace DepartmentAutomation.Application.Common.Models.WordDocument
{
    public class EvaluationToolType : IMapFrom<Domain.Entities.EvaluationToolInfo.EvaluationToolType>
    {
        public string Name { get; set; }
    }
}