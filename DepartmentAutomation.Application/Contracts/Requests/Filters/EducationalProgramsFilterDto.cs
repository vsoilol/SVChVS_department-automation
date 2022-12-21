using DepartmentAutomation.Application.Common.Enums;
using DepartmentAutomation.Application.Common.Models;

namespace DepartmentAutomation.Application.Contracts.Requests.Filters
{
    public class EducationalProgramsFilterDto : SortDirectionInfo
    {
        public string DisciplineName { get; set; }
        
        public string UserId { get; set; }

        public string PropertyName { get; set; } = "Id";

        public Order SortDirection { get; set; } = Order.Asc;
    }
}