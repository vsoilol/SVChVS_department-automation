using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Infrastructure.WordDocument.Extensions.Implementations;
using DepartmentAutomation.Infrastructure.WordDocument.Helpers.Implementations;
using DepartmentAutomation.WordDocument.Extensions.Implementations;
using DepartmentAutomation.WordDocument.Extensions.Interfaces;
using DepartmentAutomation.WordDocument.Helpers.Implementations;
using DepartmentAutomation.WordDocument.Helpers.Interfaces;
using DepartmentAutomation.WordDocument.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DepartmentAutomation.WordDocument
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWordDocumentInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddTransient<IWordDocumentService, WordDocumentService>();

            services.AddTransient<ITableHelper, TableHelper>();
            services.AddTransient<IWordprocessingHelper, WordprocessingHelper>();
            services.AddTransient<IMonthHelper, MonthHelper>();

            services.AddTransient<IBriefCompetenceTable, BriefCompetenceTable>();
            services.AddTransient<ICompetenceFormationLevelsTable, CompetenceFormationLevelsTable>();
            services.AddTransient<ICourseProjectDescription, CourseProjectDescription>();
            services.AddTransient<IEvaluationToolsTable, EvaluationToolsTable>();
            services.AddTransient<IInformationBlocks, InformationBlocks>();
            services.AddTransient<IInformationTechnology, InformationTechnology>();
            services.AddTransient<IKnowledgeControlForms, KnowledgeControlForms>();
            services.AddTransient<ILecturesTable, LecturesTable>();
            services.AddTransient<ILiteraturesTable, LiteraturesTable>();
            services.AddTransient<IMainPageInformation, MainPageInformation>();
            services.AddTransient<IMainPageTable, MainPageTable>();
            services.AddTransient<IMethodicalRecommendationBlock, MethodicalRecommendationBlock>();
            services.AddTransient<ITrainingCourseFormTable, TrainingCourseFormTable>();
            services.AddTransient<IWeeksTable, WeeksTable>();

            services.AddTransient<IFederalStateEducationalStandardInfo, FederalStateEducationalStandardInfo>();
            services.AddTransient<ISecondPageInfo, SecondPageInfo>();
            services.AddTransient<IMaterialSupport, MaterialSupport>();
            services.AddTransient<IAnnotationInfo, AnnotationInfo>();

            return services;
        }
    }
}