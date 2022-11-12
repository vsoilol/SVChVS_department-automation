using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DepartmentAutomation.Domain.Contracts;
using DepartmentAutomation.Domain.Entities.DepartmentInfo;

namespace DepartmentAutomation.Domain.Entities
{
    /// <summary>
    /// Федеральный государственный образовательный стандарт
    /// </summary>
    public class FederalStateEducationalStandard : Entity<int>
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public virtual List<Specialty> Specialties { get; set; }
    }
}