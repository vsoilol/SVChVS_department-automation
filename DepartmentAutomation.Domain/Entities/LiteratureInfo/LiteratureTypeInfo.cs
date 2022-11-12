using DepartmentAutomation.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace DepartmentAutomation.Domain.Entities.LiteratureInfo
{
    public class LiteratureTypeInfo
    {
        [Required]
        public int EducationalProgramId { get; set; }

        public virtual EducationalProgram EducationalProgram { get; set; }

        [Required]
        public int LiteratureId { get; set; }

        public virtual Literature Literature { get; set; }

        [Required]
        public LiteratureType Type { get; set; }
    }
}