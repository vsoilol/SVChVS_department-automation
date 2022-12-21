using DepartmentAutomation.Application.Common.Extensions;
using DepartmentAutomation.Domain.Contracts;
using FluentValidation.Validators;

namespace DepartmentAutomation.Application.Validators.PropertyValidators
{
    public class PropertyNameValidatorFor<TEntity> : PropertyValidator
        where TEntity : Entity<int>
    {
        protected override bool IsValid(PropertyValidatorContext context)
        {
            var propertyName = (string)context.PropertyValue;
            
            return ReflectionMethods.IsPropertyExist<TEntity>(propertyName);
        }
        
        protected override string GetDefaultMessageTemplate()
            => "Property name must be a valid.";
    }
}