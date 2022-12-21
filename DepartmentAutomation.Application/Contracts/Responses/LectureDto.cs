using System.Linq;
using AutoMapper;
using DepartmentAutomation.Application.Common.Mappings;
using DepartmentAutomation.Domain.Entities;

namespace DepartmentAutomation.Application.Contracts.Responses
{
    public class LectureDto : IMapFrom<Lesson>
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public string Name { get; set; }

        public int Hours { get; set; }
        
        public string Content { get; set; }
        
        public int[] CompetencesId { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Lesson, LectureDto>()
                .ForMember(dto => dto.CompetencesId,
                    opt => opt
                        .MapFrom(x => x.Competences.Select(_ => _.Id)));
        }
    }
}