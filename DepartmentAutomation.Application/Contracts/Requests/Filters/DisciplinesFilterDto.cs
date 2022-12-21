using DepartmentAutomation.Application.Common.Enums;
using DepartmentAutomation.Application.Common.Models;
using DepartmentAutomation.Domain.Enums;

namespace DepartmentAutomation.Application.Contracts.Requests.Filters
{
    public class DisciplinesFilterDto : SortDirectionInfo
    {
        public string DisciplineName { get; set; }
        
        public Status? Status { get; set; }
        
        public int? StudyStartingYear { get; set; }
        
        public int DepartmentId { get; set; }

        public string PropertyName { get; set; } = "Id";
        
        public Order SortDirection { get; set; }
    }
}