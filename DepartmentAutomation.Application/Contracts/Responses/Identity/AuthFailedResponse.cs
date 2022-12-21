using System.Collections.Generic;

namespace DepartmentAutomation.Application.Contracts.Responses.Identity
{
    public class AuthFailedResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}