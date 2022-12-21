using DepartmentAutomation.Application.Common.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using DepartmentAutomation.Application.Contracts.Requests.Filters;
using DepartmentAutomation.Application.Features.Teachers.Queries.GetTeachersWithPagination;
using DepartmentAutomation.Application.PredicateFactories;
using DepartmentAutomation.Domain.Entities;
using DepartmentAutomation.Domain.Entities.TeacherInformation;
using DepartmentAutomation.Domain.Entities.InformationBlockInfo;

namespace DepartmentAutomation.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            /*services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));*/
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));

            // Predicate Factories
            services.AddTransient<IExpressionBuilder<EducationalProgram, EducationalProgramsFilterDto>, EducationalProgramFilterPredicateFactory>();
            services.AddTransient<IExpressionBuilder<Teacher, TeacherFilterDto>, TeacherPredicateFactory>();
            services.AddTransient<IExpressionBuilder<InformationBlock, Discipline>, InformationBlockPredicateFactory>();
            services.AddTransient<IExpressionBuilder<Discipline, DisciplinesFilterDto>, DisciplineFilterPredicateFactory>();
            
            return services;
        }
    }
}