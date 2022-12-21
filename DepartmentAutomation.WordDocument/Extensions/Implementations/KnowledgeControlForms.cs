using System.Collections.Generic;
using DepartmentAutomation.Application.Common.Models.WordDocument;
using DepartmentAutomation.WordDocument.Extensions.Interfaces;
using DepartmentAutomation.WordDocument.Helpers.Interfaces;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DepartmentAutomation.WordDocument.Extensions.Implementations
{
    internal class KnowledgeControlForms : IKnowledgeControlForms
    {
        private readonly IWordprocessingHelper _wordprocessingHelper;

        public KnowledgeControlForms(IWordprocessingHelper wordprocessingHelper)
        {
            _wordprocessingHelper = wordprocessingHelper;
        }

        public void CreateKnowledgeControlForms(Body body, IReadOnlyList<KnowledgeControlForm> knowledgeControlForms)
        {
            var paragraphAfter = _wordprocessingHelper.GetElementByInnerText<Paragraph>(body, "Текущий контроль");

            foreach (var controlForm in knowledgeControlForms)
            {
                paragraphAfter.InsertAfterSelf(_wordprocessingHelper
                    .CreateParagraphWithText($"{controlForm.ShortName} - {controlForm.Name}."));
            }
        }
    }
}