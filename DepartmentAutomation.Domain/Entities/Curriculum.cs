using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DepartmentAutomation.Domain.Contracts;
using DepartmentAutomation.Domain.Entities.DepartmentInfo;

namespace DepartmentAutomation.Domain.Entities
{
    /// <summary>
    /// Учебный план
    /// </summary>
    public class Curriculum : Entity<int>
    {
        /// <summary>
        /// Пример: 090304-4
        /// </summary>
        [Required]
        public string RegistrationNumber { get; set; }

        [Required]
        public DateTime ApprovalDate { get; set; }

        /// <summary>
        /// Год начало подготовки по учебному плану
        /// Пример: 2020
        /// </summary>
        [Required]
        public DateTime StudyStartingYear { get; set; }

        public virtual List<Discipline> Disciplines { get; set; }

        [Required]
        public int SpecialtyId { get; set; }

        /// <summary>
        /// Специальность для которой будет преподаваться данная дисциплина
        /// </summary>
        [ForeignKey(nameof(SpecialtyId))]
        public virtual Specialty Specialty { get; set; }
    }
}