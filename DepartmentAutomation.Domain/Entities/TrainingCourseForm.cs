using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DepartmentAutomation.Domain.Contracts;

namespace DepartmentAutomation.Domain.Entities
{
    /// <summary>
    /// Форма проведения занятия
    /// </summary>
    public class TrainingCourseForm : Entity<int>
    {
        /// <summary>
        /// Пример: Традиционные
        /// </summary>
        [Required]
        public string Name { get; set; }

        public virtual List<EducationalProgram> EducationalPrograms { get; set; }

        public virtual List<Lesson> Lessons { get; set; }
    }
}