using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.TrainingCourseForms.Commands.AddLessonsToTrainingCourseForm;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.TrainingCourseForms.Commands.AddLessonsToTrainingCourseForm
{
    public class AddLessonsToTrainingCourseFormQueryValidation : AbstractValidator<AddLessonsToTrainingCourseFormQuery>
    {
        public AddLessonsToTrainingCourseFormQueryValidation(IApplicationDbContext context)
        {
            RuleFor(x => x.EducationalProgramId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<EducationalProgram>(context));

            RuleFor(x => x.TrainingCourseFormId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<TrainingCourseForm>(context));

            RuleFor(x => x.FromLessonId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<Lesson>(context));

            RuleFor(x => x.ToLessonId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<Lesson>(context));
        }
    }
}