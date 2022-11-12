using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DepartmentAutomation.Domain.Entities.DepartmentInfo;
using Microsoft.AspNetCore.Identity;

namespace DepartmentAutomation.Domain.Entities.UserInfo
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Surname { get; set; }

        [Required]
        public string Patronymic { get; set; }

        [Required] 
        public bool IsActive { get; set; } = true;
        
        public int? DepartmentId { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public virtual Department Department { get; set; }
    }
}