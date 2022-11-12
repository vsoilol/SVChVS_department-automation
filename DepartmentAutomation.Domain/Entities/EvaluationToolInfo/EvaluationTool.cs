using System.ComponentModel.DataAnnotations;

namespace DepartmentAutomation.Domain.Entities.EvaluationToolInfo
{
    /// <summary>
    /// Оценочные средства
    /// </summary>
    public sealed class EvaluationTool
    {
        [Required]
        public int EducationalProgramId { get; set; }

        public EducationalProgram EducationalProgram { get; set; }

        [Required]
        public int EvaluationToolTypeId { get; set; }

        public EvaluationToolType EvaluationToolType { get; set; }

        /// <summary>
        /// Количество комплектов
        /// </summary>
        [Required]
        public int SetNumber { get; set; }
    }
}