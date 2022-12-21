using DepartmentAutomation.Application.Common.Mappings;
using DepartmentAutomation.Domain.Entities.LiteratureInfo;

namespace DepartmentAutomation.Application.Contracts.Responses
{
    public class LiteratureDto : IMapFrom<Literature>
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string Recommended { get; set; }

        public string SetNumber { get; set; }
    }
}