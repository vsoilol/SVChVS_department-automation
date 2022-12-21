using DepartmentAutomation.Application.Common.Enums;
using DepartmentAutomation.Application.Common.Models;

namespace DepartmentAutomation.Application.Contracts.Requests.Filters
{
    public class TeacherFilterDto : SortDirectionInfo
    {
        public string Surname { get; set; }

        public int? DepartmentId { get; set; }

        public int? PositionId { get; set; }

        public string PropertyName { get; set; } = "Id";
        
        public Order SortDirection { get; set; }
    }
}