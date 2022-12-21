using DepartmentAutomation.Application.Common.Mappings;
using DepartmentAutomation.Domain.Entities.SemesterInfo;

namespace DepartmentAutomation.Application.Contracts.Responses
{
    public class SemesterDto : IMapFrom<Semester>
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public int WeeksNumber { get; set; }

        public int CourseNumber { get; set; }
    }
}