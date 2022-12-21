using DepartmentAutomation.Application.Common.Mappings;

namespace DepartmentAutomation.Application.Common.Models.WordDocument
{
    public class MethodicalRecommendation : IMapFrom<Domain.Entities.MethodicalRecommendation>
    {
        public string Content { get; set; }

        public string Link { get; set; }
    }
}