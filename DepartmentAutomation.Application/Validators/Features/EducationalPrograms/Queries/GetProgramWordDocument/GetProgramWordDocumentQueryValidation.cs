using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.EducationalPrograms.Queries.GetProgramWordDocument;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.EducationalPrograms.Queries.GetProgramWordDocument
{
    public class GetProgramWordDocumentQueryValidation : AbstractValidator<GetProgramWordDocumentQuery>
    {
        public GetProgramWordDocumentQueryValidation(IApplicationDbContext context)
        {
            RuleFor(x => x.EducationalProgramId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<EducationalProgram>(context));
        }
    }
}