using System.ComponentModel.DataAnnotations;

namespace DepartmentAutomation.Domain.Entities.InformationBlockInfo
{
    public class InformationBlockContent
    {
        [Required]
        public int EducationalProgramId { get; set; }

        public virtual EducationalProgram EducationalProgram { get; set; }

        [Required]
        public int InformationBlockId { get; set; }

        public virtual InformationBlock InformationBlock { get; set; }

        [Required]
        public string Content { get; set; }
    }
}