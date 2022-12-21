using DepartmentAutomation.Application.Common.Models;
using System.Threading.Tasks;
using DepartmentAutomation.Application.Contracts.Responses;
using DepartmentAutomation.Application.Contracts.Responses.Common;
using DepartmentAutomation.Application.Contracts.Responses.Identity;

namespace DepartmentAutomation.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        //Task<AuthenticationResult> RegisterAsync(string email, string password);

        Task<AuthenticationResult> LoginAsync(string email, string password);
        
        Task<AuthenticationResult> LoginByFullNameAsync(string name, string surname, string patronymic, string password);

        Task<ChangePasswordResult> ChangePasswordAsync(string email, string newPassword);

        Task<ChangePasswordResult> ChangePasswordAsync(string id);

        Task<ResultInfo> ActivateUserAsync(string id);

        Task<ResultInfo> DeactivateUser(string id);
    }
}