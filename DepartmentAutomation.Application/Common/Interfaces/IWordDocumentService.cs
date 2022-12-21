using DepartmentAutomation.Application.Common.Models.WordDocument;

namespace DepartmentAutomation.Application.Common.Interfaces
{
    public interface IWordDocumentService
    {
        byte[] GenerateDocument(EducationalProgram educationalProgram);
    }
}