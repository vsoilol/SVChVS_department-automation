using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DepartmentAutomation.Domain.Contracts;
using DepartmentAutomation.Domain.Entities.DepartmentInfo;
using DepartmentAutomation.Domain.Entities.UserInfo;

namespace DepartmentAutomation.Domain.Entities.TeacherInformation
{
    public class Teacher : Entity<int>
    {
        public int? AcademicDegreeId { get; set; }

        [ForeignKey(nameof(AcademicDegreeId))]
        public virtual AcademicDegree AcademicDegree { get; set; }

        public int? AcademicRankId { get; set; }

        [ForeignKey(nameof(AcademicRankId))]
        public virtual AcademicRank AcademicRank { get; set; }

        [Required]
        public int PositionId { get; set; }

        [ForeignKey(nameof(PositionId))]
        public virtual Position Position { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public virtual Department Department { get; set; }

        public virtual List<Discipline> Disciplines { get; set; }
        
        [Required]
        public string ApplicationUserId { get; set; }
        
        [ForeignKey(nameof(ApplicationUserId))]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}