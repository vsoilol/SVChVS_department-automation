using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using DepartmentAutomation.Domain.Contracts;

namespace DepartmentAutomation.Domain.Entities.UserInfo
{
    public class RefreshToken : Entity<Guid>
    {
        public string Token { get; set; }

        public string JwtId { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public bool Used { get; set; }

        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; }
    }
}