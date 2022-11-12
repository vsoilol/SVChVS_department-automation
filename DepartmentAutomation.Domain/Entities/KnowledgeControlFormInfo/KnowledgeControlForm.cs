using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DepartmentAutomation.Domain.Contracts;

namespace DepartmentAutomation.Domain.Entities.KnowledgeControlFormInfo
{
    /// <summary>
    /// Форма контроля знаний
    /// </summary>
    public class KnowledgeControlForm : Entity<int>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string ShortName { get; set; }

        public virtual List<KnowledgeAssessment> KnowledgeAssessments { get; set; }

        public virtual List<EducationalProgram> EducationalPrograms { get; set; }
    }
}