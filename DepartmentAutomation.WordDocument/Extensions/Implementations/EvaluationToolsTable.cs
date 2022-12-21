using System.Collections.Generic;
using System.Linq;
using DepartmentAutomation.Application.Common.Models.WordDocument;
using DepartmentAutomation.WordDocument.Extensions.Interfaces;
using DepartmentAutomation.WordDocument.Helpers.Interfaces;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DepartmentAutomation.WordDocument.Extensions.Implementations
{
    internal class EvaluationToolsTable : IEvaluationToolsTable
    {
        private const string TableFontSize = "20";
        private readonly ITableHelper _tableHelper;

        private readonly IWordprocessingHelper _wordprocessingHelper;

        public EvaluationToolsTable(IWordprocessingHelper wordprocessingHelper, ITableHelper tableHelper)
        {
            _wordprocessingHelper = wordprocessingHelper;
            _tableHelper = tableHelper;
        }

        public void CreateEvaluationToolsTable(Body body, IReadOnlyList<EvaluationTool> evaluationTools)
        {
            var table = _wordprocessingHelper.GetElementByInnerText<Table>(body, "Вид оценочных средств");

            for (var i = 0; i < evaluationTools.Count(); i++)
            {
                PasteEvaluationToolInfo(table, evaluationTools.ElementAt(i), i + 1);
            }
        }

        private void PasteEvaluationToolInfo(Table table, EvaluationTool evaluationTool, int number)
        {
            var row = new TableRow();

            var numberCell =
                _tableHelper.CreateCellWithFontSizeAndJustification(number.ToString(), JustificationValues.Center,
                    TableFontSize);
            row.Append(numberCell);

            var evaluationToolNameCell = _tableHelper.CreateCellWithFontSizeAndJustification(evaluationTool.Name,
                JustificationValues.Left, TableFontSize);
            row.Append(evaluationToolNameCell);

            var setNumberCell = _tableHelper.CreateCellWithFontSizeAndJustification(evaluationTool.SetNumber.ToString(),
                JustificationValues.Center, TableFontSize);
            row.Append(setNumberCell);

            table.Append(row);
        }
    }
}