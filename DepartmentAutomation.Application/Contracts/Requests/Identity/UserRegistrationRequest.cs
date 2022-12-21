namespace DepartmentAutomation.Application.Contracts.Requests.Identity
{
    public class UserRegistrationRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}