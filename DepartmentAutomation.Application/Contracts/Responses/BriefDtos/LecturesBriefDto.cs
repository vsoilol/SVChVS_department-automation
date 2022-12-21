using System.Linq;
using AutoMapper;
using DepartmentAutomation.Application.Common.Mappings;
using DepartmentAutomation.Domain.Entities;

namespace DepartmentAutomation.Application.Contracts.Responses.BriefDtos
{
    public class LecturesBriefDto : IMapFrom<Lesson>
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public string Name { get; set; }

        public int Hours { get; set; }

        public string CompetenceInfo { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Lesson, LecturesBriefDto>()
                .ForMember(dto => dto.CompetenceInfo,
                    opt => opt
                        .MapFrom(x => string.Join(',', x.Competences.Select(_ => _.Code))));
        }
    }
}