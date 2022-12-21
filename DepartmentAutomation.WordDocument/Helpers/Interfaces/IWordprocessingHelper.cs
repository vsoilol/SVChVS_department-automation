using System.Collections.Generic;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DepartmentAutomation.WordDocument.Helpers.Interfaces
{
    internal interface IWordprocessingHelper
    {
        Paragraph GetNewLine(Body body);

        AltChunk GenerateAltChunkFromHtml(MainDocumentPart mainDocPart, string html);

        Paragraph CreateParagraphWithText(string text,
            string fontSize = "24",
            JustificationValues justificationValues = JustificationValues.Left);

        Paragraph CreateParagraphWithText(string text, Body body, RunProperties runProperties);

        Paragraph CreateParagraphWithText(string text, Body body);

        T GetElementByInnerText<T>(OpenXmlElement parentElement, string text)
            where T : OpenXmlElement;

        T GetElementByInnerText<T>(IReadOnlyList<OpenXmlElement> elements, string text)
            where T : OpenXmlElement;

        void PasteTextIntoMark(IReadOnlyList<OpenXmlElement> elements, string mark, string text);

        void PasteTextIntoMark(Body body, string mark, string text);

        Paragraph CreateEmptyParagraph(string fontSize = "24");

        void DeleteNewLineBeforeParagraph(Paragraph paragraph);

        Paragraph CreateBoldParagraph(string text, string fontSize = "24",
            JustificationValues justificationValues = JustificationValues.Left);

        Paragraph CreateBoldParagraphWithTab(Body body, string text, string fontSize = "24",
            JustificationValues justificationValues = JustificationValues.Left);

        Paragraph CreateItalicParagraph(string text, string fontSize = "24",
            JustificationValues justificationValues = JustificationValues.Left);
    }
}