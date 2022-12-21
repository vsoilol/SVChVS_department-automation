using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.Teachers.Queries.GetTeachersWithPagination;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities.DepartmentInfo;
using DepartmentAutomation.Domain.Entities.TeacherInformation;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.Teachers.Queries.GetTeachersWithPagination
{
    public class GetTeachersWithPaginationQueryValidator : AbstractValidator<GetTeachersWithPaginationQuery>
    {
        public GetTeachersWithPaginationQueryValidator(IApplicationDbContext context)
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1);

            When(x => x.Filter != null, () =>
            {
                When(x => x.Filter.DepartmentId != null, () =>
                {
                    RuleFor(x => x.Filter.DepartmentId)
                        .Cascade(CascadeMode.StopOnFirstFailure)
                        .GreaterThanOrEqualTo(1)
                        .SetValidator(new SqlIdValidatorFor<Department>(context));
                });

                When(x => x.Filter.PositionId != null, () =>
                {
                    RuleFor(x => x.Filter.PositionId)
                        .Cascade(CascadeMode.StopOnFirstFailure)
                        .GreaterThanOrEqualTo(1)
                        .SetValidator(new SqlIdValidatorFor<Position>(context));
                });
            });
        }
    }
}