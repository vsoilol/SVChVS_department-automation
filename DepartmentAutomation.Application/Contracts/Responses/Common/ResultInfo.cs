using System.Collections.Generic;

namespace DepartmentAutomation.Application.Contracts.Responses.Common
{
    public class ResultInfo
    {
        public bool Success { get; set; }

        public IEnumerable<string> Errors { get; set; }
    }
}