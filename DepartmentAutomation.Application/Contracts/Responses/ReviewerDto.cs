using DepartmentAutomation.Application.Common.Mappings;
using DepartmentAutomation.Domain.Entities;

namespace DepartmentAutomation.Application.Contracts.Responses
{
    public class ReviewerDto : IMapFrom<Reviewer>
    {
        public int Id { get; set; }

        public string Position { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }
    }
}