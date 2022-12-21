using DepartmentAutomation.Application.Common.Enums;

namespace DepartmentAutomation.Application.Common.Models
{
    public interface SortDirectionInfo
    {
        string PropertyName { get; set; }

        Order SortDirection { get; set; }
    }
}