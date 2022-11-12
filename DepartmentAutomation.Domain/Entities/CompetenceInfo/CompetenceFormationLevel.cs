using DepartmentAutomation.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DepartmentAutomation.Domain.Contracts;
using DepartmentAutomation.Domain.Entities.EvaluationToolInfo;

namespace DepartmentAutomation.Domain.Entities.CompetenceInfo
{
    /// <summary>
    /// Уровень сформированности компетенции
    /// </summary>
    public class CompetenceFormationLevel : Entity<int>
    {
        [Required]
        public int LevelNumber { get; set; }

        [Required]
        public FormationLevel FormationLevel { get; set; }

        /// <summary>
        /// Содержательное описание уровня
        /// </summary>
        [Required]
        public string FactualDescription { get; set; }

        /// <summary>
        /// Результаты обучения
        /// </summary>
        [Required]
        public string LearningOutcomes { get; set; }

        [Required]
        public int IndicatorId { get; set; }

        [ForeignKey(nameof(IndicatorId))]
        public virtual Indicator Indicator { get; set; }

        [Required]
        public int EducationalProgramId { get; set; }

        [ForeignKey(nameof(EducationalProgramId))]
        public virtual EducationalProgram EducationalProgram { get; set; }

        public virtual List<EvaluationToolType> EvaluationToolTypes { get; set; }
    }
}