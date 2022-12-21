using AutoMapper;
using DepartmentAutomation.Application.Common.Mappings;

namespace DepartmentAutomation.Application.Common.Models.WordDocument
{
    public class EvaluationTool : IMapFrom<Domain.Entities.EvaluationToolInfo.EvaluationTool>
    {
        public string Name { get; set; }

        public int SetNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.EvaluationToolInfo.EvaluationTool, EvaluationTool>()
                .ForMember(dto => dto.Name,
                    opt => opt.MapFrom(x => x.EvaluationToolType.Name));
        }
    }
}