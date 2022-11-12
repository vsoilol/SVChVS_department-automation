using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DepartmentAutomation.Domain.Contracts;
using DepartmentAutomation.Domain.Entities.TeacherInformation;

namespace DepartmentAutomation.Domain.Entities.DepartmentInfo
{
    /// <summary>
    /// Кафедра.
    /// </summary>
    public class Department : Entity<int>
    {
        /// <summary>
        /// Пример: Программное обеспечение информационных технологий
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Пример: ПОИТ
        /// </summary>
        [Required]
        public string ShortName { get; set; }

        [Required]
        public int DepartmentHeadId { get; set; }

        [ForeignKey(nameof(DepartmentHeadId))]
        public virtual DepartmentHead DepartmentHead { get; set; }

        public virtual List<Teacher> Teachers { get; set; }

        public virtual List<Discipline> Disciplines { get; set; }
    }
}