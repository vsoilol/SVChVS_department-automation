using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.Lessons.Commands.CreateLesson;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities;
using DepartmentAutomation.Domain.Entities.CompetenceInfo;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.Lessons.Commands.CreateLesson
{
    public class CreateLessonCommandValidation : AbstractValidator<CreateLessonCommand>
    {
        public CreateLessonCommandValidation(IApplicationDbContext context)
        {
            RuleFor(x => x.EducationalProgramId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<EducationalProgram>(context));

            When(x => x.CompetencesId != null, () =>
            {
                RuleForEach(x => x.CompetencesId)
                    .Cascade(CascadeMode.StopOnFirstFailure)
                    .GreaterThanOrEqualTo(1)
                    .SetValidator(new SqlIdValidatorFor<Competence>(context));
            });
        }
    }
}