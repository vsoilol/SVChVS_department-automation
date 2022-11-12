using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DepartmentAutomation.Domain.Contracts;

namespace DepartmentAutomation.Domain.Entities
{
    public class Protocol : Entity<int>
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int Number { get; set; }

        public virtual List<EducationalProgram> EducationalPrograms { get; set; }
    }
}