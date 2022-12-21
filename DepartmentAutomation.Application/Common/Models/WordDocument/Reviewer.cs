using DepartmentAutomation.Application.Common.Mappings;

namespace DepartmentAutomation.Application.Common.Models.WordDocument
{
    public class Reviewer : IMapFrom<Domain.Entities.Reviewer>
    {
        public string Position { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }
    }
}