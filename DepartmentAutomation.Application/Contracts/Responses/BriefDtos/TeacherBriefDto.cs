using AutoMapper;
using DepartmentAutomation.Application.Common.Mappings;
using DepartmentAutomation.Domain.Entities.TeacherInformation;

namespace DepartmentAutomation.Application.Contracts.Responses.BriefDtos
{
    public class TeacherBriefDto : IMapFrom<Teacher>
    {
        public int Id { get; set; }
        
        public string UserId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public bool IsActive { get; set; }

        public string ApplicationUserId { get; set; }

        public string Position { get; set; }

        public string Department { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Teacher, TeacherBriefDto>()
                .ForMember(dto => dto.Name,
                    opt => opt
                        .MapFrom(x => x.ApplicationUser.UserName))
                .ForMember(dto => dto.Surname,
                    opt => opt
                        .MapFrom(x => x.ApplicationUser.Surname))
                .ForMember(dto => dto.Patronymic,
                    opt => opt
                        .MapFrom(x => x.ApplicationUser.Patronymic))
                .ForMember(dto => dto.IsActive,
                    opt => opt
                        .MapFrom(x => x.ApplicationUser.IsActive))
                .ForMember(dto => dto.Position,
                    opt => opt
                        .MapFrom(x => x.Position.Name))
                .ForMember(dto => dto.UserId,
                    opt => opt
                        .MapFrom(x => x.ApplicationUser.Id))
                .ForMember(dto => dto.Department,
                    opt => opt
                        .MapFrom(x => x.Department.Name));
        }
    }
}