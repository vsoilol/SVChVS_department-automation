using DepartmentAutomation.Application.Contracts.Requests;
using DepartmentAutomation.Application.Contracts.Requests.Identity;
using Swashbuckle.AspNetCore.Filters;

namespace DepartmentAutomation.Web.SwaggerExamples.Requests
{
    public class UserRegistrationRequestExample : IExamplesProvider<UserRegistrationRequest>
    {
        public UserRegistrationRequest GetExamples()
        {
            return new UserRegistrationRequest
            {
                Email = "email@mail.com",
                Password = "S@1tring",
            };
        }
    }
}