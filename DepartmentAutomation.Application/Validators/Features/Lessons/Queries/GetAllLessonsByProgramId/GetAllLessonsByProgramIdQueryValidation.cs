using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.Lessons.Queries.GetAllLessonsByProgramId;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.Lessons.Queries.GetAllLessonsByProgramId
{
    public class GetAllLessonsByProgramIdQueryValidation : AbstractValidator<GetAllLessonsByProgramIdQuery>
    {
        public GetAllLessonsByProgramIdQueryValidation(IApplicationDbContext context)
        {
            RuleFor(x => x.EducationalProgramId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<EducationalProgram>(context));
        }
    }
}