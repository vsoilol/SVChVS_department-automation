using System;
using DepartmentAutomation.Application.Common.Mappings;

namespace DepartmentAutomation.Application.Common.Models.WordDocument
{
    public class Curriculum : IMapFrom<Domain.Entities.Curriculum>
    {
        public string RegistrationNumber { get; set; }

        public DateTime ApprovalDate { get; set; }
    }
}