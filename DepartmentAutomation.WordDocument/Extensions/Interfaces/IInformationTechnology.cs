using System.Collections.Generic;
using DepartmentAutomation.Application.Common.Models.WordDocument;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DepartmentAutomation.WordDocument.Extensions.Interfaces
{
    internal interface IInformationTechnology
    {
        void CreateInformationTechnology(Body body, IReadOnlyList<Lesson> lessons);
    }
}