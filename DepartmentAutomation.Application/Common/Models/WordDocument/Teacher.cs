using AutoMapper;
using DepartmentAutomation.Application.Common.Mappings;

namespace DepartmentAutomation.Application.Common.Models.WordDocument
{
    public class Teacher : IMapFrom<Domain.Entities.TeacherInformation.Teacher>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        /// <summary>
        ///     Отчество.
        /// </summary>
        public string Patronymic { get; set; }

        public string Position { get; set; }

        public string PositionShort { get; set; }

        public string AcademicDegree { get; set; }

        public string AcademicDegreeShort { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.TeacherInformation.Teacher, Teacher>()
                .ForMember(dto => dto.Position,
                    opt => opt.MapFrom(x => x.Position.Name))
                .ForMember(dto => dto.PositionShort,
                    opt => opt.MapFrom(x => x.Position.ShortName))
                .ForMember(dto => dto.AcademicDegree,
                    opt => opt.MapFrom(x => x.AcademicDegree != null ? x.AcademicDegree.Name : null))
                .ForMember(dto => dto.AcademicDegreeShort,
                    opt => opt.MapFrom(x => x.AcademicDegree != null ? x.AcademicDegree.ShortName : null))
                .ForMember(dto => dto.Name,
                    opt => opt
                        .MapFrom(x => x.ApplicationUser.UserName))
                .ForMember(dto => dto.Surname,
                    opt => opt
                        .MapFrom(x => x.ApplicationUser.Surname))
                .ForMember(dto => dto.Patronymic,
                    opt => opt
                        .MapFrom(x => x.ApplicationUser.Patronymic));
        }
    }
}