using DepartmentAutomation.Application.Common.Models.WordDocument;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DepartmentAutomation.WordDocument.Extensions.Interfaces
{
    internal interface IWeeksTable
    {
        void CreateWeeksTable(Body body, Discipline discipline);
    }
}