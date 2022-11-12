using System.ComponentModel.DataAnnotations;

namespace DepartmentAutomation.Domain.Contracts
{
    public abstract class Entity<T>
    {
        [Key]
        public T Id { get; set; }
    }
}