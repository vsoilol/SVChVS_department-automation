using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using DepartmentAutomation.WordDocument.Helpers.Interfaces;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DepartmentAutomation.Infrastructure.WordDocument.Helpers.Implementations
{
    internal class WordprocessingHelper : IWordprocessingHelper
    {
        public Paragraph CreateParagraphWithText(string text,
            string fontSize = "24",
            JustificationValues justificationValues = JustificationValues.Left)
        {
            return new Paragraph(
                new ParagraphProperties(new Justification { Val = justificationValues }),
                new Run(new RunProperties(new FontSize { Val = fontSize }),
                    new Text(text)));
        }

        public Paragraph CreateParagraphWithText(string text, Body body, RunProperties runProperties)
        {
            return new Paragraph(
                GetMainParagraphProperties(body),
                new Run(runProperties,
                    new Text(text)));
        }

        public Paragraph CreateParagraphWithText(string text, Body body)
        {
            return new Paragraph(
                GetMainParagraphProperties(body),
                new Run(new Text(text)));
        }

        public AltChunk GenerateAltChunkFromHtml(MainDocumentPart mainDocPart, string html)
        {
            var altChunkId = GetUniqueKey(4);

            var ms = new MemoryStream(Encoding.UTF8.GetBytes(html));

            var formatImportPart =
                mainDocPart.AddAlternativeFormatImportPart(
                    AlternativeFormatImportPartType.Html, altChunkId);

            formatImportPart.FeedData(ms);
            var altChunk = new AltChunk();
            altChunk.Id = altChunkId;

            return altChunk;
        }

        public Paragraph GetNewLine(Body body)
        {
            return body.Descendants<Paragraph>()
                .FirstOrDefault(c => c.InnerText.Contains("Требования к освоению учебной дисциплины"))
                .ElementsAfter()
                .First()
                .CloneNode(true) as Paragraph;
        }

        public T GetElementByInnerText<T>(OpenXmlElement parentElement, string text)
            where T : OpenXmlElement
        {
            return parentElement.Descendants<T>()
                .FirstOrDefault(c => c.InnerText.Contains(text));
        }

        public T GetElementByInnerText<T>(IReadOnlyList<OpenXmlElement> elements, string text)
            where T : OpenXmlElement
        {
            return elements.FirstOrDefault(_ => _.InnerText.Contains(text)) as T;
        }

        public void PasteTextIntoMark(Body body, string mark, string text)
        {
            var element = body.Descendants<Text>()
                .FirstOrDefault(c => c.Text.Contains(mark));
            element.Text = text;
        }

        public void PasteTextIntoMark(IReadOnlyList<OpenXmlElement> elements, string mark, string text)
        {
            var element = elements.FirstOrDefault(_ => _.InnerText.Contains(mark))
                .Descendants<Text>()
                .FirstOrDefault(c => c.Text.Contains(mark));
            element.Text = text;
        }

        public Paragraph CreateEmptyParagraph(string fontSize = "24")
        {
            var para = new Paragraph();
            var paraProps = new ParagraphProperties();
            var paraRunProps = new ParagraphMarkRunProperties();
            var runStyl = new RunStyle { Val = "Table9Point" };
            paraRunProps.Append(runStyl);
            var runFont = new FontSize { Val = fontSize };
            paraRunProps.Append(runFont);
            paraProps.Append(paraRunProps);
            para.Append(paraProps);

            return para;
        }

        public void DeleteNewLineBeforeParagraph(Paragraph paragraph)
        {
            var newLine = paragraph.ElementsBefore().Last();
            newLine.Remove();
        }

        public Paragraph CreateBoldParagraph(string text, string fontSize = "24",
            JustificationValues justificationValues = JustificationValues.Left)
        {
            return new Paragraph(
                new ParagraphProperties(new Justification { Val = justificationValues }),
                new Run(new RunProperties(new FontSize { Val = fontSize }, new Bold()),
                    new Text(text)));
        }

        public Paragraph CreateBoldParagraphWithTab(Body body, string text, string fontSize = "24",
            JustificationValues justificationValues = JustificationValues.Left)
        {
            return new Paragraph(
                GetMainParagraphProperties(body),
                new Run(new RunProperties(new FontSize { Val = fontSize }, new Bold()),
                    new Text(text)));
        }

        public Paragraph CreateItalicParagraph(string text, string fontSize = "24",
            JustificationValues justificationValues = JustificationValues.Left)
        {
            var para = new Paragraph(new ParagraphProperties(new Justification { Val = justificationValues }));

            var run = new Run(new RunProperties(new FontSize { Val = fontSize }, new Italic()), new Text(text));
            para.Append(run);

            return para;
        }

        private ParagraphProperties GetMainParagraphProperties(Body body)
        {
            return body.Descendants<Paragraph>()
                .FirstOrDefault(c => c.InnerText.Contains("Требования к освоению учебной дисциплины"))
                .ParagraphProperties
                .CloneNode(true) as ParagraphProperties;
        }

        private string GetUniqueKey(int size)
        {
            var chars =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            var data = new byte[size];
            using (var crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }

            var result = new StringBuilder(size + 2);
            result.Append("id");
            foreach (var b in data)
            {
                result.Append(chars[b % chars.Length]);
            }

            return result.ToString();
        }
    }
}