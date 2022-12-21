using System;
using AutoMapper;
using DepartmentAutomation.Application.Common.Mappings;
using DepartmentAutomation.Domain.Entities;
using DepartmentAutomation.Domain.Enums;

namespace DepartmentAutomation.Application.Contracts.Responses
{
    public class DepartmentHeadDisciplineDto : IMapFrom<Discipline>
    {
        public int DisciplineId { get; set; }
        
        public string Name { get; set; }
        
        public int StudyStartingYear { get; set; }
        
        public Status Status { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Discipline, DepartmentHeadDisciplineDto>()
                .ForMember(dto => dto.DisciplineId,
                    opt => opt
                        .MapFrom(x => x.Id))
                .ForMember(dto => dto.StudyStartingYear,
                    opt => opt
                        .MapFrom(x => x.Curriculum.StudyStartingYear.Year));
        }
    }
}