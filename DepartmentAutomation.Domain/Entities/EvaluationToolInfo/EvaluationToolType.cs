using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DepartmentAutomation.Domain.Contracts;
using DepartmentAutomation.Domain.Entities.CompetenceInfo;

namespace DepartmentAutomation.Domain.Entities.EvaluationToolInfo
{
    /// <summary>
    /// Вид оценочных средств
    /// </summary>
    public class EvaluationToolType : Entity<int>
    {
        [Required]
        public string Name { get; set; }

        public virtual List<EvaluationTool> EvaluationTools { get; set; }

        public virtual List<CompetenceFormationLevel> CompetenceFormationLevels { get; set; }
    }
}