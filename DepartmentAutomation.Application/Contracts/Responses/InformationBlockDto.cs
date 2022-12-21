using AutoMapper;
using DepartmentAutomation.Application.Common.Mappings;
using DepartmentAutomation.Domain.Entities.InformationBlockInfo;

namespace DepartmentAutomation.Application.Contracts.Responses
{
    public class InformationBlockDto : IMapFrom<InformationBlockContent>
    {
        public int Id { get; set; }

        public int EducationalProgramId { get; set; }

        public string Content { get; set; }

        public string Number { get; set; }

        public string Name { get; set; }

        public bool IsRequired { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<InformationBlockContent, InformationBlockDto>()
                .ForMember(dto => dto.Id,
                    opt => opt.MapFrom(x => x.InformationBlock.Id))
                .ForMember(dto => dto.Number,
                opt => opt.MapFrom(x => x.InformationBlock.Number))
                .ForMember(dto => dto.Name,
                    opt => opt.MapFrom(x => x.InformationBlock.Name))
                .ForMember(dto => dto.IsRequired,
                    opt => opt.MapFrom(x => x.InformationBlock.IsRequired));
        }
    }
}