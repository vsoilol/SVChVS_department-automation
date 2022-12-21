using DepartmentAutomation.Application.Common.Mappings;
using DepartmentAutomation.Domain.Entities.CompetenceInfo;

namespace DepartmentAutomation.Application.Contracts.Responses
{
    public class CompetenceDto : IMapFrom<Competence>
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }
    }
}