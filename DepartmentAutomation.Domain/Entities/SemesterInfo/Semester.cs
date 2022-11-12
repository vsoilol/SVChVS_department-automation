using DepartmentAutomation.Domain.Contracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DepartmentAutomation.Domain.Entities.SemesterInfo
{
    /// <summary>
    /// Семестр.
    /// </summary>
    public class Semester : Entity<int>
    {
        /// <summary>
        /// Номер семестра.
        /// </summary>
        [Required]
        public int Number { get; set; }

        /// <summary>
        /// Количество недель.
        /// </summary>
        [Required]
        public int WeeksNumber { get; set; }

        [Required]
        public int CourseProjectEndWeek { get; set; }

        [Required]
        public int ExamEndWeek { get; set; }

        /// <summary>
        /// Номер курса.
        /// </summary>
        [Required]
        public int CourseNumber { get; set; }

        public virtual List<SemesterDistribution> SemesterDistributions { get; set; }

        public virtual List<Week> Weeks { get; set; }
    }
}