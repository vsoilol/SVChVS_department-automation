using System.Linq;
using DepartmentAutomation.WordDocument.Helpers.Interfaces;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DepartmentAutomation.WordDocument.Helpers.Implementations
{
    internal class TableHelper : ITableHelper
    {
        private readonly IWordprocessingHelper _wordprocessingHelper;

        public TableHelper(IWordprocessingHelper wordprocessingHelper)
        {
            _wordprocessingHelper = wordprocessingHelper;
        }

        public TableCell CreateCellWithAlignment(
            TableVerticalAlignmentValues alignment = TableVerticalAlignmentValues.Top)
        {
            return new TableCell(new TableCellProperties(new TableCellVerticalAlignment { Val = alignment }));
        }

        public TableCell CreateCellWithFontSizeAndJustification(string text,
            JustificationValues justificationValues,
            string fontSize = "24",
            TableVerticalAlignmentValues alignment = TableVerticalAlignmentValues.Top)
        {
            var cell = CreateCellWithAlignment(alignment);
            cell.Append(_wordprocessingHelper.CreateParagraphWithText(text, fontSize, justificationValues));

            return cell;
        }

        public TableCell CreateCellWithParagraph(Paragraph paragraph,
            TableVerticalAlignmentValues alignment = TableVerticalAlignmentValues.Top)
        {
            var cell = CreateCellWithAlignment(alignment);
            cell.Append(paragraph);
            return cell;
        }

        public TableCell CreateEmptyCell(string fontSize = "24")
        {
            return new TableCell(_wordprocessingHelper.CreateEmptyParagraph(fontSize));
        }

        public void DeleteCell(TableRow row, string innerText)
        {
            var cell = _wordprocessingHelper.GetElementByInnerText<TableCell>(row, innerText);
            cell.Remove();
        }

        public void DeleteTableAfterText(Body body, string text)
        {
            var paragraph = _wordprocessingHelper.GetElementByInnerText<Paragraph>(body, text);

            var table = paragraph.ElementsAfter().First() as Table;
            var newLine = paragraph.ElementsBefore().Last() as Paragraph;

            newLine.Remove();
            paragraph.Remove();
            table.Remove();
        }

        public void PasteTextIntoCell(TableCell cell, string text)
        {
            var run = cell.GetFirstChild<Paragraph>().GetFirstChild<Run>();
            run.Append(new Text(text));
        }
    }
}