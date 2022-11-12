using DepartmentAutomation.Domain.Contracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DepartmentAutomation.Domain.Entities.InformationBlockInfo
{
    public class InformationBlock : Entity<int>
    {
        [Required]
        public string Number { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool IsRequired { get; set; }

        public virtual List<InformationBlockContent> InformationBlockContents { get; set; }

        public virtual List<InformationTemplate> InformationTemplates { get; set; }
    }
}