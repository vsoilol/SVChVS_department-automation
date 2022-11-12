using System;

namespace DepartmentAutomation.Domain.Enums
{
    [Flags]
    public enum Role
    {
        Admin = 1,
        Teacher = 2,
        DepartmentHead = 3,
        EducationDepartmentOfficial = 4, // Сотрудник учебного отдела
        SimpleUser = 5
    }
}