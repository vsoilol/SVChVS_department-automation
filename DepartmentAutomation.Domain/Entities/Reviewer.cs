using System.ComponentModel.DataAnnotations;
using DepartmentAutomation.Domain.Contracts;

namespace DepartmentAutomation.Domain.Entities
{
    public class Reviewer : Entity<int>
    {
        [Required]
        public string Position { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Patronymic { get; set; }

        public virtual EducationalProgram EducationalProgram { get; set; }
    }
}