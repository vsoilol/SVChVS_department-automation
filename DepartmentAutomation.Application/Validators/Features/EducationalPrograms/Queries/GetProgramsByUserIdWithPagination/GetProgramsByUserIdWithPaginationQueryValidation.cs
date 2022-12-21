using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.EducationalPrograms.Queries.GetProgramsByUserIdWithPagination;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities;
using DepartmentAutomation.Domain.Entities.UserInfo;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace DepartmentAutomation.Application.Validators.Features.EducationalPrograms.Queries.
    GetProgramsByUserIdWithPagination
{
    public class
        GetProgramsByUserIdWithPaginationQueryValidation : AbstractValidator<
            GetProgramsByUserIdWithPaginationQuery>
    {
        public GetProgramsByUserIdWithPaginationQueryValidation(IApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1);

            When(x => x.Filter != null, () =>
            {
                RuleFor(x => x.Filter.UserId)
                    .Cascade(CascadeMode.StopOnFirstFailure)
                    .NotEmpty()
                    .SetAsyncValidator(
                        new ApplicationUserValidatorFor<GetProgramsByUserIdWithPaginationQuery>(context, userManager));

                RuleFor(x => x.Filter.PropertyName)
                    .Cascade(CascadeMode.StopOnFirstFailure)
                    .NotEmpty()
                    .SetValidator(new PropertyNameValidatorFor<EducationalProgram>());
            });
        }
    }
}