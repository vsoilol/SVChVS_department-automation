using AutoMapper;
using DepartmentAutomation.Application.Common.Mappings;
using DepartmentAutomation.Domain.Entities.KnowledgeControlFormInfo;

namespace DepartmentAutomation.Application.Contracts.Responses
{
    public class KnowledgeAssessmentDto : IMapFrom<KnowledgeAssessment>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public int MaxMark { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<KnowledgeAssessment, KnowledgeAssessmentDto>()
                .ForMember(dto => dto.Id,
                    opt => opt.MapFrom(x => x.KnowledgeControlFormId))
                .ForMember(dto => dto.Name,
                    opt => opt.MapFrom(x => x.KnowledgeControlForm.Name))
                .ForMember(dto => dto.ShortName,
                    opt => opt.MapFrom(x => x.KnowledgeControlForm.ShortName));
        }
    }
}