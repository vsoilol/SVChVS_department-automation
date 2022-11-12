using System.ComponentModel.DataAnnotations;

namespace DepartmentAutomation.Domain.Entities.KnowledgeControlFormInfo
{
    public class KnowledgeAssessment
    {
        [Required]
        public int KnowledgeControlFormId { get; set; }

        public virtual KnowledgeControlForm KnowledgeControlForm { get; set; }

        [Required]
        public int WeekId { get; set; }

        public virtual Week Week { get; set; }

        [Required]
        public int MaxMark { get; set; }
    }
}