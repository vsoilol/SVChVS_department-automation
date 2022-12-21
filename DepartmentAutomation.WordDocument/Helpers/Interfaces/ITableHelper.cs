using DocumentFormat.OpenXml.Wordprocessing;

namespace DepartmentAutomation.WordDocument.Helpers.Interfaces
{
    internal interface ITableHelper
    {
        TableCell CreateCellWithFontSizeAndJustification(string text,
            JustificationValues justificationValues,
            string fontSize = "24",
            TableVerticalAlignmentValues alignment = TableVerticalAlignmentValues.Top);

        TableCell CreateCellWithAlignment(TableVerticalAlignmentValues alignment = TableVerticalAlignmentValues.Top);

        void DeleteTableAfterText(Body body, string text);

        void DeleteCell(TableRow row, string innerText);

        TableCell CreateEmptyCell(string fontSize = "24");

        TableCell CreateCellWithParagraph(Paragraph paragraph,
            TableVerticalAlignmentValues alignment = TableVerticalAlignmentValues.Top);

        void PasteTextIntoCell(TableCell cell, string text);
    }
}