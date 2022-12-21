using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.Disciplines.Commands.UpdateDisciplineTeachers;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities;
using DepartmentAutomation.Domain.Entities.TeacherInformation;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.Disciplines.Commands.UpdateDisciplineTeachers
{
    public class UpdateDisciplineTeachersCommandValidation : AbstractValidator<UpdateDisciplineTeachersCommand>
    {
        public UpdateDisciplineTeachersCommandValidation(IApplicationDbContext context)
        {
            RuleFor(x => x.DisciplineId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<Discipline>(context));

            RuleForEach(x => x.TeachersId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<Teacher>(context));
        }
    }
}