using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DepartmentAutomation.Domain.Contracts;

namespace DepartmentAutomation.Domain.Entities.DepartmentInfo
{
    /// <summary>
    /// Специальность
    /// </summary>
    public class Specialty : Entity<int>
    {
        /// <summary>
        /// Пример: Программная инженерия
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Пример: 09.03.04
        /// </summary>
        [Required]
        public string Code { get; set; }

        /// <summary>
        /// Название профиля
        /// Пример:  Разработка программно-информационных систем
        /// </summary>
        [Required]
        public string ProfileName { get; set; }

        /// <summary>
        /// Квалификация
        /// Пример: Бакалавр
        /// </summary>
        [Required]
        public string Qualification { get; set; }

        /// <summary>
        /// Форма обучения
        /// Пример: Очная
        /// </summary>
        [Required]
        public string LearningForm { get; set; }

        /// <summary>
        /// Срок обучения
        /// Пример: 4 года
        /// </summary>
        [Required]
        public int StudyPeriod { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [Required]
        public int FederalStateEducationalStandardId { get; set; }

        [ForeignKey(nameof(FederalStateEducationalStandardId))]
        public virtual FederalStateEducationalStandard FederalStateEducationalStandard { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public virtual Department Department { get; set; }

        public virtual List<Curriculum> Curricula { get; set; }

        [Required]
        public int FacultyId { get; set; }

        [ForeignKey(nameof(FacultyId))]
        public virtual Faculty Faculty { get; set; }
    }
}