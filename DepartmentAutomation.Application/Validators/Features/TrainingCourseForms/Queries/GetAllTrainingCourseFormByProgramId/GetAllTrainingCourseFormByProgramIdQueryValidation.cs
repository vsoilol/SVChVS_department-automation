using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.TrainingCourseForms.Queries.GetAllTrainingCourseFormByProgramId;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.TrainingCourseForms.Queries.GetAllTrainingCourseFormByProgramId
{
    public class GetAllTrainingCourseFormByProgramIdQueryValidation : AbstractValidator<GetAllTrainingCourseFormByProgramIdQuery>
    {
        public GetAllTrainingCourseFormByProgramIdQueryValidation(IApplicationDbContext context)
        {
            RuleFor(x => x.EducationalProgramId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<EducationalProgram>(context));
        }
    }
}