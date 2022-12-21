using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.EducationalPrograms.Commands.UpdateAudiences;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.EducationalPrograms.Commands.UpdateAudiences
{
    public class UpdateAudiencesCommandValidation : AbstractValidator<UpdateAudiencesCommand>
    {
        public UpdateAudiencesCommandValidation(IApplicationDbContext context)
        {
            RuleFor(x => x.EducationalProgramId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<EducationalProgram>(context));

            When(x => x.AudienceIds != null, () =>
            {
                RuleForEach(x => x.AudienceIds)
                    .Cascade(CascadeMode.StopOnFirstFailure)
                    .GreaterThanOrEqualTo(1)
                    .SetValidator(new SqlIdValidatorFor<Audience>(context));
            });
        }
    }
}