using DepartmentAutomation.Application.Common.Interfaces;
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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Threading.Tasks;

namespace DepartmentAutomation.Infrastructure.Persistence
{
    public sealed class DepartmentAutomationContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        private readonly ILoggerFactory _loggerFactory;

        public DepartmentAutomationContext(DbContextOptions<DepartmentAutomationContext> options,
            ILoggerFactory loggerFactory)
            : base(options)
        {
            // Database.EnsureCreated();
            _loggerFactory = loggerFactory;

            // Database.Migrate();
        }

        public DbSet<Faculty> Faculties { get; set; }

        public DbSet<Audience> Audiences { get; set; }

        public DbSet<Protocol> Protocols { get; set; }

        public DbSet<Reviewer> Reviewers { get; set; }

        public DbSet<Discipline> Disciplines { get; set; }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        public DbSet<Semester> Semesters { get; set; }

        public DbSet<SemesterDistribution> SemesterDistributions { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<DepartmentHead> DepartmentHeads { get; set; }

        public DbSet<FederalStateEducationalStandard> FederalStateEducationalStandards { get; set; }

        public DbSet<Specialty> Specialties { get; set; }

        public DbSet<EducationalProgram> EducationalPrograms { get; set; }

        public DbSet<Competence> Competences { get; set; }

        public DbSet<Indicator> Indicators { get; set; }

        public DbSet<Curriculum> Curriculums { get; set; }

        public DbSet<TrainingCourseForm> TrainingCourseForms { get; set; }

        public DbSet<Lesson> Lessons { get; set; }

        public DbSet<Week> Weeks { get; set; }

        public DbSet<KnowledgeControlForm> KnowledgeControlForms { get; set; }

        public DbSet<MethodicalRecommendation> MethodicalRecommendations { get; set; }

        public DbSet<CompetenceFormationLevel> CompetenceFormationLevels { get; set; }

        public DbSet<EvaluationToolType> EvaluationToolTypes { get; set; }

        public DbSet<EvaluationTool> EvaluationTools { get; set; }

        public DbSet<InformationBlock> InformationBlocks { get; set; }

        public DbSet<InformationBlockContent> InformationBlockContents { get; set; }

        public DbSet<InformationTemplate> InformationTemplates { get; set; }

        public DbSet<Literature> Literatures { get; set; }

        public DbSet<Inspector> Inspectors { get; set; }

        public DbSet<AcademicDegree> AcademicDegrees { get; set; }

        public DbSet<AcademicRank> AcademicRanks { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<LiteratureTypeInfo> LiteratureTypeInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            var user = modelBuilder.Entity<ApplicationUser>().ToTable("Users");

            user.Ignore(c => c.LockoutEnabled)
                .Ignore(c => c.LockoutEnd)
                .Ignore(c => c.AccessFailedCount)
                .Ignore(c => c.PhoneNumberConfirmed)
                .Ignore(c => c.TwoFactorEnabled);

            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");

            modelBuilder.Ignore<IdentityUserLogin<string>>();
            modelBuilder.Ignore<IdentityUserToken<string>>();
        }

        public Task<int> SaveChangesAsync() => base.SaveChangesAsync();

        public bool Exists<T>(int id)
            where T : Entity<int>
        {
            var entity = Find<T>(id);
            return entity is not null;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(_loggerFactory);
        }
    }
}