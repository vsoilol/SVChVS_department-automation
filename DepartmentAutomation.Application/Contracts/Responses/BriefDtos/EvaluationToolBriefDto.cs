using AutoMapper;
using DepartmentAutomation.Application.Common.Mappings;
using DepartmentAutomation.Domain.Entities.EvaluationToolInfo;

namespace DepartmentAutomation.Application.Contracts.Responses.BriefDtos
{
    public class EvaluationToolBriefDto : IMapFrom<EvaluationTool>
    {
        public int EvaluationToolTypeId { get; set; }

        public string Name { get; set; }

        public int SetNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EvaluationTool, EvaluationToolBriefDto>()
                .ForMember(dto => dto.Name,
                    opt => opt.MapFrom(x => x.EvaluationToolType.Name));
        }
    }
}