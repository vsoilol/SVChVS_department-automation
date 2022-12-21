using DepartmentAutomation.Application.Common.Models.WordDocument;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DepartmentAutomation.WordDocument.Extensions.Interfaces
{
    public interface IAnnotationInfo
    {
        void PasteAnnotationInfo(Body body, MainDocumentPart documentPart, EducationalProgram educationalProgram);
    }
}