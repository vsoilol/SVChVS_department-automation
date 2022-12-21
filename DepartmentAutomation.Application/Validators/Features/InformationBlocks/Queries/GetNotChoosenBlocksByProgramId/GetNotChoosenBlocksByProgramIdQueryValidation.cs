using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.InformationBlocks.Queries.GetNotChoosenBlocksByProgramId;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.InformationBlocks.Queries.GetNotChoosenBlocksByProgramId
{
    public class GetNotChoosenBlocksByProgramIdQueryValidation : AbstractValidator<GetNotChoosenBlocksByProgramIdQuery>
    {
        public GetNotChoosenBlocksByProgramIdQueryValidation(IApplicationDbContext context)
        {
            RuleFor(x => x.EducationalProgramId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<EducationalProgram>(context));
        }
    }
}