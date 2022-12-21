using System.Collections.Generic;
using DepartmentAutomation.Application.Common.Models.WordDocument;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DepartmentAutomation.WordDocument.Extensions.Interfaces
{
    internal interface IEvaluationToolsTable
    {
        public void CreateEvaluationToolsTable(Body body, IReadOnlyList<EvaluationTool> evaluationTools);
    }
}