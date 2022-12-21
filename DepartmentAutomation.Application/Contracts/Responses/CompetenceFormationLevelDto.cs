using AutoMapper;
using DepartmentAutomation.Application.Common.Mappings;
using DepartmentAutomation.Domain.Entities.CompetenceInfo;
using DepartmentAutomation.Domain.Enums;
using System.Collections.Generic;

namespace DepartmentAutomation.Application.Contracts.Responses
{
    public class CompetenceFormationLevelDto : IMapFrom<CompetenceFormationLevel>
    {
        public int Id { get; set; }

        public FormationLevel FormationLevel { get; set; }

        public string FactualDescription { get; set; }

        public string LearningOutcomes { get; set; }

        public List<EvaluationToolTypeDto> EvaluationToolTypes { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CompetenceFormationLevel, CompetenceFormationLevelDto>()
                .ForMember(dto => dto.EvaluationToolTypes,
                    opt => opt.MapFrom(x => x.EvaluationToolTypes));
        }
    }
}