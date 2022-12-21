using DepartmentAutomation.Application.Common.Mappings;
using DepartmentAutomation.Domain.Entities;

namespace DepartmentAutomation.Application.Contracts.Responses
{
    public class LessonDto : IMapFrom<Lesson>
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public string Name { get; set; }

        public int Hours { get; set; }

        public string Content { get; set; }
    }
}