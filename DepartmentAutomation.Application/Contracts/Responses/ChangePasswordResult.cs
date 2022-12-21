using System.Collections.Generic;

namespace DepartmentAutomation.Application.Contracts.Responses
{
    public class ChangePasswordResult
    {
        public string NewPassword { get; set; }

        public bool Success { get; set; }

        public IEnumerable<string> Errors { get; set; }
    }
}