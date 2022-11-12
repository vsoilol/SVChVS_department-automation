using DepartmentAutomation.Domain.Contracts;
using System.ComponentModel.DataAnnotations;

namespace DepartmentAutomation.Domain.Entities
{
    /// <summary>
    /// Проверяющий
    /// </summary>
    public class Inspector : Entity<int>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        /// <summary>
        /// Отчество.
        /// </summary>
        [Required]
        public string Patronymic { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        [Required]
        public string Position { get; set; }
    }
}