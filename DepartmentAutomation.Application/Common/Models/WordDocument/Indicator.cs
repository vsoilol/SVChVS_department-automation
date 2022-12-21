using System.Collections.Generic;
using DepartmentAutomation.Application.Common.Mappings;

namespace DepartmentAutomation.Application.Common.Models.WordDocument
{
    public class Indicator : IMapFrom<Domain.Entities.CompetenceInfo.Indicator>
    {
        public int Number { get; set; }

        public string Description { get; set; }

        public Competence Competence { get; set; }

        public List<CompetenceFormationLevel> CompetenceFormationLevels { get; set; }
    }
}