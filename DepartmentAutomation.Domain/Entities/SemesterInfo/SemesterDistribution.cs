using DepartmentAutomation.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using DepartmentAutomation.Domain.Entities.SemesterInfo;

namespace DepartmentAutomation.Domain.Entities
{
    /// <summary>
    /// Распределение по семестрам.
    /// </summary>
    public class SemesterDistribution
    {
        [Required]
        public int DisciplineId { get; set; }

        public virtual Discipline Discipline { get; set; }

        [Required]
        public int SemesterId { get; set; }

        public virtual Semester Semester { get; set; }

        [Required]
        public KnowledgeCheckType KnowledgeCheckType { get; set; }
    }
}