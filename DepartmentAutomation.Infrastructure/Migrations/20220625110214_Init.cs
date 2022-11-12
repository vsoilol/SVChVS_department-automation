using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace DepartmentAutomation.Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "AcademicDegrees",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicDegrees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AcademicRanks",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicRanks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Audiences",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    BuildingNumber = table.Column<int>(type: "int", nullable: false),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audiences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Competences",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentHeads",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Patronymic = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentHeads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationToolTypes",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationToolTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Faculties",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FederalStateEducationalStandards",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FederalStateEducationalStandards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InformationBlocks",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InformationBlocks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Inspectors",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Patronymic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inspectors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KnowledgeControlForms",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnowledgeControlForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Literatures",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Recommended = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SetNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Literatures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MethodicalRecommendations",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MethodicalRecommendations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Protocols",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Protocols", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reviewers",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Patronymic = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviewers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Semesters",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    WeeksNumber = table.Column<int>(type: "int", nullable: false),
                    CourseProjectEndWeek = table.Column<int>(type: "int", nullable: false),
                    ExamEndWeek = table.Column<int>(type: "int", nullable: false),
                    CourseNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semesters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingCourseForms",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingCourseForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Patronymic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Indicators",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompetenceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Indicators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Indicators_Competences_CompetenceId",
                        column: x => x.CompetenceId,
                        principalSchema: "dbo",
                        principalTable: "Competences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentHeadId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_DepartmentHeads_DepartmentHeadId",
                        column: x => x.DepartmentHeadId,
                        principalSchema: "dbo",
                        principalTable: "DepartmentHeads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InformationTemplates",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InformationBlockId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InformationTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InformationTemplates_InformationBlocks_InformationBlockId",
                        column: x => x.InformationBlockId,
                        principalSchema: "dbo",
                        principalTable: "InformationBlocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dbo",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JwtId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Used = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dbo",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Specialties",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Qualification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LearningForm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudyPeriod = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    FederalStateEducationalStandardId = table.Column<int>(type: "int", nullable: false),
                    FacultyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Specialties_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "dbo",
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Specialties_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalSchema: "dbo",
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Specialties_FederalStateEducationalStandards_FederalStateEducationalStandardId",
                        column: x => x.FederalStateEducationalStandardId,
                        principalSchema: "dbo",
                        principalTable: "FederalStateEducationalStandards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AcademicDegreeId = table.Column<int>(type: "int", nullable: true),
                    AcademicRankId = table.Column<int>(type: "int", nullable: true),
                    PositionId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teachers_AcademicDegrees_AcademicDegreeId",
                        column: x => x.AcademicDegreeId,
                        principalSchema: "dbo",
                        principalTable: "AcademicDegrees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teachers_AcademicRanks_AcademicRankId",
                        column: x => x.AcademicRankId,
                        principalSchema: "dbo",
                        principalTable: "AcademicRanks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teachers_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "dbo",
                        principalTable: "Departments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Teachers_Positions_PositionId",
                        column: x => x.PositionId,
                        principalSchema: "dbo",
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teachers_Users_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Curriculums",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudyStartingYear = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SpecialtyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curriculums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Curriculums_Specialties_SpecialtyId",
                        column: x => x.SpecialtyId,
                        principalSchema: "dbo",
                        principalTable: "Specialties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Disciplines",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LaborIntensityHours = table.Column<int>(type: "int", nullable: false),
                    LaborIntensityCreditUnits = table.Column<int>(type: "int", nullable: false),
                    ContactWorkHours = table.Column<int>(type: "int", nullable: false),
                    LecturesHours = table.Column<int>(type: "int", nullable: false),
                    LaboratoryClassesHours = table.Column<int>(type: "int", nullable: true),
                    PracticalClassesHours = table.Column<int>(type: "int", nullable: true),
                    CourseProjectSemester = table.Column<int>(type: "int", nullable: true),
                    CourseWorkSemester = table.Column<int>(type: "int", nullable: true),
                    SelfStudyHours = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    CurriculumId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Disciplines_Curriculums_CurriculumId",
                        column: x => x.CurriculumId,
                        principalSchema: "dbo",
                        principalTable: "Curriculums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Disciplines_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "dbo",
                        principalTable: "Departments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DisciplineIndicator",
                schema: "dbo",
                columns: table => new
                {
                    DisciplinesId = table.Column<int>(type: "int", nullable: false),
                    IndicatorsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplineIndicator", x => new { x.DisciplinesId, x.IndicatorsId });
                    table.ForeignKey(
                        name: "FK_DisciplineIndicator_Disciplines_DisciplinesId",
                        column: x => x.DisciplinesId,
                        principalSchema: "dbo",
                        principalTable: "Disciplines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisciplineIndicator_Indicators_IndicatorsId",
                        column: x => x.IndicatorsId,
                        principalSchema: "dbo",
                        principalTable: "Indicators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DisciplineTeacher",
                schema: "dbo",
                columns: table => new
                {
                    DisciplinesId = table.Column<int>(type: "int", nullable: false),
                    TeachersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplineTeacher", x => new { x.DisciplinesId, x.TeachersId });
                    table.ForeignKey(
                        name: "FK_DisciplineTeacher_Disciplines_DisciplinesId",
                        column: x => x.DisciplinesId,
                        principalSchema: "dbo",
                        principalTable: "Disciplines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisciplineTeacher_Teachers_TeachersId",
                        column: x => x.TeachersId,
                        principalSchema: "dbo",
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EducationalPrograms",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovalRecommendedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProtocolNumber = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DisciplineId = table.Column<int>(type: "int", nullable: false),
                    ReviewerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationalPrograms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EducationalPrograms_Disciplines_DisciplineId",
                        column: x => x.DisciplineId,
                        principalSchema: "dbo",
                        principalTable: "Disciplines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EducationalPrograms_Reviewers_ReviewerId",
                        column: x => x.ReviewerId,
                        principalSchema: "dbo",
                        principalTable: "Reviewers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SemesterDistributions",
                schema: "dbo",
                columns: table => new
                {
                    DisciplineId = table.Column<int>(type: "int", nullable: false),
                    SemesterId = table.Column<int>(type: "int", nullable: false),
                    KnowledgeCheckType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemesterDistributions", x => new { x.DisciplineId, x.SemesterId });
                    table.ForeignKey(
                        name: "FK_SemesterDistributions_Disciplines_DisciplineId",
                        column: x => x.DisciplineId,
                        principalSchema: "dbo",
                        principalTable: "Disciplines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SemesterDistributions_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalSchema: "dbo",
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AudienceEducationalProgram",
                schema: "dbo",
                columns: table => new
                {
                    AudiencesId = table.Column<int>(type: "int", nullable: false),
                    EducationalProgramsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AudienceEducationalProgram", x => new { x.AudiencesId, x.EducationalProgramsId });
                    table.ForeignKey(
                        name: "FK_AudienceEducationalProgram_Audiences_AudiencesId",
                        column: x => x.AudiencesId,
                        principalSchema: "dbo",
                        principalTable: "Audiences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AudienceEducationalProgram_EducationalPrograms_EducationalProgramsId",
                        column: x => x.EducationalProgramsId,
                        principalSchema: "dbo",
                        principalTable: "EducationalPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompetenceFormationLevels",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LevelNumber = table.Column<int>(type: "int", nullable: false),
                    FormationLevel = table.Column<int>(type: "int", nullable: false),
                    FactualDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LearningOutcomes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IndicatorId = table.Column<int>(type: "int", nullable: false),
                    EducationalProgramId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetenceFormationLevels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetenceFormationLevels_EducationalPrograms_EducationalProgramId",
                        column: x => x.EducationalProgramId,
                        principalSchema: "dbo",
                        principalTable: "EducationalPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetenceFormationLevels_Indicators_IndicatorId",
                        column: x => x.IndicatorId,
                        principalSchema: "dbo",
                        principalTable: "Indicators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EducationalProgramInspector",
                schema: "dbo",
                columns: table => new
                {
                    EducationalProgramsId = table.Column<int>(type: "int", nullable: false),
                    InspectorsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationalProgramInspector", x => new { x.EducationalProgramsId, x.InspectorsId });
                    table.ForeignKey(
                        name: "FK_EducationalProgramInspector_EducationalPrograms_EducationalProgramsId",
                        column: x => x.EducationalProgramsId,
                        principalSchema: "dbo",
                        principalTable: "EducationalPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EducationalProgramInspector_Inspectors_InspectorsId",
                        column: x => x.InspectorsId,
                        principalSchema: "dbo",
                        principalTable: "Inspectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EducationalProgramKnowledgeControlForm",
                schema: "dbo",
                columns: table => new
                {
                    EducationalProgramsId = table.Column<int>(type: "int", nullable: false),
                    KnowledgeControlFormsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationalProgramKnowledgeControlForm", x => new { x.EducationalProgramsId, x.KnowledgeControlFormsId });
                    table.ForeignKey(
                        name: "FK_EducationalProgramKnowledgeControlForm_EducationalPrograms_EducationalProgramsId",
                        column: x => x.EducationalProgramsId,
                        principalSchema: "dbo",
                        principalTable: "EducationalPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EducationalProgramKnowledgeControlForm_KnowledgeControlForms_KnowledgeControlFormsId",
                        column: x => x.KnowledgeControlFormsId,
                        principalSchema: "dbo",
                        principalTable: "KnowledgeControlForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EducationalProgramMethodicalRecommendation",
                schema: "dbo",
                columns: table => new
                {
                    EducationalProgramsId = table.Column<int>(type: "int", nullable: false),
                    MethodicalRecommendationsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationalProgramMethodicalRecommendation", x => new { x.EducationalProgramsId, x.MethodicalRecommendationsId });
                    table.ForeignKey(
                        name: "FK_EducationalProgramMethodicalRecommendation_EducationalPrograms_EducationalProgramsId",
                        column: x => x.EducationalProgramsId,
                        principalSchema: "dbo",
                        principalTable: "EducationalPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EducationalProgramMethodicalRecommendation_MethodicalRecommendations_MethodicalRecommendationsId",
                        column: x => x.MethodicalRecommendationsId,
                        principalSchema: "dbo",
                        principalTable: "MethodicalRecommendations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EducationalProgramProtocol",
                schema: "dbo",
                columns: table => new
                {
                    EducationalProgramsId = table.Column<int>(type: "int", nullable: false),
                    ProtocolsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationalProgramProtocol", x => new { x.EducationalProgramsId, x.ProtocolsId });
                    table.ForeignKey(
                        name: "FK_EducationalProgramProtocol_EducationalPrograms_EducationalProgramsId",
                        column: x => x.EducationalProgramsId,
                        principalSchema: "dbo",
                        principalTable: "EducationalPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EducationalProgramProtocol_Protocols_ProtocolsId",
                        column: x => x.ProtocolsId,
                        principalSchema: "dbo",
                        principalTable: "Protocols",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EducationalProgramTrainingCourseForm",
                schema: "dbo",
                columns: table => new
                {
                    EducationalProgramsId = table.Column<int>(type: "int", nullable: false),
                    TrainingCourseFormsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationalProgramTrainingCourseForm", x => new { x.EducationalProgramsId, x.TrainingCourseFormsId });
                    table.ForeignKey(
                        name: "FK_EducationalProgramTrainingCourseForm_EducationalPrograms_EducationalProgramsId",
                        column: x => x.EducationalProgramsId,
                        principalSchema: "dbo",
                        principalTable: "EducationalPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EducationalProgramTrainingCourseForm_TrainingCourseForms_TrainingCourseFormsId",
                        column: x => x.TrainingCourseFormsId,
                        principalSchema: "dbo",
                        principalTable: "TrainingCourseForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationTools",
                schema: "dbo",
                columns: table => new
                {
                    EducationalProgramId = table.Column<int>(type: "int", nullable: false),
                    EvaluationToolTypeId = table.Column<int>(type: "int", nullable: false),
                    SetNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationTools", x => new { x.EducationalProgramId, x.EvaluationToolTypeId });
                    table.ForeignKey(
                        name: "FK_EvaluationTools_EducationalPrograms_EducationalProgramId",
                        column: x => x.EducationalProgramId,
                        principalSchema: "dbo",
                        principalTable: "EducationalPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EvaluationTools_EvaluationToolTypes_EvaluationToolTypeId",
                        column: x => x.EvaluationToolTypeId,
                        principalSchema: "dbo",
                        principalTable: "EvaluationToolTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InformationBlockContents",
                schema: "dbo",
                columns: table => new
                {
                    EducationalProgramId = table.Column<int>(type: "int", nullable: false),
                    InformationBlockId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InformationBlockContents", x => new { x.EducationalProgramId, x.InformationBlockId });
                    table.ForeignKey(
                        name: "FK_InformationBlockContents_EducationalPrograms_EducationalProgramId",
                        column: x => x.EducationalProgramId,
                        principalSchema: "dbo",
                        principalTable: "EducationalPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InformationBlockContents_InformationBlocks_InformationBlockId",
                        column: x => x.InformationBlockId,
                        principalSchema: "dbo",
                        principalTable: "InformationBlocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lessons",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hours = table.Column<int>(type: "int", nullable: false),
                    LessonType = table.Column<int>(type: "int", nullable: false),
                    TrainingCourseFormId = table.Column<int>(type: "int", nullable: true),
                    EducationalProgramId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lessons_EducationalPrograms_EducationalProgramId",
                        column: x => x.EducationalProgramId,
                        principalSchema: "dbo",
                        principalTable: "EducationalPrograms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lessons_TrainingCourseForms_TrainingCourseFormId",
                        column: x => x.TrainingCourseFormId,
                        principalSchema: "dbo",
                        principalTable: "TrainingCourseForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LiteratureTypeInfos",
                schema: "dbo",
                columns: table => new
                {
                    EducationalProgramId = table.Column<int>(type: "int", nullable: false),
                    LiteratureId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiteratureTypeInfos", x => new { x.LiteratureId, x.EducationalProgramId });
                    table.ForeignKey(
                        name: "FK_LiteratureTypeInfos_EducationalPrograms_EducationalProgramId",
                        column: x => x.EducationalProgramId,
                        principalSchema: "dbo",
                        principalTable: "EducationalPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LiteratureTypeInfos_Literatures_LiteratureId",
                        column: x => x.LiteratureId,
                        principalSchema: "dbo",
                        principalTable: "Literatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Weeks",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    IndependentWorkHours = table.Column<int>(type: "int", nullable: false),
                    TrainingModuleNumber = table.Column<int>(type: "int", nullable: false),
                    SemesterId = table.Column<int>(type: "int", nullable: false),
                    EducationalProgramId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weeks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Weeks_EducationalPrograms_EducationalProgramId",
                        column: x => x.EducationalProgramId,
                        principalSchema: "dbo",
                        principalTable: "EducationalPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Weeks_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalSchema: "dbo",
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompetenceFormationLevelEvaluationToolType",
                schema: "dbo",
                columns: table => new
                {
                    CompetenceFormationLevelsId = table.Column<int>(type: "int", nullable: false),
                    EvaluationToolTypesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetenceFormationLevelEvaluationToolType", x => new { x.CompetenceFormationLevelsId, x.EvaluationToolTypesId });
                    table.ForeignKey(
                        name: "FK_CompetenceFormationLevelEvaluationToolType_CompetenceFormationLevels_CompetenceFormationLevelsId",
                        column: x => x.CompetenceFormationLevelsId,
                        principalSchema: "dbo",
                        principalTable: "CompetenceFormationLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetenceFormationLevelEvaluationToolType_EvaluationToolTypes_EvaluationToolTypesId",
                        column: x => x.EvaluationToolTypesId,
                        principalSchema: "dbo",
                        principalTable: "EvaluationToolTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompetenceLesson",
                schema: "dbo",
                columns: table => new
                {
                    CompetencesId = table.Column<int>(type: "int", nullable: false),
                    LessonsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetenceLesson", x => new { x.CompetencesId, x.LessonsId });
                    table.ForeignKey(
                        name: "FK_CompetenceLesson_Competences_CompetencesId",
                        column: x => x.CompetencesId,
                        principalSchema: "dbo",
                        principalTable: "Competences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetenceLesson_Lessons_LessonsId",
                        column: x => x.LessonsId,
                        principalSchema: "dbo",
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KnowledgeAssessment",
                schema: "dbo",
                columns: table => new
                {
                    KnowledgeControlFormId = table.Column<int>(type: "int", nullable: false),
                    WeekId = table.Column<int>(type: "int", nullable: false),
                    MaxMark = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnowledgeAssessment", x => new { x.WeekId, x.KnowledgeControlFormId });
                    table.ForeignKey(
                        name: "FK_KnowledgeAssessment_KnowledgeControlForms_KnowledgeControlFormId",
                        column: x => x.KnowledgeControlFormId,
                        principalSchema: "dbo",
                        principalTable: "KnowledgeControlForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KnowledgeAssessment_Weeks_WeekId",
                        column: x => x.WeekId,
                        principalSchema: "dbo",
                        principalTable: "Weeks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LessonWeek",
                schema: "dbo",
                columns: table => new
                {
                    LessonsId = table.Column<int>(type: "int", nullable: false),
                    WeeksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonWeek", x => new { x.LessonsId, x.WeeksId });
                    table.ForeignKey(
                        name: "FK_LessonWeek_Lessons_LessonsId",
                        column: x => x.LessonsId,
                        principalSchema: "dbo",
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonWeek_Weeks_WeeksId",
                        column: x => x.WeeksId,
                        principalSchema: "dbo",
                        principalTable: "Weeks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AudienceEducationalProgram_EducationalProgramsId",
                schema: "dbo",
                table: "AudienceEducationalProgram",
                column: "EducationalProgramsId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetenceFormationLevelEvaluationToolType_EvaluationToolTypesId",
                schema: "dbo",
                table: "CompetenceFormationLevelEvaluationToolType",
                column: "EvaluationToolTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetenceFormationLevels_EducationalProgramId",
                schema: "dbo",
                table: "CompetenceFormationLevels",
                column: "EducationalProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetenceFormationLevels_IndicatorId",
                schema: "dbo",
                table: "CompetenceFormationLevels",
                column: "IndicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetenceLesson_LessonsId",
                schema: "dbo",
                table: "CompetenceLesson",
                column: "LessonsId");

            migrationBuilder.CreateIndex(
                name: "IX_Curriculums_SpecialtyId",
                schema: "dbo",
                table: "Curriculums",
                column: "SpecialtyId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_DepartmentHeadId",
                schema: "dbo",
                table: "Departments",
                column: "DepartmentHeadId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineIndicator_IndicatorsId",
                schema: "dbo",
                table: "DisciplineIndicator",
                column: "IndicatorsId");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplines_CurriculumId",
                schema: "dbo",
                table: "Disciplines",
                column: "CurriculumId");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplines_DepartmentId",
                schema: "dbo",
                table: "Disciplines",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineTeacher_TeachersId",
                schema: "dbo",
                table: "DisciplineTeacher",
                column: "TeachersId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationalProgramInspector_InspectorsId",
                schema: "dbo",
                table: "EducationalProgramInspector",
                column: "InspectorsId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationalProgramKnowledgeControlForm_KnowledgeControlFormsId",
                schema: "dbo",
                table: "EducationalProgramKnowledgeControlForm",
                column: "KnowledgeControlFormsId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationalProgramMethodicalRecommendation_MethodicalRecommendationsId",
                schema: "dbo",
                table: "EducationalProgramMethodicalRecommendation",
                column: "MethodicalRecommendationsId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationalProgramProtocol_ProtocolsId",
                schema: "dbo",
                table: "EducationalProgramProtocol",
                column: "ProtocolsId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationalPrograms_DisciplineId",
                schema: "dbo",
                table: "EducationalPrograms",
                column: "DisciplineId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EducationalPrograms_ReviewerId",
                schema: "dbo",
                table: "EducationalPrograms",
                column: "ReviewerId",
                unique: true,
                filter: "[ReviewerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EducationalProgramTrainingCourseForm_TrainingCourseFormsId",
                schema: "dbo",
                table: "EducationalProgramTrainingCourseForm",
                column: "TrainingCourseFormsId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationTools_EvaluationToolTypeId",
                schema: "dbo",
                table: "EvaluationTools",
                column: "EvaluationToolTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Indicators_CompetenceId",
                schema: "dbo",
                table: "Indicators",
                column: "CompetenceId");

            migrationBuilder.CreateIndex(
                name: "IX_InformationBlockContents_InformationBlockId",
                schema: "dbo",
                table: "InformationBlockContents",
                column: "InformationBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_InformationTemplates_InformationBlockId",
                schema: "dbo",
                table: "InformationTemplates",
                column: "InformationBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeAssessment_KnowledgeControlFormId",
                schema: "dbo",
                table: "KnowledgeAssessment",
                column: "KnowledgeControlFormId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_EducationalProgramId",
                schema: "dbo",
                table: "Lessons",
                column: "EducationalProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_TrainingCourseFormId",
                schema: "dbo",
                table: "Lessons",
                column: "TrainingCourseFormId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonWeek_WeeksId",
                schema: "dbo",
                table: "LessonWeek",
                column: "WeeksId");

            migrationBuilder.CreateIndex(
                name: "IX_LiteratureTypeInfos_EducationalProgramId",
                schema: "dbo",
                table: "LiteratureTypeInfos",
                column: "EducationalProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                schema: "dbo",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                schema: "dbo",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "dbo",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterDistributions_SemesterId",
                schema: "dbo",
                table: "SemesterDistributions",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_Specialties_DepartmentId",
                schema: "dbo",
                table: "Specialties",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Specialties_FacultyId",
                schema: "dbo",
                table: "Specialties",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Specialties_FederalStateEducationalStandardId",
                schema: "dbo",
                table: "Specialties",
                column: "FederalStateEducationalStandardId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_AcademicDegreeId",
                schema: "dbo",
                table: "Teachers",
                column: "AcademicDegreeId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_AcademicRankId",
                schema: "dbo",
                table: "Teachers",
                column: "AcademicRankId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_ApplicationUserId",
                schema: "dbo",
                table: "Teachers",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_DepartmentId",
                schema: "dbo",
                table: "Teachers",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_PositionId",
                schema: "dbo",
                table: "Teachers",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                schema: "dbo",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "dbo",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "dbo",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "dbo",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Weeks_EducationalProgramId",
                schema: "dbo",
                table: "Weeks",
                column: "EducationalProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Weeks_SemesterId",
                schema: "dbo",
                table: "Weeks",
                column: "SemesterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AudienceEducationalProgram",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CompetenceFormationLevelEvaluationToolType",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CompetenceLesson",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "DisciplineIndicator",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "DisciplineTeacher",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "EducationalProgramInspector",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "EducationalProgramKnowledgeControlForm",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "EducationalProgramMethodicalRecommendation",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "EducationalProgramProtocol",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "EducationalProgramTrainingCourseForm",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "EvaluationTools",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "InformationBlockContents",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "InformationTemplates",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "KnowledgeAssessment",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "LessonWeek",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "LiteratureTypeInfos",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "RefreshTokens",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "RoleClaims",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "SemesterDistributions",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UserClaims",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Audiences",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CompetenceFormationLevels",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Teachers",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Inspectors",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "MethodicalRecommendations",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Protocols",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "EvaluationToolTypes",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "InformationBlocks",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "KnowledgeControlForms",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Lessons",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Weeks",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Literatures",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Indicators",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AcademicDegrees",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AcademicRanks",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Positions",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TrainingCourseForms",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "EducationalPrograms",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Semesters",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Competences",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Disciplines",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Reviewers",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Curriculums",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Specialties",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Departments",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Faculties",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "FederalStateEducationalStandards",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "DepartmentHeads",
                schema: "dbo");
        }
    }
}
