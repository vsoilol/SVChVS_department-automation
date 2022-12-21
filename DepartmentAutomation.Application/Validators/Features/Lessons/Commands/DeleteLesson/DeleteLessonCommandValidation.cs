using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.Lessons.Commands.DeleteLesson;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.Lessons.Commands.DeleteLesson
{
    public class DeleteLessonCommandValidation : AbstractValidator<DeleteLessonCommand>
    {
        public DeleteLessonCommandValidation(IApplicationDbContext context)
        {
            RuleFor(x => x.LessonId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<Lesson>(context));
        }
    }
}