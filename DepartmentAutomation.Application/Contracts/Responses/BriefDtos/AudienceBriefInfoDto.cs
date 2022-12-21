using DepartmentAutomation.Application.Common.Mappings;
using DepartmentAutomation.Domain.Entities;

namespace DepartmentAutomation.Application.Contracts.Responses.BriefDtos
{
    public class AudienceBriefInfoDto : IMapFrom<Audience>
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public int BuildingNumber { get; set; }
    }
}