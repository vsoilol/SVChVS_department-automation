using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.Lessons.Commands.UpdateLesson;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities;
using DepartmentAutomation.Domain.Entities.CompetenceInfo;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.Lessons.Commands.UpdateLesson
{
    public class UpdateLessonCommandValidation : AbstractValidator<UpdateLessonCommand>
    {
        public UpdateLessonCommandValidation(IApplicationDbContext context)
        {
            RuleFor(x => x.Lesson.Id)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<Lesson>(context));

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