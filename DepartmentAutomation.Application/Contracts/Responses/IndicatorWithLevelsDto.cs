using System.Collections.Generic;
using AutoMapper;
using DepartmentAutomation.Application.Common.Mappings;
using DepartmentAutomation.Domain.Entities.CompetenceInfo;

namespace DepartmentAutomation.Application.Contracts.Responses
{
    public class IndicatorWithLevelsDto : IMapFrom<Indicator>
    {
        public int CompetenceId { get; set; }

        public string CompetenceCode { get; set; }

        public string CompetenceName { get; set; }
        
        public List<CompetenceFormationLevelDto> CompetenceFormationLevels { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Indicator, IndicatorWithLevelsDto>()
                .ForMember(dto => dto.CompetenceFormationLevels,
                    opt => opt
                        .MapFrom(x => x.CompetenceFormationLevels));
        }
    }
}