using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Common.Models.WordDocument;
using DepartmentAutomation.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using EducationalProgram = DepartmentAutomation.Domain.Entities.EducationalProgram;
using Lesson = DepartmentAutomation.Application.Common.Models.WordDocument.Lesson;
using Semester = DepartmentAutomation.Application.Common.Models.WordDocument.Semester;

namespace DepartmentAutomation.Application.Converters
{
    public class EducationalProgramToWordDocumentConverter : ITypeConverter<EducationalProgram, Common.Models.WordDocument.EducationalProgram>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EducationalProgramToWordDocumentConverter(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Common.Models.WordDocument.EducationalProgram Convert(EducationalProgram source, Common.Models.WordDocument.EducationalProgram destination, ResolutionContext context)
        {
            destination ??= new Common.Models.WordDocument.EducationalProgram();

            GetDisciplineInfo(source, destination);

            GetTeachersInfo(source, destination);

            GetIndicatorsInfo(source, destination);

            GetLectures(source, destination);

            GetSemestersInfo(source, destination);

            GetInformationBlocks(source, destination);

            GetKnowledgeControlForms(source, destination);

            GetTrainingCourseForms(source, destination);

            GetEvaluationTools(source, destination);

            GetLiteratures(source, destination);

            GetMethodicalRecommendations(source, destination);

            destination.Reviewer = _mapper.Map<Reviewer>(source.Reviewer);

            GetProtocolInfo(destination, source);

            GetAudiencesInfo(destination, source);

            return destination;
        }

        private void GetAudiencesInfo(Common.Models.WordDocument.EducationalProgram destination, EducationalProgram source)
        {
            var audiences = _context.Audiences
                .Where(_ => _.EducationalPrograms.Any(program => program.Id == source.Id))
                .ProjectTo<Audience>(_mapper.ConfigurationProvider)
                .ToList();

            destination.Audiences = audiences;
        }

        private void GetProtocolInfo(Common.Models.WordDocument.EducationalProgram destination, EducationalProgram source)
        {
            var protocols = _context.Protocols
                .Where(_ => _.EducationalPrograms.Any(program => program.Id == source.Id))
                .ProjectTo<Protocol>(_mapper.ConfigurationProvider)
                .ToList();

            destination.Protocols = protocols;
        }

        private void GetIndicatorsInfo(EducationalProgram source, Common.Models.WordDocument.EducationalProgram destination)
        {
            var indicators = _context.Indicators
                .Where(_ => _.Disciplines.Any(discipline => discipline.Id == source.DisciplineId))
                .ProjectTo<Indicator>(_mapper.ConfigurationProvider)
                .ToList();

            destination.Discipline.Indicators = indicators;
        }

        private void GetTeachersInfo(EducationalProgram source, Common.Models.WordDocument.EducationalProgram destination)
        {
            var teachers = _context.Teachers
                .Where(_ => _.Disciplines.Any(discipline => discipline.Id == source.DisciplineId))
                .ProjectTo<Teacher>(_mapper.ConfigurationProvider)
                .ToList();

            destination.Discipline.Teachers = teachers;
        }

        private void GetDisciplineInfo(EducationalProgram source, Common.Models.WordDocument.EducationalProgram destination)
        {
            var discipline = _context.Disciplines
                .Include(_ => _.Department)
                .ThenInclude(_ => _.DepartmentHead)
                .Include(_ => _.Curriculum)
                .ThenInclude(_ => _.Specialty)
                .ThenInclude(_ => _.Department)
                .ThenInclude(_ => _.DepartmentHead)
                .Include(_ => _.Curriculum)
                .ThenInclude(_ => _.Specialty)
                .ThenInclude(_ => _.FederalStateEducationalStandard)
                .FirstOrDefault(_ => _.Id == source.DisciplineId);

            destination.Discipline = _mapper.Map<Discipline>(discipline);
        }

        private void GetMethodicalRecommendations(EducationalProgram source, Common.Models.WordDocument.EducationalProgram destination)
        {
            var methodicalRecommendations = _context.MethodicalRecommendations
                .Where(_ => _.EducationalPrograms.Any(program => program.Id == source.Id))
                .ProjectTo<MethodicalRecommendation>(_mapper.ConfigurationProvider)
                .ToList();

            destination.MethodicalRecommendations = methodicalRecommendations;
        }

        private void GetLiteratures(EducationalProgram source, Common.Models.WordDocument.EducationalProgram destination)
        {
            var literature = _context.LiteratureTypeInfos
                .Where(_ => _.EducationalProgramId == source.Id);

            destination.AdditionalLiteratures = literature
                .Where(_ => _.Type == LiteratureType.Additional)
                .ProjectTo<Literature>(_mapper.ConfigurationProvider)
                .ToList();

            destination.MainLiteratures = literature
                .Where(_ => _.Type == LiteratureType.Main)
                .ProjectTo<Literature>(_mapper.ConfigurationProvider)
                .ToList();
        }

        private void GetEvaluationTools(EducationalProgram source, Common.Models.WordDocument.EducationalProgram destination)
        {
            var evaluationTools = _context.EvaluationTools
                .Where(_ => _.EducationalProgramId == source.Id)
                .ProjectTo<EvaluationTool>(_mapper.ConfigurationProvider)
                .ToList();

            destination.EvaluationTools = evaluationTools;
        }

        private void GetTrainingCourseForms(EducationalProgram source, Common.Models.WordDocument.EducationalProgram destination)
        {
            var trainingCourseForms = _context.TrainingCourseForms
                .Where(_ => _.EducationalPrograms.Any(program => program.Id == source.Id))
                .ProjectTo<TrainingCourseForm>(_mapper.ConfigurationProvider)
                .ToList();

            destination.TrainingCourseForms = trainingCourseForms;
        }

        private void GetKnowledgeControlForms(EducationalProgram source, Common.Models.WordDocument.EducationalProgram destination)
        {
            var knowledgeControlForms = _context.Weeks
                .Where(_ => _.EducationalProgramId == source.Id)
                .SelectMany(_ => _.KnowledgeAssessments)
                .Select(_ => _.KnowledgeControlForm)
                .Distinct()
                .ProjectTo<KnowledgeControlForm>(_mapper.ConfigurationProvider)
                .ToList();

            destination.KnowledgeControlForms = knowledgeControlForms;
        }

        private void GetInformationBlocks(EducationalProgram source, Common.Models.WordDocument.EducationalProgram destination)
        {
            var informationBlocks = _context.InformationBlockContents
                .Where(_ => _.EducationalProgramId == source.Id)
                .ProjectTo<InformationBlock>(_mapper.ConfigurationProvider)
                .ToList();

            destination.InformationBlocks = informationBlocks;
        }

        private void GetSemestersInfo(EducationalProgram source, Common.Models.WordDocument.EducationalProgram destination)
        {
            var semesters = _context.SemesterDistributions
                .Where(_ => _.DisciplineId == source.DisciplineId)
                .ProjectTo<Semester>(_mapper.ConfigurationProvider)
                .ToList();

            foreach (var semester in semesters)
            {
                semester.Weeks = _context.Weeks
                    .Where(_ => _.EducationalProgramId == source.Id && 
                                _.Semester.Number == semester.Number)
                    .OrderBy(_ => _.Number)
                    .ProjectTo<Week>(_mapper.ConfigurationProvider)
                    .ToList();
            }

            destination.Discipline.Semesters = semesters;
        }

        private void GetLectures(EducationalProgram source, Common.Models.WordDocument.EducationalProgram destination)
        {
            var lectures = _context.Lessons
                .Include(_ => _.Competences)
                .Where(_ => _.EducationalProgramId == source.Id && _.LessonType == LessonType.Lecture)
                .ProjectTo<Lesson>(_mapper.ConfigurationProvider)
                .ToList();

            destination.Lectures = lectures;
        }
    }
}