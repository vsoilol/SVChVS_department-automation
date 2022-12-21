using System.Collections.Generic;
using System.Linq;
using DepartmentAutomation.Application.Common.Models.WordDocument;
using DepartmentAutomation.WordDocument.Extensions.Interfaces;
using DepartmentAutomation.WordDocument.Helpers.Interfaces;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DepartmentAutomation.WordDocument.Extensions.Implementations
{
    internal class InformationBlocks : IInformationBlocks
    {
        private readonly IWordprocessingHelper _wordprocessingHelper;

        public InformationBlocks(IWordprocessingHelper wordprocessingHelper)
        {
            _wordprocessingHelper = wordprocessingHelper;
        }

        public void CreateInformationBlocks(Body body, MainDocumentPart mainDocPart,
            IReadOnlyList<InformationBlock> informationBlocks, string textBefore)
        {
            var paragraph = _wordprocessingHelper.GetElementByInnerText<Paragraph>(body, textBefore);

            var paragraphAfter = paragraph.InsertAfterSelf(_wordprocessingHelper.GetNewLine(body));

            paragraph.Remove();

            informationBlocks
                .OrderBy(_ => _.Number)
                .ToList()
                .ForEach(block =>
                {
                    var altChunk = _wordprocessingHelper.GenerateAltChunkFromHtml(mainDocPart, block.Content);
                    var altChunckBlock = paragraphAfter.InsertAfterSelf(altChunk);

                    var para = _wordprocessingHelper
                        .CreateParagraphWithText($"{block.Number} {block.Name}", body,
                            new RunProperties(new Bold()));

                    altChunckBlock.InsertBeforeSelf(para);

                    paragraphAfter = altChunckBlock.InsertAfterSelf(_wordprocessingHelper.GetNewLine(body));
                });
        }
    }
}