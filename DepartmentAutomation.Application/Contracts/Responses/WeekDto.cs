using AutoMapper;
using DepartmentAutomation.Application.Common.Mappings;
using DepartmentAutomation.Domain.Entities;
using DepartmentAutomation.Domain.Enums;
using System.Collections.Generic;
using System.Linq;

namespace DepartmentAutomation.Application.Contracts.Responses
{
    public class WeekDto : IMapFrom<Week>
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public int IndependentWorkHours { get; set; }

        public int TrainingModuleNumber { get; set; }

        public LessonDto Lecture { get; set; }

        public LessonDto PracticalLesson { get; set; }

        public LessonDto LaboratoryLesson { get; set; }

        public List<KnowledgeAssessmentDto> KnowledgeAssessments { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Week, WeekDto>()
                .ForMember(dto => dto.Lecture,
                    opt => opt
                        .MapFrom(x => x.Lessons
                            .FirstOrDefault(_ => _.LessonType == LessonType.Lecture)))
                .ForMember(dto => dto.PracticalLesson,
                    opt => opt
                        .MapFrom(x => x.Lessons
                            .FirstOrDefault(_ => _.LessonType == LessonType.Practical)))
                .ForMember(dto => dto.LaboratoryLesson,
                    opt => opt
                        .MapFrom(x => x.Lessons
                            .FirstOrDefault(_ => _.LessonType == LessonType.Laboratory)))
                .ForMember(dto => dto.KnowledgeAssessments,
                opt => opt
                    .MapFrom(x => x.KnowledgeAssessments));
        }
    }
}