using System;
using DepartmentAutomation.Application.Common.Mappings;

namespace DepartmentAutomation.Application.Common.Models.WordDocument
{
    public class Protocol : IMapFrom<Domain.Entities.Protocol>
    {
        public DateTime Date { get; set; }

        public int Number { get; set; }
    }
}