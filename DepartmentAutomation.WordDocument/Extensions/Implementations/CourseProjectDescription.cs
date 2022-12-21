using System.Linq;
using DepartmentAutomation.Application.Common.Models.WordDocument;
using DepartmentAutomation.WordDocument.Extensions.Interfaces;
using DepartmentAutomation.WordDocument.Helpers.Interfaces;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DepartmentAutomation.Infrastructure.WordDocument.Extensions.Implementations
{
    internal class CourseProjectDescription : ICourseProjectDescription
    {
        private readonly IWordprocessingHelper _wordprocessingHelper;

        public CourseProjectDescription(IWordprocessingHelper wordprocessingHelper)
        {
            _wordprocessingHelper = wordprocessingHelper;
        }

        public void CreateCourseProjectDescription(Body body, MainDocumentPart mainDocPart, Paragraph paragraph,
            InformationBlock informationBlock)
        {
            paragraph = paragraph.InsertAfterSelf(_wordprocessingHelper.GetNewLine(body));
            var altChunk = _wordprocessingHelper.GenerateAltChunkFromHtml(mainDocPart, informationBlock.Content);
            paragraph.InsertAfterSelf(altChunk);

            var paragraphAfterNewLine =
                _wordprocessingHelper.GetElementByInnerText<Paragraph>(body, "Итоговая оценка курсового проекта");
            _wordprocessingHelper.DeleteNewLineBeforeParagraph(paragraphAfterNewLine);
        }

        public void DeleteCourseProjectDescription(Paragraph paragraph)
        {
            var emptyParagraph = paragraph.ElementsBefore().Last();
            emptyParagraph.Remove();

            var elementsForDelete = paragraph.ElementsAfter().Take(4).ToList();

            foreach (var element in elementsForDelete)
            {
                element.Remove();
            }

            paragraph.Remove();
        }
    }
}