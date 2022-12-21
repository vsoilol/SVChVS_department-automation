using DepartmentAutomation.Application.Common.Mappings;
using DepartmentAutomation.Domain.Entities.DepartmentInfo;

namespace DepartmentAutomation.Application.Contracts.Responses
{
    public class DepartmentDto : IMapFrom<Department>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }
    }
}