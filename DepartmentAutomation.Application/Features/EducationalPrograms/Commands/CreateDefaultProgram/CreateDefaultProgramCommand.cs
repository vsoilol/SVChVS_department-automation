using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Domain.Entities;
using DepartmentAutomation.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DepartmentAutomation.Application.PredicateFactories;
using DepartmentAutomation.Domain.Entities.InformationBlockInfo;
using DepartmentAutomation.Domain.Entities.CompetenceInfo;
using DepartmentAutomation.Domain.Entities.SemesterInfo;

namespace DepartmentAutomation.Application.Features.EducationalPrograms.Commands.CreateDefaultProgram
{
    public class CreateDefaultProgramCommand : IRequest<int>
    {
        public int DisciplineId { get; set; }
    }

    public class CreateDefaultProgramCommandHandler : IRequestHandler<CreateDefaultProgramCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IExpressionBuilder<InformationBlock, Discipline> _expressionBuilder;

        public CreateDefaultProgramCommandHandler(IApplicationDbContext context,
            IExpressionBuilder<InformationBlock, Discipline> expressionBuilder)
        {
            _context = context;
            _expressionBuilder = expressionBuilder;
        }

        public async Task<int> Handle(CreateDefaultProgramCommand request, CancellationToken cancellationToken)
        {
            var discipline = await _context.Disciplines
                .FirstOrDefaultAsync(_ => _.Id == request.DisciplineId, cancellationToken: cancellationToken);

            discipline.Status = Status.NotStarted;
            
            var indicatorsId = await _context.Indicators
                .Where(_ => _.Disciplines.Any(item => item.Id == request.DisciplineId))
                .Select(_ => _.Id)
                .ToListAsync(cancellationToken: cancellationToken);

            var educationalProgram = new EducationalProgram
            {
                Discipline = discipline,
                InformationBlockContents = await GetInformationBlocksContent(discipline, cancellationToken),
                Weeks = await CreateWeeksAsync(request.DisciplineId, cancellationToken),
                CompetenceFormationLevels = GenerateCompetenceFormationLevelsByIndicators(indicatorsId),
                Reviewer = new Reviewer
                {
                    Name = "",
                    Position = "",
                    Patronymic = "",
                    Surname = ""
                }
            };

            _context.EducationalPrograms.Add(educationalProgram);

            await _context.SaveChangesAsync(cancellationToken);

            return educationalProgram.Id;
        }

        private async Task<List<InformationBlockContent>> GetInformationBlocksContent(Discipline discipline,
            CancellationToken cancellationToken)
        {
            var predicate = _expressionBuilder.Build(discipline);

            var predicateQuery = predicate is not null
                ? _context.InformationBlocks.Where(predicate)
                : _context.InformationBlocks;

            var informationBlockContents = await predicateQuery.Select(_ => new InformationBlockContent
            {
                InformationBlock = _,
                Content = "",
            }).ToListAsync(cancellationToken: cancellationToken);

            return informationBlockContents;
        }

        private async Task<List<Week>> CreateWeeksAsync(int disciplineId, CancellationToken cancellationToken)
        {
            var weeks = new List<Week>();

            var semesters = await _context.Semesters
                .Include(_ => _.SemesterDistributions)
                .Where(semester =>
                    semester.SemesterDistributions.Any(semesterDistribution =>
                        semesterDistribution.DisciplineId == disciplineId))
                .ToListAsync(cancellationToken: cancellationToken);

            foreach (var semester in semesters)
            {
                weeks.AddRange(GenerateSemesterWeeks(semester));
            }

            return weeks;
        }

        private static IEnumerable<Week> GenerateSemesterWeeks(Semester semester)
        {
            var weeks = new List<Week>();

            for (var i = 1; i <= semester.WeeksNumber; i++)
            {
                if (i <= 8)
                {
                    weeks.Add(CreateEmptyWeek(i, 1, semester));
                    continue;
                }

                weeks.Add(CreateEmptyWeek(i, 2, semester));
            }

            return weeks;
        }

        private static Week CreateEmptyWeek(int weekNumber, int moduleNumber, Semester semester)
        {
            return new Week
            {
                IndependentWorkHours = 0,
                Semester = semester,
                Number = weekNumber,
                TrainingModuleNumber = moduleNumber,
            };
        }

        private List<CompetenceFormationLevel> GenerateCompetenceFormationLevelsByIndicators(
            IEnumerable<int> indicatorsId)
        {
            var competenceFormationLevels = new List<CompetenceFormationLevel>();

            foreach (var indicatorId in indicatorsId)
            {
                var levelsData = new List<CompetenceFormationLevel>
                {
                    new CompetenceFormationLevel
                    {
                        FormationLevel = FormationLevel.Threshold,
                        LevelNumber = 1,
                        FactualDescription = "",
                        LearningOutcomes = "",
                        IndicatorId = indicatorId
                    },
                    new CompetenceFormationLevel
                    {
                        FormationLevel = FormationLevel.Advanced,
                        LevelNumber = 2,
                        FactualDescription = "",
                        LearningOutcomes = "",
                        IndicatorId = indicatorId
                    },
                    new CompetenceFormationLevel
                    {
                        FormationLevel = FormationLevel.High,
                        LevelNumber = 3,
                        FactualDescription = "",
                        LearningOutcomes = "",
                        IndicatorId = indicatorId
                    }
                };

                competenceFormationLevels.AddRange(levelsData);
            }

            return competenceFormationLevels;
        }
    }
}