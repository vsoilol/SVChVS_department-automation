using AutoMapper;
using DepartmentAutomation.Application.Common.Mappings;

namespace DepartmentAutomation.Application.Common.Models.WordDocument
{
    public class Specialty : IMapFrom<Domain.Entities.DepartmentInfo.Specialty>
    {
        /// <summary>
        ///     Пример: Программная инженерия
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Пример: 09.03.04
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///     Название профиля
        ///     Пример:  Разработка программно-информационных систем
        /// </summary>
        public string ProfileName { get; set; }

        /// <summary>
        ///     Квалификация
        ///     Пример: Бакалавр
        /// </summary>
        public string Qualification { get; set; }

        /// <summary>
        ///     Форма обучения
        ///     Пример: Очная
        /// </summary>
        public string LearningForm { get; set; }

        public FederalStateEducationalStandard FederalStateEducationalStandard { get; set; }

        public Department Department { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.DepartmentInfo.Specialty, Specialty>()
                .ForMember(dto => dto.FederalStateEducationalStandard,
                    opt => opt.MapFrom(x => x.FederalStateEducationalStandard))
                .ForMember(dto => dto.Department,
                    opt => opt.MapFrom(x => x.Department));
        }
    }
}