using AutoMapper;
using DepartmentAutomation.Application.Common.Mappings;

namespace DepartmentAutomation.Application.Contracts.Responses
{
    public class CurriculumsStudyStartingYearDto : IMapFrom<int>
    {
        public int StudyStartingYear { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<int, CurriculumsStudyStartingYearDto>()
                .ForMember(dto => dto.StudyStartingYear,
                    opt => opt.MapFrom(x => x));
        }
    }
}