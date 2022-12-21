using DepartmentAutomation.Application.Common.Models.WordDocument;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DepartmentAutomation.WordDocument.Extensions.Interfaces
{
    internal interface IFederalStateEducationalStandardInfo
    {
        void CreateFederalStateEducationalStandardInfo(Body body, Discipline discipline);
    }
}