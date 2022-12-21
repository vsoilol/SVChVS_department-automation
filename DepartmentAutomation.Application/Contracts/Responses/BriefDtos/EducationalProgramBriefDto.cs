using AutoMapper;
using DepartmentAutomation.Application.Common.Mappings;
using DepartmentAutomation.Domain.Entities;
using DepartmentAutomation.Domain.Enums;

namespace DepartmentAutomation.Application.Contracts.Responses.BriefDtos
{
    public class EducationalProgramBriefDto : IMapFrom<EducationalProgram>
    {
        public int Id { get; set; }

        public Status Status { get; set; }

        public string DisciplineName { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<EducationalProgram, EducationalProgramBriefDto>()
                .ForMember(dto => dto.Status,
                    opt => opt.MapFrom(x => x.Discipline.Status));
        }
    }
}