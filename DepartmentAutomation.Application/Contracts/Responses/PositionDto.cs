using DepartmentAutomation.Application.Common.Mappings;
using DepartmentAutomation.Domain.Entities.TeacherInformation;

namespace DepartmentAutomation.Application.Contracts.Responses
{
    public class PositionDto : IMapFrom<Position>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}