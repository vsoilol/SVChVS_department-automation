using System.Collections.Generic;
using DepartmentAutomation.Application.Common.Models.WordDocument;
using DepartmentAutomation.WordDocument.Extensions.Interfaces;
using DepartmentAutomation.WordDocument.Helpers.Interfaces;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DepartmentAutomation.WordDocument.Extensions.Implementations
{
    internal class InformationTechnology : IInformationTechnology
    {
        private readonly IWordprocessingHelper _wordprocessingHelper;

        public InformationTechnology(IWordprocessingHelper wordprocessingHelper)
        {
            _wordprocessingHelper = wordprocessingHelper;
        }

        public void CreateInformationTechnology(Body body, IReadOnlyList<Lesson> lessons)
        {
            var paragraph = _wordprocessingHelper.GetElementByInnerText<Paragraph>(body, "Информационные технологии");

            foreach (var lesson in lessons)
            {
                paragraph = paragraph.InsertAfterSelf(_wordprocessingHelper
                    .CreateParagraphWithText($"Тема {lesson.Number}. {lesson.Name}", body, new RunProperties()));
            }
        }
    }
}