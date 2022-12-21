using DepartmentAutomation.Application.Common.Models.WordDocument;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DepartmentAutomation.WordDocument.Extensions.Interfaces
{
    internal interface ICourseProjectDescription
    {
        void CreateCourseProjectDescription(Body body, MainDocumentPart mainDocPart, Paragraph paragraph,
            InformationBlock informationBlock);

        void DeleteCourseProjectDescription(Paragraph paragraph);
    }
}