﻿using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.Lessons.Queries.GetAllLessonsWithoutTrainingCourseForm;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.Lessons.Queries.GetAllLessonsWithoutTrainingCourseForm
{
    public class
        GetAllLessonsWithoutTrainingCourseFormQueryValidation : AbstractValidator<
            GetAllLessonsWithoutTrainingCourseFormQuery>
    {
        public GetAllLessonsWithoutTrainingCourseFormQueryValidation(IApplicationDbContext context)
        {
            RuleFor(x => x.EducationalProgramId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<EducationalProgram>(context));
        }
    }
}