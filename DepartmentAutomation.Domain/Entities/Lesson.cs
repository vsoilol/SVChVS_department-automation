using DepartmentAutomation.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DepartmentAutomation.Domain.Contracts;
using DepartmentAutomation.Domain.Entities.CompetenceInfo;

namespace DepartmentAutomation.Domain.Entities
{
    public class Lesson : Entity<int>
    {
        [Required]
        public int Number { get; set; }

        [Required]
        public string Name { get; set; }

        public string Content { get; set; }

        [Required]
        public int Hours { get; set; }

        [Required]
        public LessonType LessonType { get; set; }

        public int? TrainingCourseFormId { get; set; }

        public virtual TrainingCourseForm TrainingCourseForm { get; set; }

        public virtual List<Week> Weeks { get; set; }

        public virtual List<Competence> Competences { get; set; }

        [Required]
        public int EducationalProgramId { get; set; }

        [ForeignKey(nameof(EducationalProgramId))]
        public virtual EducationalProgram EducationalProgram { get; set; }
    }
}