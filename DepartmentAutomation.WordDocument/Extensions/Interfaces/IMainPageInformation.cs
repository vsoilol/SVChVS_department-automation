using DepartmentAutomation.Application.Common.Models.WordDocument;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DepartmentAutomation.WordDocument.Extensions.Interfaces
{
    internal interface IMainPageInformation
    {
        void CreateMainPage(Body body, Discipline discipline);
    }
}