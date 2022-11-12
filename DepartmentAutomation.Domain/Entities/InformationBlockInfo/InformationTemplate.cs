using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DepartmentAutomation.Domain.Contracts;

namespace DepartmentAutomation.Domain.Entities.InformationBlockInfo
{
    public class InformationTemplate : Entity<int>
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public int InformationBlockId { get; set; }

        [ForeignKey(nameof(InformationBlockId))]
        public virtual InformationBlock InformationBlock { get; set; }
    }
}