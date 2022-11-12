using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DepartmentAutomation.Domain.Contracts;

namespace DepartmentAutomation.Domain.Entities.TeacherInformation
{
    /// <summary>
    /// Должность
    /// </summary>
    public class Position : Entity<int>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string ShortName { get; set; }

        public virtual List<Teacher> Teachers { get; set; }
    }
}