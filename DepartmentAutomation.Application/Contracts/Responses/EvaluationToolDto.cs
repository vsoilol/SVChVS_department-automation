using AutoMapper;
using DepartmentAutomation.Application.Common.Mappings;
using DepartmentAutomation.Domain.Entities.EvaluationToolInfo;

namespace DepartmentAutomation.Application.Contracts.Responses
{
    public class EvaluationToolDto : IMapFrom<EvaluationTool>
    {
        public int EvaluationTool { get; set; }

        public int EducationalProgramId { get; set; }

        public int SetNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EvaluationTool, EvaluationToolDto>()
                .ForMember(dto => dto.EvaluationTool,
                    opt => opt.MapFrom(x => x.EvaluationToolType));
        }
    }
}