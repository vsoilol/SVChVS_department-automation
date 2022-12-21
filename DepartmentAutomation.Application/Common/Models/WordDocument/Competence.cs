using DepartmentAutomation.Application.Common.Mappings;

namespace DepartmentAutomation.Application.Common.Models.WordDocument
{
    public class Competence : IMapFrom<Domain.Entities.CompetenceInfo.Competence>
    {
        public string Code { get; set; }

        public string Name { get; set; }
    }
}