using AutoMapper;
using DepartmentAutomation.Application.Common.Mappings;
using DepartmentAutomation.Domain.Entities;
using DepartmentAutomation.Domain.Enums;

namespace DepartmentAutomation.Application.Contracts.Responses
{
    public class EducationalProgramDto : IMapFrom<EducationalProgram>
    {
        public int Id { get; set; }

        public Status Status { get; set; }

        public bool IsPracticalLessons { get; set; }

        public bool IsLaboratoryLessons { get; set; }

        public int DisciplineId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EducationalProgram, EducationalProgramDto>()
                .ForMember(dto => dto.IsLaboratoryLessons,
                    opt => opt.MapFrom(x => x.Discipline.LaboratoryClassesHours != null))
                .ForMember(dto => dto.IsPracticalLessons,
                    opt => opt.MapFrom(x => x.Discipline.PracticalClassesHours != null))
                .ForMember(dto => dto.Status,
                    opt => opt.MapFrom(x => x.Discipline.Status));
        }
    }
}