using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.TrainingCourseForms.Commands.DeleteLessonsFromTrainingCourseForm;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.TrainingCourseForms.Commands.DeleteLessonsFromTrainingCourseForm
{
    public class DeleteLessonsFromTrainingCourseFormQueryValidation : AbstractValidator<DeleteLessonsFromTrainingCourseFormQuery>
    {
        public DeleteLessonsFromTrainingCourseFormQueryValidation(IApplicationDbContext context)
        {
            RuleFor(x => x.EducationalProgramId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<EducationalProgram>(context));

            RuleFor(x => x.TrainingCourseFormId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<TrainingCourseForm>(context));
        }
    }
}