using System.IO;
using System.Linq;
using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Common.Models.WordDocument;
using DepartmentAutomation.Domain.Enums;
using DepartmentAutomation.WordDocument.Extensions.Interfaces;
using DepartmentAutomation.WordDocument.Helpers.Interfaces;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace DepartmentAutomation.WordDocument.Services
{
    internal class WordDocumentService : IWordDocumentService
    {
        private readonly IWordprocessingHelper _wordprocessingHelper;
        private readonly ITableHelper _tableHelper;

        private readonly IMainPageInformation _mainPageInformation;
        private readonly IMainPageTable _mainPageTable;
        private readonly IBriefCompetenceTable _briefCompetenceTable;
        private readonly ILecturesTable _lecturesTable;
        private readonly IWeeksTable _weeksTable;
        private readonly IKnowledgeControlForms _knowledgeControlForms;
        private readonly ICourseProjectDescription _courseProjectDescription;
        private readonly ITrainingCourseFormTable _trainingCourseFormTable;
        private readonly IEvaluationToolsTable _evaluationToolsTable;

        private readonly ICompetenceFormationLevelsTable _competenceFormationLevelsTable;

        private readonly ILiteraturesTable _literaturesTable;
        private readonly IInformationBlocks _informationBlocks;

        private readonly IMethodicalRecommendationBlock _methodicalRecommendationBlock;

        private readonly IInformationTechnology _informationTechnology;

        private readonly ISecondPageInfo _secondPageInfo;
        private readonly IFederalStateEducationalStandardInfo _federalStateEducationalStandardInfo;
        private readonly IMaterialSupport _materialSupport;
        private readonly IAnnotationInfo _annotationInfo;

        private readonly ILogger<WordDocumentService> _logger;

        private IWebHostEnvironment _environment;

        public WordDocumentService(ITableHelper tableHelper, IWordprocessingHelper wordprocessingHelper,
            IMainPageInformation mainPageInformation, IMainPageTable mainPageTable,
            IBriefCompetenceTable briefCompetenceTable, ILecturesTable lecturesTable, IWeeksTable weeksTable,
            IKnowledgeControlForms knowledgeControlForms, ICourseProjectDescription courseProjectDescription,
            ITrainingCourseFormTable trainingCourseFormTable, IEvaluationToolsTable evaluationToolsTable,
            ICompetenceFormationLevelsTable competenceFormationLevelsTable, ILiteraturesTable literaturesTable,
            IInformationBlocks informationBlocks, IMethodicalRecommendationBlock methodicalRecommendationBlock,
            IInformationTechnology informationTechnology, ISecondPageInfo secondPageInfo,
            IFederalStateEducationalStandardInfo federalStateEducationalStandardInfo, IMaterialSupport materialSupport,
            IAnnotationInfo annotationInfo,
            ILogger<WordDocumentService> logger,
            IWebHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
            _tableHelper = tableHelper;
            _wordprocessingHelper = wordprocessingHelper;
            _mainPageInformation = mainPageInformation;
            _mainPageTable = mainPageTable;
            _briefCompetenceTable = briefCompetenceTable;
            _lecturesTable = lecturesTable;
            _weeksTable = weeksTable;
            _knowledgeControlForms = knowledgeControlForms;
            _courseProjectDescription = courseProjectDescription;
            _trainingCourseFormTable = trainingCourseFormTable;
            _evaluationToolsTable = evaluationToolsTable;
            _competenceFormationLevelsTable = competenceFormationLevelsTable;
            _literaturesTable = literaturesTable;
            _informationBlocks = informationBlocks;
            _methodicalRecommendationBlock = methodicalRecommendationBlock;
            _informationTechnology = informationTechnology;
            _secondPageInfo = secondPageInfo;
            _federalStateEducationalStandardInfo = federalStateEducationalStandardInfo;
            _materialSupport = materialSupport;
            _annotationInfo = annotationInfo;
        }

        public byte[] GenerateDocument(EducationalProgram educationalProgram)
        {
            string path = Path.Combine(_environment.WebRootPath, $"Template/template.docx");
            
            _logger.LogInformation(path);
            var docAsArray = File.ReadAllBytes(path);

            using (var stream = new MemoryStream())
            {
                stream.Write(docAsArray, 0, docAsArray.Length);
                using (var doc = WordprocessingDocument.Open(stream, true))
                {
                    var mainDocPart = doc.MainDocumentPart;
                    var bod = doc.MainDocumentPart.Document.Body;

                    _mainPageInformation.CreateMainPage(bod, educationalProgram.Discipline);

                    _mainPageTable.CreateMainTable(bod, educationalProgram.Discipline);

                    _federalStateEducationalStandardInfo.CreateFederalStateEducationalStandardInfo(bod,
                        educationalProgram.Discipline);

                    _secondPageInfo.CreateSecondPageInfo(bod, educationalProgram);

                    _informationBlocks.CreateInformationBlocks(bod, mainDocPart,
                        educationalProgram.InformationBlocks.Where(_ => _.Number.StartsWith("1.")).ToList(),
                        "MainInformationBlocks");

                    _briefCompetenceTable.CreateBriefCompetenceTable(bod, educationalProgram.Discipline.Indicators);

                    _lecturesTable.CreateLecturesTable(bod,
                        educationalProgram.Lectures.OrderBy(_ => _.Number).ToList());

                    _weeksTable.CreateWeeksTable(bod, educationalProgram.Discipline);

                    _knowledgeControlForms
                        .CreateKnowledgeControlForms(bod, educationalProgram
                            .KnowledgeControlForms.ToList());

                    CheckExamAndCreditTables(bod, educationalProgram);

                    CheckCourseProjectDescription(educationalProgram, mainDocPart, bod);

                    _trainingCourseFormTable.CreateTrainingCourseFormTable(bod,
                        educationalProgram.TrainingCourseForms);

                    _evaluationToolsTable.CreateEvaluationToolsTable(bod, educationalProgram.EvaluationTools);

                    _competenceFormationLevelsTable.CreateCompetenceFormationLevelsTable(bod,
                        educationalProgram.Discipline.Indicators);

                    _informationBlocks
                        .CreateInformationBlocks(bod, mainDocPart, educationalProgram.InformationBlocks
                            .Where(_ => _.Number.StartsWith("5")).ToList(), "InformationAdditionalBlocks");

                    _literaturesTable
                        .CreateLiteraturesTable(bod,
                            educationalProgram.MainLiteratures, "Основная литература");

                    _literaturesTable
                        .CreateLiteraturesTable(bod,
                            educationalProgram.AdditionalLiteratures, "Дополнительная литература");

                    _informationBlocks.CreateInformationBlocks(bod, mainDocPart,
                        educationalProgram.InformationBlocks.Where(_ => _.Number.StartsWith("7.3")).ToList(),
                        "InternetResources");

                    _methodicalRecommendationBlock.CreateMethodicalRecommendationBlock(bod,
                        educationalProgram.MethodicalRecommendations);

                    _informationTechnology.CreateInformationTechnology(bod,
                        educationalProgram.Lectures
                            .OrderBy(_ => _.Number).ToList());

                    _informationBlocks.CreateInformationBlocks(bod, mainDocPart,
                        educationalProgram.InformationBlocks.Where(_ => _.Number.StartsWith("7.4.3")).ToList(),
                        "SoftwareList");

                    _materialSupport.PasteInfoAboutMaterialSupport(bod, educationalProgram.Audiences);

                    _annotationInfo.PasteAnnotationInfo(bod, mainDocPart, educationalProgram);
                }

                // File.WriteAllBytes($@"C:\Data\Programming\Draft\Draft\Documents\{DateTime.Now:HH_mm_ss}_doc.docx", stream.ToArray());
                return stream.ToArray();
            }
        }

        private void CheckCourseProjectDescription(EducationalProgram educationalProgram,
            MainDocumentPart mainDocPart, Body bod)
        {
            var courseProjectInformationBlock =
                educationalProgram.InformationBlocks.FirstOrDefault(_ => _.Number.StartsWith("2.3"));
            var courseProjectParagraph =
                _wordprocessingHelper.GetElementByInnerText<Paragraph>(bod, "Требования к курсовому проекту");

            if (courseProjectInformationBlock is not null)
            {
                _courseProjectDescription
                    .CreateCourseProjectDescription(bod, mainDocPart, courseProjectParagraph,
                        courseProjectInformationBlock);
            }
            else
            {
                _courseProjectDescription.DeleteCourseProjectDescription(courseProjectParagraph);
            }
        }

        private void CheckExamAndCreditTables(Body body, EducationalProgram educationalProgram)
        {
            var isCredit =
                educationalProgram.Discipline.Semesters.Any(_ => _.KnowledgeCheckType == KnowledgeCheckType.Credit);
            var isExam =
                educationalProgram.Discipline.Semesters.Any(_ => _.KnowledgeCheckType == KnowledgeCheckType.Exam);

            if (!isCredit)
            {
                _tableHelper.DeleteTableAfterText(body, "Зачет");
            }

            if (!isExam)
            {
                _tableHelper.DeleteTableAfterText(body, "Экзамен, дифференцированный зачет");
            }
        }
    }
}