using DepartmentAutomation.Domain.Contracts;
using DepartmentAutomation.Domain.Entities.KnowledgeControlFormInfo;
using DepartmentAutomation.Domain.Entities.SemesterInfo;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepartmentAutomation.Domain.Entities
{
    public class Week : Entity<int>
    {
        [Required]
        public int Number { get; set; }

        /// <summary>
        /// Самостоятельная работа часы
        /// </summary>
        [Required]
        public int IndependentWorkHours { get; set; }

        [Required]
        public int TrainingModuleNumber { get; set; }

        [Required]
        public int SemesterId { get; set; }

        [ForeignKey(nameof(SemesterId))]
        public virtual Semester Semester { get; set; }

        [Required]
        public int EducationalProgramId { get; set; }

        [ForeignKey(nameof(EducationalProgramId))]
        public virtual EducationalProgram EducationalProgram { get; set; }

        public virtual List<Lesson> Lessons { get; set; }

        public virtual List<KnowledgeAssessment> KnowledgeAssessments { get; set; }
    }
}