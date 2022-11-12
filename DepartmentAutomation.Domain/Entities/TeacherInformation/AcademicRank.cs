using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DepartmentAutomation.Domain.Contracts;

namespace DepartmentAutomation.Domain.Entities.TeacherInformation
{
    /// <summary>
    /// Ученое звание
    /// </summary>
    public class AcademicRank : Entity<int>
    {
        [Required]
        public string Name { get; set; }

        public virtual List<Teacher> Teachers { get; set; }
    }
}