using System.Collections.Generic;
using DepartmentAutomation.Application.Common.Models.WordDocument;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DepartmentAutomation.WordDocument.Extensions.Interfaces
{
    internal interface IBriefCompetenceTable
    {
        void CreateBriefCompetenceTable(Body body, IReadOnlyList<Indicator> indicators);
    }
}