

using System.Collections.Generic;
using DepartmentAutomation.Domain.Contracts;

namespace DepartmentAutomation.Domain.Entities.DepartmentInfo
{
    public class Faculty : Entity<int>
    {
        public string Name { get; set; }

        public List<Specialty> Specialties { get; set; }
    }
}