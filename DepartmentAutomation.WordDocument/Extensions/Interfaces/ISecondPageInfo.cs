using DepartmentAutomation.Application.Common.Models.WordDocument;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DepartmentAutomation.WordDocument.Extensions.Interfaces
{
    public interface ISecondPageInfo
    {
        void CreateSecondPageInfo(Body body, EducationalProgram educationalProgram);
    }
}