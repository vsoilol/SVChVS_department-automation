using AutoMapper;
using DepartmentAutomation.Application.Common.Mappings;

namespace DepartmentAutomation.Application.Common.Models.WordDocument
{
    public class Department : IMapFrom<Domain.Entities.DepartmentInfo.Department>
    {
        public int Id { get; set; }

        /// <summary>
        ///     Пример: Программное обеспечение информационных технологий
        /// </summary>
        public string Name { get; set; }

        public string ShortName { get; set; }

        public DepartmentHead DepartmentHead { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.DepartmentInfo.Department, Department>()
                .ForMember(dto => dto.DepartmentHead,
                    opt => opt.MapFrom(x => x.DepartmentHead));
        }
    }
}