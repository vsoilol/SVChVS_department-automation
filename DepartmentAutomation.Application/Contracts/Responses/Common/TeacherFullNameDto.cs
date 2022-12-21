using AutoMapper;
using DepartmentAutomation.Application.Common.Mappings;
using DepartmentAutomation.Domain.Entities.TeacherInformation;

namespace DepartmentAutomation.Application.Contracts.Responses.Common
{
    public class TeacherFullNameDto : IMapFrom<Teacher>
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Teacher, TeacherFullNameDto>()
                .ForMember(dto => dto.FullName,
                    opt => opt
                        .MapFrom(x => x.ApplicationUser.Surname + 
                                      " " + x.ApplicationUser.UserName + 
                                      " " + x.ApplicationUser.Patronymic));
        }
    }
}