using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Domain.Contracts;
using FluentValidation.Validators;

namespace DepartmentAutomation.Application.Validators.PropertyValidators
{
    public class SqlIdValidatorFor<TEntity> : PropertyValidator
        where TEntity : Entity<int>
    {
        private readonly IApplicationDbContext _context;

        public SqlIdValidatorFor(IApplicationDbContext context)
        {
            _context = context;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var entity = _context.Exists<TEntity>((int)context.PropertyValue);

            return entity;
        }

        protected override string GetDefaultMessageTemplate()
            => "'Id' must be a valid.";

    }
}