using System.Collections.Generic;
using AutoMapper;
using DepartmentAutomation.Application.Common.Mappings;
using DepartmentAutomation.Application.Contracts.Responses.BriefDtos;
using DepartmentAutomation.Application.Contracts.Responses.Common;
using DepartmentAutomation.Domain.Entities;

namespace DepartmentAutomation.Application.Contracts.Responses
{
    public class AdditionalDisciplineInfoDto : IMapFrom<Discipline>
    {
        public string Name { get; set; }
        
        public string SpecialtyName { get; set; }
        
        public int StudyStartingYear { get; set; }
        
        public List<TeacherFullNameDto> Teachers { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Discipline, AdditionalDisciplineInfoDto>()
                .ForMember(dto => dto.SpecialtyName,
                    opt => opt
                        .MapFrom(x => x.Curriculum.Specialty.Name))
                .ForMember(dto => dto.StudyStartingYear,
                    opt => opt
                        .MapFrom(x => x.Curriculum.StudyStartingYear.Year))
                .ForMember(dto => dto.Teachers,
                    opt => opt
                        .MapFrom(x => x.Teachers));
        }
    }
}