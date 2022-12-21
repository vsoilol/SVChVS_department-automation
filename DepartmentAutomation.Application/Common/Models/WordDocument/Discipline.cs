using System.Collections.Generic;
using AutoMapper;
using DepartmentAutomation.Application.Common.Mappings;

namespace DepartmentAutomation.Application.Common.Models.WordDocument
{
    public class Discipline : IMapFrom<Domain.Entities.Discipline>
    {
        /// <summary>
        ///     Пример: 090304-4
        /// </summary>
        public string RegistrationNumber { get; set; }

        /// <summary>
        ///     Номер п/п.
        ///     Пример: Б.1.0.19
        /// </summary>
        public string Number { get; set; }

        public string Name { get; set; }

        /// <summary>
        ///     Трудоемкость в часах.
        /// </summary>
        public int LaborIntensityHours { get; set; }

        /// <summary>
        ///     Трудоемкость в ЗЕ.
        /// </summary>
        public int LaborIntensityCreditUnits { get; set; }

        /// <summary>
        ///     Контактная работа в часах.
        /// </summary>
        public int ContactWorkHours { get; set; }

        /// <summary>
        ///     Лекции часов.
        /// </summary>
        public int LecturesHours { get; set; }

        /// <summary>
        ///     Лабы часы.
        /// </summary>
        public int? LaboratoryClassesHours { get; set; }

        /// <summary>
        ///     Практические часы.
        /// </summary>
        public int? PracticalClassesHours { get; set; }

        /// <summary>
        ///     Курсовой проект семестр.
        /// </summary>
        public int? CourseProjectSemester { get; set; }

        /// <summary>
        ///     Курсовая работа семестр.
        /// </summary>
        public int? CourseWorkSemester { get; set; }

        /// <summary>
        ///     Самостоятельная работа часы.
        /// </summary>
        public int SelfStudyHours { get; set; }

        public Specialty Specialty { get; set; }

        /// <summary>
        ///     Кафедра-разработчик программы
        /// </summary>
        public Department Department { get; set; }

        public List<Teacher> Teachers { get; set; }

        public List<Semester> Semesters { get; set; }

        public List<Indicator> Indicators { get; set; }

        public Curriculum Curriculum { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Discipline, Discipline>()
                .ForMember(dto => dto.Semesters,
                    opt => opt.MapFrom(x => x.SemesterDistributions))
                .ForMember(dto => dto.RegistrationNumber,
                    opt => opt.MapFrom(x => x.Curriculum.RegistrationNumber))
                .ForMember(dto => dto.Curriculum,
                    opt => opt.MapFrom(x => x.Curriculum))
                .ForMember(dto => dto.Specialty,
                    opt => opt.MapFrom(x => x.Curriculum.Specialty));
        }
    }
}