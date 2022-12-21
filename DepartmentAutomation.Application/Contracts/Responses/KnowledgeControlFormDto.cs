using DepartmentAutomation.Application.Common.Mappings;
using DepartmentAutomation.Domain.Entities.KnowledgeControlFormInfo;

namespace DepartmentAutomation.Application.Contracts.Responses
{
    public class KnowledgeControlFormDto : IMapFrom<KnowledgeControlForm>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }
    }
}