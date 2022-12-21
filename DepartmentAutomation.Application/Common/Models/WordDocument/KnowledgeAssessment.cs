using DepartmentAutomation.Application.Common.Mappings;

namespace DepartmentAutomation.Application.Common.Models.WordDocument
{
    public class KnowledgeAssessment : IMapFrom<Domain.Entities.KnowledgeControlFormInfo.KnowledgeAssessment>
    {
        public int MaxMark { get; set; }

        public KnowledgeControlForm KnowledgeControlForm { get; set; }
    }
}