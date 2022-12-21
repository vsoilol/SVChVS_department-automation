namespace DepartmentAutomation.Application.Contracts.Requests.Identity
{
    public class UserLoginByFullNameRequest
    {
        public string Name { get; set; }
        
        public string Surname { get; set; }
        
        public string Patronymic { get; set; }
        
        public string Password { get; set; }
    }
}