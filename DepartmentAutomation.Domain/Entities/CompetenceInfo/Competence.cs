using DepartmentAutomation.Domain.Contracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DepartmentAutomation.Domain.Entities.CompetenceInfo
{
    /// <summary>
    /// Компетенция
    /// </summary>
    public class Competence : Entity<int>
    {
        /// <summary>
        /// Пример: УК-1
        /// </summary>
        [Required]
        public string Code { get; set; }

        /// <summary>
        /// Пример: Способен осуществлять поиск, критический анализ и синтез информации...
        /// </summary>
        [Required]
        public string Name { get; set; }

        public virtual List<Lesson> Lessons { get; set; }

        public virtual List<Indicator> Indicators { get; set; }
    }
}