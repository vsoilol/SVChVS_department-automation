using System.Collections.Generic;
using AutoMapper;
using DepartmentAutomation.Application.Common.Mappings;
using DepartmentAutomation.Domain.Entities;
using DepartmentAutomation.Domain.Entities.SemesterInfo;
using DepartmentAutomation.Domain.Enums;

namespace DepartmentAutomation.Application.Common.Models.WordDocument
{
    public class Semester : IMapFrom<SemesterDistribution>
    {
        /// <summary>
        ///     Номер семестра.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        ///     Количество недель.
        /// </summary>
        public int WeeksNumber { get; set; }

        /// <summary>
        ///     Номер курса.
        /// </summary>
        public int CourseNumber { get; set; }

        public KnowledgeCheckType KnowledgeCheckType { get; set; }

        public int CourseProjectEndWeek { get; set; }

        public int ExamEndWeek { get; set; }

        public List<Week> Weeks { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SemesterDistribution, Semester>()
                .ForMember(dto => dto.Number,
                    opt => opt.MapFrom(x => x.Semester.Number))
                .ForMember(dto => dto.WeeksNumber,
                    opt => opt.MapFrom(x => x.Semester.WeeksNumber))
                .ForMember(dto => dto.CourseNumber,
                    opt => opt.MapFrom(x => x.Semester.CourseNumber))
                .ForMember(dto => dto.ExamEndWeek,
                    opt => opt.MapFrom(x => x.Semester.ExamEndWeek))
                .ForMember(dto => dto.CourseProjectEndWeek,
                    opt => opt.MapFrom(x => x.Semester.CourseProjectEndWeek));
        }
    }
}