using System;
using DepartmentAutomation.Application.Common.Mappings;

namespace DepartmentAutomation.Application.Common.Models.WordDocument
{
    public class FederalStateEducationalStandard : IMapFrom<Domain.Entities.FederalStateEducationalStandard>
    {
        public string Code { get; set; }

        public DateTime Date { get; set; }
    }
}