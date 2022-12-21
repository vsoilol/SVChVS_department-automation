using DepartmentAutomation.Application.Common.Mappings;

namespace DepartmentAutomation.Application.Common.Models.WordDocument
{
    public class KnowledgeControlForm : IMapFrom<Domain.Entities.KnowledgeControlFormInfo.KnowledgeControlForm>
    {
        public string Name { get; set; }

        public string ShortName { get; set; }
    }
}