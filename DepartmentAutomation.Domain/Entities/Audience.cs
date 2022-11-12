using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DepartmentAutomation.Domain.Contracts;

namespace DepartmentAutomation.Domain.Entities
{
    /// <summary>
    /// Аудитория
    /// </summary>
    public class Audience : Entity<int>
    {
        [Required]
        public int Number { get; set; }

        /// <summary>
        /// Номер корпуса
        /// </summary>
        [Required]
        public int BuildingNumber { get; set; }

        /// <summary>
        /// Пример: ПУЛ-4/517.2-21
        /// </summary>
        [Required]
        public string RegistrationNumber { get; set; }

        public List<EducationalProgram> EducationalPrograms { get; set; }
    }
}