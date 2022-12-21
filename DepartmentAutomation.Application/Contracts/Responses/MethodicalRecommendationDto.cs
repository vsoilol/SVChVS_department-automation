using DepartmentAutomation.Application.Common.Mappings;
using DepartmentAutomation.Domain.Entities;

namespace DepartmentAutomation.Application.Contracts.Responses
{
    public class MethodicalRecommendationDto : IMapFrom<MethodicalRecommendation>
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string Link { get; set; }
    }
}