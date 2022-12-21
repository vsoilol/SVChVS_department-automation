using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities;
using FluentValidation;

namespace DepartmentAutomation.Application.Features.Competences.Queries.GetAllCompetencesByProgramId
{
    public class GetAllCompetencesByProgramIdQueryValidation : AbstractValidator<GetAllCompetencesByProgramIdQuery>
    {
        public GetAllCompetencesByProgramIdQueryValidation(IApplicationDbContext context)
        {
            RuleFor(x => x.EducationalProgramId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<EducationalProgram>(context));
        }
    }
}