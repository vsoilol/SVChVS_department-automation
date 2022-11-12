using System.ComponentModel.DataAnnotations;
using DepartmentAutomation.Domain.Contracts;

namespace DepartmentAutomation.Domain.Entities.DepartmentInfo
{
    /// <summary>
    /// Заведующий кафедры.
    /// </summary>
    public class DepartmentHead : Entity<int>
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

        public virtual Department Department { get; set; }
    }
}