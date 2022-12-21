using AutoMapper;
using DepartmentAutomation.Application.Common.Mappings;
using DepartmentAutomation.Domain.Entities.InformationBlockInfo;

namespace DepartmentAutomation.Application.Common.Models.WordDocument
{
    public class InformationBlock : IMapFrom<InformationBlockContent>
    {
        public string Number { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<InformationBlockContent, InformationBlock>()
                .ForMember(dto => dto.Number,
                    opt => opt.MapFrom(x => x.InformationBlock.Number))
                .ForMember(dto => dto.Name,
                    opt => opt.MapFrom(x => x.InformationBlock.Name));
        }
    }
}