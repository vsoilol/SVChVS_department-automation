using DepartmentAutomation.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;

namespace DepartmentAutomation.Application.Common.Attributes
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params Role[] allowedRoles)
        {
            var allowedRolesAsStrings = allowedRoles.Select(x => Enum.GetName(typeof(Role), x));
            Roles = string.Join(",", allowedRolesAsStrings);
        }
    }
}