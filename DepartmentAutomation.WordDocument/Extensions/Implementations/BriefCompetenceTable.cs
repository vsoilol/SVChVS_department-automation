using System.Collections.Generic;
using DepartmentAutomation.Application.Common.Models.WordDocument;
using DepartmentAutomation.WordDocument.Extensions.Interfaces;
using DepartmentAutomation.WordDocument.Helpers.Interfaces;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DepartmentAutomation.WordDocument.Extensions.Implementations
{
    internal class BriefCompetenceTable : IBriefCompetenceTable
    {
        private readonly ITableHelper _tableHelper;
        private readonly IWordprocessingHelper _wordprocessingHelper;

        public BriefCompetenceTable(ITableHelper tableHelper, IWordprocessingHelper wordprocessingHelper)
        {
            _tableHelper = tableHelper;
            _wordprocessingHelper = wordprocessingHelper;
        }

        public void CreateBriefCompetenceTable(Body body, IReadOnlyList<Indicator> indicators)
        {
            var competenceTable =
                _wordprocessingHelper.GetElementByInnerText<Table>(body, "Коды формируемых компетенций");

            foreach (var indicator in indicators)
            {
                var row = new TableRow();

                var competenceName = _tableHelper
                    .CreateCellWithFontSizeAndJustification(indicator.Competence.Code, JustificationValues.Both);

                var competenceDescription = _tableHelper
                    .CreateCellWithFontSizeAndJustification(indicator.Competence.Name, JustificationValues.Both);

                row.Append(competenceName, competenceDescription);
                competenceTable.Append(row);
            }
        }
    }
}