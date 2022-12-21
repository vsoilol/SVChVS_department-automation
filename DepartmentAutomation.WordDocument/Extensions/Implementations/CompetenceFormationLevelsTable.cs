using System.Collections.Generic;
using System.Linq;
using DepartmentAutomation.Application.Common.Models.WordDocument;
using DepartmentAutomation.Domain.Enums;
using DepartmentAutomation.WordDocument.Extensions.Interfaces;
using DepartmentAutomation.WordDocument.Helpers.Interfaces;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DepartmentAutomation.WordDocument.Extensions.Implementations
{
    internal class CompetenceFormationLevelsTable : ICompetenceFormationLevelsTable
    {
        private const string TableFontSize = "22";
        private readonly ITableHelper _tableHelper;

        private readonly IWordprocessingHelper _wordprocessingHelper;

        public CompetenceFormationLevelsTable(ITableHelper tableHelper, IWordprocessingHelper wordprocessingHelper)
        {
            _tableHelper = tableHelper;
            _wordprocessingHelper = wordprocessingHelper;
        }

        public void CreateCompetenceFormationLevelsTable(Body body, IReadOnlyList<Indicator> indicators)
        {
            var mainTable =
                _wordprocessingHelper.GetElementByInnerText<Table>(body, "Уровни сформированности компетенции");
            var additionalTable = _wordprocessingHelper.GetElementByInnerText<Table>(body, "Оценочные средства");

            foreach (var indicator in indicators)
            {
                CreateRowsWithCompetenceAndIndicatorInfo(mainTable, additionalTable, indicator);

                CreateRowsWithIndicatorLevelInfo(mainTable, additionalTable, indicator);
            }
        }

        private void CreateRowsWithCompetenceAndIndicatorInfo(Table mainTable, Table additionalTable,
            Indicator indicator)
        {
            CreateRowWithCompetenceInfo(mainTable, indicator, 4);
            CreateRowWithCompetenceInfo(additionalTable, indicator, 2);

            CreateRowWithIndicatorInfo(mainTable, indicator, 4);
        }

        private void CreateRowsWithIndicatorLevelInfo(Table mainTable, Table additionalTable, Indicator indicator)
        {
            foreach (var competenceFormationLevel in indicator.CompetenceFormationLevels)
            {
                CreateRowWithLevelInfoIntoMainTable(mainTable, competenceFormationLevel);
                CreateRowWithLevelInfoIntoAdditionalTable(additionalTable, competenceFormationLevel);
            }
        }

        private void CreateRowWithLevelInfoIntoMainTable(Table table, CompetenceFormationLevel competenceFormationLevel)
        {
            var row = new TableRow();

            var levelNumberCell = _tableHelper
                .CreateCellWithFontSizeAndJustification(competenceFormationLevel.LevelNumber.ToString(),
                    JustificationValues.Left, TableFontSize);

            var levelNameCell = _tableHelper
                .CreateCellWithFontSizeAndJustification(
                    $"{GetLevelName(competenceFormationLevel.FormationLevel)} уровень",
                    JustificationValues.Left, TableFontSize);

            var factualDescriptionCell = _tableHelper
                .CreateCellWithFontSizeAndJustification(competenceFormationLevel.FactualDescription,
                    JustificationValues.Both, TableFontSize);

            var learningOutcomesCell = _tableHelper
                .CreateCellWithFontSizeAndJustification(competenceFormationLevel.LearningOutcomes,
                    JustificationValues.Both, TableFontSize);

            row.Append(levelNumberCell, levelNameCell, factualDescriptionCell, learningOutcomesCell);
            table.Append(row);
        }

        private void CreateRowWithLevelInfoIntoAdditionalTable(Table table,
            CompetenceFormationLevel competenceFormationLevel)
        {
            var row = new TableRow();

            var learningOutcomesCell = _tableHelper
                .CreateCellWithFontSizeAndJustification(competenceFormationLevel.LearningOutcomes,
                    JustificationValues.Both, TableFontSize);
            row.Append(learningOutcomesCell);

            if (competenceFormationLevel.EvaluationToolTypes.Any())
            {
                PasteInfoAboutEvaluationToolTypes(row, competenceFormationLevel.EvaluationToolTypes);
            }
            else
            {
                row.Append(_tableHelper.CreateEmptyCell());
            }

            table.Append(row);
        }

        private void PasteInfoAboutEvaluationToolTypes(TableRow tableRow,
            IEnumerable<EvaluationToolType> evaluationToolTypes)
        {
            var evaluationToolTypesCell = _tableHelper.CreateCellWithAlignment();

            foreach (var evaluationToolType in evaluationToolTypes)
            {
                evaluationToolTypesCell
                    .Append(_wordprocessingHelper.CreateParagraphWithText(evaluationToolType.Name, TableFontSize));
            }

            tableRow.Append(evaluationToolTypesCell);
        }

        private string GetLevelName(FormationLevel formationLevel)
        {
            switch (formationLevel)
            {
                case FormationLevel.Threshold:
                    return "Пороговый";

                case FormationLevel.Advanced:
                    return "Продвинутый";

                case FormationLevel.High:
                    return "Высокий";

                default:
                    return "";
            }
        }

        private void CreateRowWithCompetenceInfo(Table table, Indicator indicator, int gridSpanValue)
        {
            var row = new TableRow();

            var competenceInfo = $"Компетенция {indicator.Competence.Code}. {indicator.Competence.Name}";
            var cell = _tableHelper.CreateCellWithParagraph(
                _wordprocessingHelper.CreateItalicParagraph(competenceInfo, TableFontSize));
            cell.Append(new TableCellProperties(new GridSpan { Val = gridSpanValue }));

            row.Append(cell);
            table.Append(row);
        }

        private void CreateRowWithIndicatorInfo(Table table, Indicator indicator, int gridSpanValue)
        {
            var row = new TableRow();

            var indicatorInfo =
                $"Индикатор достижения {indicator.Competence.Code}.{indicator.Number}. {indicator.Description}";
            var cell = _tableHelper.CreateCellWithParagraph(
                _wordprocessingHelper.CreateItalicParagraph(indicatorInfo, TableFontSize));
            cell.Append(new TableCellProperties(new GridSpan { Val = gridSpanValue }));

            row.Append(cell);
            table.Append(row);
        }
    }
}