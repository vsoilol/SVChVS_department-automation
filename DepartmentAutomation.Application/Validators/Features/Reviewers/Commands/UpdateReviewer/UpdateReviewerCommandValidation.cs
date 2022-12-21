using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.Reviewers.Commands.UpdateReviewer;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.Reviewers.Commands.UpdateReviewer
{
    public class UpdateReviewerCommandValidation : AbstractValidator<UpdateReviewerCommand>
    {
        public UpdateReviewerCommandValidation(IApplicationDbContext context)
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<Reviewer>(context));
        }
    }
}