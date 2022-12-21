using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.Reviewers.Queries.GetReviewerByProgramId;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.Reviewers.Queries.GetReviewerByProgramId
{
    public class GetReviewerByProgramIdQueryValidation : AbstractValidator<GetReviewerByProgramIdQuery>
    {
        public GetReviewerByProgramIdQueryValidation(IApplicationDbContext context)
        {
            RuleFor(x => x.EducationalProgramId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<EducationalProgram>(context));
        }
    }
}