using DepartmentAutomation.Domain.Contracts;
using DepartmentAutomation.Domain.Entities.CompetenceInfo;
using DepartmentAutomation.Domain.Entities.DepartmentInfo;
using DepartmentAutomation.Domain.Entities.TeacherInformation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DepartmentAutomation.Domain.Enums;

namespace DepartmentAutomation.Domain.Entities
{
    /// <summary>
    /// Дисциплина.
    /// </summary>
    public class Discipline : Entity<int>
    {
        /// <summary>
        /// Наименование.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Номер п/п.
        /// </summary>
        [Required]
        public string Number { get; set; }

        /// <summary>
        /// Трудоемкость в часах.
        /// </summary>
        [Required]
        public int LaborIntensityHours { get; set; }

        /// <summary>
        /// Трудоемкость в ЗЕ.
        /// </summary>
        [Required]
        public int LaborIntensityCreditUnits { get; set; }

        /// <summary>
        /// Контактная работа в часах.
        /// </summary>
        [Required]
        public int ContactWorkHours { get; set; }

        /// <summary>
        /// Лекции часов.
        /// </summary>
        [Required]
        public int LecturesHours { get; set; }

        /// <summary>
        /// Лабы часы.
        /// </summary>
        public int? LaboratoryClassesHours { get; set; }

        /// <summary>
        /// Практические часы.
        /// </summary>
        public int? PracticalClassesHours { get; set; }

        /// <summary>
        /// Курсовой проект семестр.
        /// </summary>
        public int? CourseProjectSemester { get; set; }

        /// <summary>
        /// Курсовая работа семестр.
        /// </summary>
        public int? CourseWorkSemester { get; set; }

        /// <summary>
        /// Самостоятельная работа часы.
        /// </summary>
        [Required]
        public int SelfStudyHours { get; set; }

        public virtual List<SemesterDistribution> SemesterDistributions { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public virtual Department Department { get; set; }

        public virtual EducationalProgram EducationalProgram { get; set; }

        public virtual List<Indicator> Indicators { get; set; }

        public virtual List<Teacher> Teachers { get; set; }

        [Required]
        public int CurriculumId { get; set; }

        /// <summary>
        /// Специальность для которой будет преподаваться данная дисциплина
        /// </summary>
        [ForeignKey(nameof(CurriculumId))]
        public virtual Curriculum Curriculum { get; set; }

        /// <summary>
        /// Статус готовности
        /// </summary>
        [Required]
        public Status Status { get; set; } = Status.NotExist;
    }
}