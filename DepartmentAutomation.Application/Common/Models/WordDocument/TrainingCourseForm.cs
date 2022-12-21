using System.Collections.Generic;
using DepartmentAutomation.Application.Common.Mappings;

namespace DepartmentAutomation.Application.Common.Models.WordDocument
{
    public class TrainingCourseForm : IMapFrom<Domain.Entities.TrainingCourseForm>
    {
        public string Name { get; set; }

        public List<Lesson> Lessons { get; set; }
    }
}