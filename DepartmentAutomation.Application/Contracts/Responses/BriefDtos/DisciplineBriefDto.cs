using DepartmentAutomation.Application.Common.Mappings;
using DepartmentAutomation.Domain.Entities;

namespace DepartmentAutomation.Application.Contracts.Responses.BriefDtos
{
    public class DisciplineBriefDto : IMapFrom<Discipline>
    {
        public string Name { get; set; }
    }
}