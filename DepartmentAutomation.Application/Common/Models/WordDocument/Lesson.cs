using System.Collections.Generic;
using DepartmentAutomation.Application.Common.Mappings;
using DepartmentAutomation.Domain.Enums;

namespace DepartmentAutomation.Application.Common.Models.WordDocument
{
    public class Lesson : IMapFrom<Domain.Entities.Lesson>
    {
        public int Number { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public int Hours { get; set; }

        public LessonType LessonType { get; set; }

        public List<Competence> Competences { get; set; }
    }
}