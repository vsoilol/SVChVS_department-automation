using AutoMapper;
using DepartmentAutomation.Application.Common.Mappings;
using DepartmentAutomation.Domain.Entities.LiteratureInfo;
using DepartmentAutomation.Domain.Enums;

namespace DepartmentAutomation.Application.Common.Models.WordDocument
{
    public class Literature : IMapFrom<LiteratureTypeInfo>
    {
        public LiteratureType Type { get; set; }

        public string Description { get; set; }

        /// <summary>
        ///     Гриф
        /// </summary>
        public string Recommended { get; set; }

        /// <summary>
        ///     Количество экземпляров
        /// </summary>
        public string SetNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<LiteratureTypeInfo, Literature>()
                .ForMember(dto => dto.Description,
                    opt => opt.MapFrom(x => x.Literature.Description))
                .ForMember(dto => dto.Recommended,
                    opt => opt.MapFrom(x => x.Literature.Recommended))
                .ForMember(dto => dto.SetNumber,
                    opt => opt.MapFrom(x => x.Literature.SetNumber));
        }
    }
}