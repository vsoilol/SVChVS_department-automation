using System.Collections.Generic;
using DepartmentAutomation.Application.Common.Models.WordDocument;
using DepartmentAutomation.WordDocument.Extensions.Interfaces;
using DepartmentAutomation.WordDocument.Helpers.Interfaces;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DepartmentAutomation.WordDocument.Extensions.Implementations
{
    internal class LecturesTable : ILecturesTable
    {
        private const string TableFontSize = "20";
        private readonly ITableHelper _tableHelper;

        private readonly IWordprocessingHelper _wordprocessingHelper;

        public LecturesTable(ITableHelper tableHelper, IWordprocessingHelper wordprocessingHelper)
        {
            _tableHelper = tableHelper;
            _wordprocessingHelper = wordprocessingHelper;
        }

        public void CreateLecturesTable(Body body, IReadOnlyList<Lesson> lessons)
        {
            var table = _wordprocessingHelper.GetElementByInnerText<Table>(body, "Номер тем");

            foreach (var lecture in lessons)
            {
                var row = new TableRow();

                var number = _tableHelper
                    .CreateCellWithFontSizeAndJustification(lecture.Number.ToString(), JustificationValues.Center,
                        TableFontSize);

                var name = _tableHelper
                    .CreateCellWithFontSizeAndJustification(lecture.Name, JustificationValues.Both, TableFontSize);

                var content = _tableHelper
                    .CreateCellWithFontSizeAndJustification(lecture.Content, JustificationValues.Both, TableFontSize);

                var competences = new TableCell();

                lecture.Competences.ForEach(_ =>
                {
                    var paragraph = _wordprocessingHelper
                        .CreateParagraphWithText(_.Code, TableFontSize, JustificationValues.Both);
                    competences.Append(paragraph);
                });

                row.Append(number, name, content, competences);
                table.Append(row);
            }
        }
    }
}