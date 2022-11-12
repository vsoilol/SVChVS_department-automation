using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DepartmentAutomation.Domain.Contracts;

namespace DepartmentAutomation.Domain.Entities.CompetenceInfo
{
    /// <summary>
    /// Индикатор
    /// </summary>
    public class Indicator : Entity<int>
    {
        /// <summary>
        /// Пример: 1
        /// </summary>
        [Required]
        public int Number { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int CompetenceId { get; set; }

        [ForeignKey(nameof(CompetenceId))]
        public virtual Competence Competence { get; set; }

        public virtual List<Discipline> Disciplines { get; set; }

        public virtual List<CompetenceFormationLevel> CompetenceFormationLevels { get; set; }
    }
}