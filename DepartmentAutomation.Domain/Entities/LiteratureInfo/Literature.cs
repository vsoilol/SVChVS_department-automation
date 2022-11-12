using DepartmentAutomation.Domain.Contracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DepartmentAutomation.Domain.Entities.LiteratureInfo
{
    public class Literature : Entity<int>
    {
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Гриф
        /// </summary>
        [Required]
        public string Recommended { get; set; }

        /// <summary>
        /// Количество экземпляров
        /// </summary>
        [Required]
        public string SetNumber { get; set; }

        public virtual List<LiteratureTypeInfo> LiteratureTypeInfos { get; set; }
    }
}