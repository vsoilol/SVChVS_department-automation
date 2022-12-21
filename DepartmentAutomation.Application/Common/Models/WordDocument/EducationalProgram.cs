using System.Collections.Generic;

namespace DepartmentAutomation.Application.Common.Models.WordDocument
{
    public class EducationalProgram
    {
        public Discipline Discipline { get; set; }

        public List<InformationBlock> InformationBlocks { get; set; }

        public List<Lesson> Lectures { get; set; }

        public List<KnowledgeControlForm> KnowledgeControlForms { get; set; }

        public List<TrainingCourseForm> TrainingCourseForms { get; set; }

        public List<EvaluationTool> EvaluationTools { get; set; }

        public List<Literature> MainLiteratures { get; set; }

        public List<Literature> AdditionalLiteratures { get; set; }

        public List<MethodicalRecommendation> MethodicalRecommendations { get; set; }

        public List<Protocol> Protocols { get; set; }

        public List<Audience> Audiences { get; set; }

        public Reviewer Reviewer { get; set; }
    }
}