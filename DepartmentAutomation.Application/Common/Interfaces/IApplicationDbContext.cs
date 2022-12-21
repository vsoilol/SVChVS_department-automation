using DepartmentAutomation.Domain.Contracts;
using DepartmentAutomation.Domain.Entities;
using DepartmentAutomation.Domain.Entities.CompetenceInfo;
using DepartmentAutomation.Domain.Entities.DepartmentInfo;
using DepartmentAutomation.Domain.Entities.EvaluationToolInfo;
using DepartmentAutomation.Domain.Entities.InformationBlockInfo;
using DepartmentAutomation.Domain.Entities.KnowledgeControlFormInfo;
using DepartmentAutomation.Domain.Entities.LiteratureInfo;
using DepartmentAutomation.Domain.Entities.SemesterInfo;
using DepartmentAutomation.Domain.Entities.TeacherInformation;
using DepartmentAutomation.Domain.Entities.UserInfo;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace DepartmentAutomation.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Discipline> Disciplines { get; set; }

        DbSet<ApplicationUser> ApplicationUser { get; set; }

        DbSet<Semester> Semesters { get; set; }

        DbSet<SemesterDistribution> SemesterDistributions { get; set; }

        DbSet<Department> Departments { get; set; }

        DbSet<DepartmentHead> DepartmentHeads { get; set; }

        DbSet<FederalStateEducationalStandard> FederalStateEducationalStandards { get; set; }

        DbSet<Specialty> Specialties { get; set; }

        DbSet<EducationalProgram> EducationalPrograms { get; set; }

        DbSet<Competence> Competences { get; set; }

        DbSet<Indicator> Indicators { get; set; }

        DbSet<Curriculum> Curriculums { get; set; }

        DbSet<TrainingCourseForm> TrainingCourseForms { get; set; }

        DbSet<Lesson> Lessons { get; set; }

        DbSet<Week> Weeks { get; set; }

        DbSet<KnowledgeControlForm> KnowledgeControlForms { get; set; }

        DbSet<MethodicalRecommendation> MethodicalRecommendations { get; set; }

        DbSet<CompetenceFormationLevel> CompetenceFormationLevels { get; set; }

        DbSet<EvaluationToolType> EvaluationToolTypes { get; set; }

        DbSet<EvaluationTool> EvaluationTools { get; set; }

        DbSet<InformationBlock> InformationBlocks { get; set; }

        DbSet<InformationBlockContent> InformationBlockContents { get; set; }

        DbSet<InformationTemplate> InformationTemplates { get; set; }

        DbSet<Literature> Literatures { get; set; }

        DbSet<Inspector> Inspectors { get; set; }

        DbSet<AcademicDegree> AcademicDegrees { get; set; }

        DbSet<AcademicRank> AcademicRanks { get; set; }

        DbSet<Position> Positions { get; set; }

        DbSet<Teacher> Teachers { get; set; }

        DbSet<RefreshToken> RefreshTokens { get; set; }

        DbSet<LiteratureTypeInfo> LiteratureTypeInfos { get; set; }

        DbSet<Audience> Audiences { get; set; }

        DbSet<Faculty> Faculties { get; set; }

        DbSet<Protocol> Protocols { get; set; }

        DbSet<Reviewer> Reviewers { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        bool Exists<T>(int id) where T : Entity<int>;
    }
}