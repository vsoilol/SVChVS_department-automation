using DepartmentAutomation.Application.Common.Mappings;

namespace DepartmentAutomation.Application.Common.Models.WordDocument
{
    public class Audience : IMapFrom<Domain.Entities.Audience>
    {
        public int Number { get; set; }

        /// <summary>
        ///     Номер корпуса
        /// </summary>
        public int BuildingNumber { get; set; }

        /// <summary>
        ///     Пример: ПУЛ-4/517.2-21
        /// </summary>
        public string RegistrationNumber { get; set; }
    }
}