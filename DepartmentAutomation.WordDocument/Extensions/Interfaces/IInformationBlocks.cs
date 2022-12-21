using System.Collections.Generic;
using DepartmentAutomation.Application.Common.Models.WordDocument;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DepartmentAutomation.WordDocument.Extensions.Interfaces
{
    internal interface IInformationBlocks
    {
        void CreateInformationBlocks(Body body, MainDocumentPart mainDocPart,
            IReadOnlyList<InformationBlock> informationBlocks, string textBefore);
    }
}