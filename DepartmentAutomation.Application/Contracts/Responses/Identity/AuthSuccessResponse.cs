﻿namespace DepartmentAutomation.Application.Contracts.Responses.Identity
{
    public class AuthSuccessResponse
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }
    }
}