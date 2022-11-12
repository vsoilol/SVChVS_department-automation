using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DepartmentAutomation.Domain.Contracts;

namespace DepartmentAutomation.Domain.Entities
{
    /// <summary>
    /// Методичка
    /// </summary>
    public class MethodicalRecommendation : Entity<int>
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public string Link { get; set; }

        public virtual List<EducationalProgram> EducationalPrograms { get; set; }
    }
}