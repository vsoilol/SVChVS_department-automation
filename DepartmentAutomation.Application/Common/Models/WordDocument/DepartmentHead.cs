using DepartmentAutomation.Application.Common.Mappings;

namespace DepartmentAutomation.Application.Common.Models.WordDocument
{
    public class DepartmentHead : IMapFrom<Domain.Entities.DepartmentInfo.DepartmentHead>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }
    }
}