using System.Collections.Generic;
using System.Linq;
using DepartmentAutomation.Application.Common.Models.WordDocument;
using DepartmentAutomation.WordDocument.Extensions.Interfaces;
using DepartmentAutomation.WordDocument.Helpers.Interfaces;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DepartmentAutomation.WordDocument.Extensions.Implementations
{
    internal class LiteraturesTable : ILiteraturesTable
    {
        private const string TableFontSize = "22";
        private readonly ITableHelper _tableHelper;

        private readonly IWordprocessingHelper _wordprocessingHelper;

        public LiteraturesTable(IWordprocessingHelper wordprocessingHelper, ITableHelper tableHelper)
        {
            _wordprocessingHelper = wordprocessingHelper;
            _tableHelper = tableHelper;
        }

        public void CreateLiteraturesTable(Body body, IReadOnlyList<Literature> literatures, string textBeforeTable)
        {
            var paragraphBeforeTable = _wordprocessingHelper.GetElementByInnerText<Paragraph>(body, textBeforeTable);
            var table = paragraphBeforeTable.ElementsAfter().FirstOrDefault(_ => _ is Table) as Table;

            for (var i = 0; i < literatures.Count(); i++)
            {
                CreateRowWithLiteratureInfo(table, literatures.ElementAt(i), i + 1);
            }
        }

        private void CreateRowWithLiteratureInfo(Table table, Literature literature, int number)
        {
            var row = new TableRow();

            var literatureNumberCell = _tableHelper
                .CreateCellWithFontSizeAndJustification(number.ToString(), JustificationValues.Left, TableFontSize);

            var descriptionCell = _tableHelper
                .CreateCellWithFontSizeAndJustification(literature.Description, JustificationValues.Both,
                    TableFontSize);

            var recommendedCell = _tableHelper
                .CreateCellWithFontSizeAndJustification(literature.Recommended, JustificationValues.Both,
                    TableFontSize);

            var setNumberCell = _tableHelper
                .CreateCellWithFontSizeAndJustification(literature.SetNumber, JustificationValues.Center, TableFontSize,
                    TableVerticalAlignmentValues.Center);

            row.Append(literatureNumberCell, descriptionCell, recommendedCell, setNumberCell);
            table.Append(row);
        }
    }
}