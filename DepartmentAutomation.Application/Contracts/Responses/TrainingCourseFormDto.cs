using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DepartmentAutomation.Application.Common.Mappings;
using DepartmentAutomation.Domain.Entities;
using DepartmentAutomation.Domain.Enums;

namespace DepartmentAutomation.Application.Contracts.Responses
{
    public class TrainingCourseFormDto : IMapFrom<TrainingCourseForm>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<LessonDto> Lectures { get; set; }

        public List<LessonDto> PracticalLessons { get; set; }

        public List<LessonDto> LaboratoryLessons { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TrainingCourseForm, TrainingCourseFormDto>()
                .ForMember(dto => dto.Lectures,
                    opt => opt
                        .MapFrom(x => x.Lessons
                            .Where(_ => _.LessonType == LessonType.Lecture)))
                .ForMember(dto => dto.PracticalLessons,
                    opt => opt
                        .MapFrom(x => x.Lessons
                            .Where(_ => _.LessonType == LessonType.Practical)))
                .ForMember(dto => dto.LaboratoryLessons,
                    opt => opt
                        .MapFrom(x => x.Lessons
                            .Where(_ => _.LessonType == LessonType.Laboratory)));
        }
    }
}