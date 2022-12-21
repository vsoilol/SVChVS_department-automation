using System.Collections.Generic;
using System.Linq;
using DepartmentAutomation.Application.Common.Models.WordDocument;
using DepartmentAutomation.Domain.Enums;
using DepartmentAutomation.WordDocument.Extensions.Interfaces;
using DepartmentAutomation.WordDocument.Helpers.Interfaces;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DepartmentAutomation.WordDocument.Extensions.Implementations
{
    internal class AnnotationInfo : IAnnotationInfo
    {
        private readonly IWordprocessingHelper _wordprocessingHelper;

        public AnnotationInfo(IWordprocessingHelper wordprocessingHelper)
        {
            _wordprocessingHelper = wordprocessingHelper;
        }

        private static string GetInfoAboutSemesters(Discipline discipline)
        {
            var semesters = discipline.Semesters
                .Select(_ => _.Number)
                .Distinct()
                .OrderBy(_ => _);

            var semesterRowText = string.Join(", ", semesters);
            return semesterRowText;
        }

        private static string GetInformationAboutCourse(Discipline discipline)
        {
            var courses = discipline.Semesters
                .Select(_ => _.CourseNumber)
                .Distinct()
                .OrderBy(_ => _);

            var courseRowText = string.Join(", ", courses);
            return courseRowText;
        }

        public void PasteAnnotationInfo(Body body, MainDocumentPart documentPart, EducationalProgram educationalProgram)
        {
            var discipline = educationalProgram.Discipline;

            var trainingDirectionInfo = $"{discipline.Specialty.Code} " +
                                        $"{discipline.Specialty.Name}";

            var annotationElements = _wordprocessingHelper
                .GetElementByInnerText<Paragraph>(body, "МАТЕРИАЛЬНО-ТЕХНИЧЕСКОЕ ОБЕСПЕЧЕНИЕ УЧЕБНОЙ ДИСЦИПЛИНЫ")
                .ElementsAfter();

            var enumerable = annotationElements.ToList();
            _wordprocessingHelper.PasteTextIntoMark(enumerable, "TrainingDirection", trainingDirectionInfo);

            _wordprocessingHelper.PasteTextIntoMark(enumerable, "Profile", discipline.Specialty.ProfileName);

            _wordprocessingHelper.PasteTextIntoMark(enumerable, "Qualification",
                discipline.Specialty.Qualification);

            _wordprocessingHelper.PasteTextIntoMark(enumerable, "LearningForm",
                discipline.Specialty.LearningForm);

            _wordprocessingHelper.PasteTextIntoMark(enumerable, "DisciplineName", discipline.Name.ToUpper());

            PasteTableInfo(enumerable, discipline);

            PasteAimInfo(
                educationalProgram.InformationBlocks.FirstOrDefault(_ => _.Name.Contains("Цель учебной дисциплины")),
                body,
                documentPart);

            PastePlanResultEducationInfo(educationalProgram.InformationBlocks.FirstOrDefault(_ =>
                    _.Name.Contains("Планируемые результаты изучения дисциплины")),
                body,
                documentPart);

            PasteCompetenceInfo(discipline.Indicators.Select(_ => _.Competence), body);

            PasteEducationalTechnologiesInfo(educationalProgram.TrainingCourseForms, body);
        }

        private void PasteEducationalTechnologiesInfo(List<TrainingCourseForm> trainingCourseForms, Body body)
        {
            body.Append(_wordprocessingHelper.CreateEmptyParagraph());

            var paragraph =
                _wordprocessingHelper.CreateBoldParagraphWithTab(body, "4 Образовательные технологии");
            body.Append(paragraph);

            body.Append(_wordprocessingHelper.CreateEmptyParagraph());

            var trainingCourseFormsInfo = string.Join(", ", trainingCourseForms.Select(_ => _.Name));

            var info = _wordprocessingHelper.CreateParagraphWithText(
                $"При изучении дисциплины используется модульно-рейтинговая " +
                $"система оценки знаний студентов, а также следующие формы проведения занятий: {trainingCourseFormsInfo}.",
                body);

            body.Append(info);
        }

        private void PasteCompetenceInfo(IEnumerable<Competence> competences, Body body)
        {
            var paragraph =
                _wordprocessingHelper.CreateBoldParagraphWithTab(body, "3 Требования к освоению учебной дисциплины");
            body.Append(paragraph);

            body.Append(_wordprocessingHelper.CreateEmptyParagraph());

            var info = _wordprocessingHelper.CreateParagraphWithText(
                "Освоение данной учебной дисциплины должно обеспечивать формирование следующих компетенций:", body);

            body.Append(info);

            foreach (var competence in competences)
            {
                var competenceInfo = $"{competence.Code} {competence.Name}";
                body.Append(_wordprocessingHelper.CreateParagraphWithText(competenceInfo, body));
            }
        }

        private void PastePlanResultEducationInfo(InformationBlock planResultInfo, Body body,
            MainDocumentPart mainDocumentPart)
        {
            var paragraph =
                _wordprocessingHelper.CreateBoldParagraphWithTab(body, "2 Планируемые результаты изучения дисциплины ");
            body.Append(paragraph);

            var altChunk = _wordprocessingHelper.GenerateAltChunkFromHtml(mainDocumentPart, planResultInfo.Content);
            var altChunckBlock = paragraph.InsertAfterSelf(altChunk);

            altChunckBlock.InsertAfterSelf(_wordprocessingHelper.GetNewLine(body));
        }

        private void PasteAimInfo(InformationBlock aimInfo, Body body,
            MainDocumentPart mainDocumentPart)
        {
            var paragraph = _wordprocessingHelper.CreateBoldParagraphWithTab(body, "1 Цель учебной дисциплины");
            body.Append(paragraph);

            var altChunk = _wordprocessingHelper.GenerateAltChunkFromHtml(mainDocumentPart, aimInfo.Content);
            var altChunckBlock = paragraph.InsertAfterSelf(altChunk);

            altChunckBlock.InsertAfterSelf(_wordprocessingHelper.GetNewLine(body));
        }

        private void PasteTableInfo(IReadOnlyList<OpenXmlElement> annotationElements, Discipline discipline)
        {
            var table = _wordprocessingHelper.GetElementByInnerText<Table>(annotationElements, "Форма обучения");

            PasteInfoAboutCourses(discipline, table);

            PasteInfoAboutSemesters(discipline, table);

            PasteInfoAboutLectureHours(discipline, table);

            PasteInfoAboutPracticalClasseHours(discipline, table);

            PasteInfoAboutLaboratoryClasseHours(discipline, table);

            PasteInfoAboutCourseWorkSemester(discipline, table);

            PasteInfoAboutCourseProjectSemester(discipline, table);

            PasteInfoAboutKnowledgeCheckSemesters(discipline, table, KnowledgeCheckType.Credit, "Зачёт, семестр");

            PasteInfoAboutKnowledgeCheckSemesters(discipline, table, KnowledgeCheckType.Exam, "Экзамен, семестр");

            PasteInfoAboutContactWorkHours(discipline, table);

            PasteInfoAboutSelfStudyHours(discipline, table);

            PasteInfoAboutLaborIntensity(discipline, table);
        }

        private void PasteInfoAboutLaborIntensity(Discipline discipline, Table table)
        {
            var laborIntensityRow =
                _wordprocessingHelper.GetElementByInnerText<TableRow>(table, "Всего часов / зачетных единиц");

            var laborIntensityRowText = $"{discipline.LaborIntensityHours}/{discipline.LaborIntensityCreditUnits}";

            PasteTextToRow(laborIntensityRow, laborIntensityRowText);
        }

        private void PasteInfoAboutSelfStudyHours(Discipline discipline, Table table)
        {
            var selfStudyHoursRow =
                _wordprocessingHelper.GetElementByInnerText<TableRow>(table, "Самостоятельная работа, часы");

            PasteTextToRow(selfStudyHoursRow, discipline.SelfStudyHours.ToString());
        }

        private void PasteInfoAboutContactWorkHours(Discipline discipline, Table table)
        {
            var contactWorkHoursRow =
                _wordprocessingHelper.GetElementByInnerText<TableRow>(table,
                    "Контактная работа по учебным занятиям, часы");

            PasteTextToRow(contactWorkHoursRow, discipline.ContactWorkHours.ToString());
        }

        private void PasteInfoAboutKnowledgeCheckSemesters(Discipline discipline, Table table,
            KnowledgeCheckType knowledgeCheckType, string rowText)
        {
            var creditSemesters = discipline.Semesters
                .Where(_ => _.KnowledgeCheckType == knowledgeCheckType)
                .Select(_ => _.Number)
                .Distinct()
                .OrderBy(_ => _);

            var creditSemestersRow = _wordprocessingHelper.GetElementByInnerText<TableRow>(table, rowText);

            var creditSemestersList = creditSemesters.ToList();
            if (!creditSemestersList.Any())
            {
                creditSemestersRow.Remove();
            }
            else
            {
                var creditSemestersRowText = string.Join(", ", creditSemestersList);
                PasteTextToRow(creditSemestersRow, creditSemestersRowText);
            }
        }

        private void PasteInfoAboutCourseProjectSemester(Discipline discipline, Table table)
        {
            var courseProjectSemesterRow =
                _wordprocessingHelper.GetElementByInnerText<TableRow>(table, "Курсовой проект, семестр");

            if (discipline.CourseProjectSemester is null)
            {
                courseProjectSemesterRow.Remove();
            }
            else
            {
                PasteTextToRow(courseProjectSemesterRow, discipline.CourseProjectSemester.ToString());
            }
        }

        private void PasteInfoAboutCourseWorkSemester(Discipline discipline, Table table)
        {
            var courseWorkSemesterRow =
                _wordprocessingHelper.GetElementByInnerText<TableRow>(table, "Курсовая работа, семестр");

            if (discipline.CourseWorkSemester is null)
            {
                courseWorkSemesterRow.Remove();
            }
            else
            {
                PasteTextToRow(courseWorkSemesterRow, discipline.CourseWorkSemester.ToString());
            }
        }

        private void PasteInfoAboutLaboratoryClasseHours(Discipline discipline, Table table)
        {
            var laboratoryClassesHoursRow =
                _wordprocessingHelper.GetElementByInnerText<TableRow>(table, "Лабораторные занятия, часы");

            if (discipline.LaboratoryClassesHours is null)
            {
                laboratoryClassesHoursRow.Remove();
            }
            else
            {
                PasteTextToRow(laboratoryClassesHoursRow, discipline.LaboratoryClassesHours.ToString());
            }
        }

        private void PasteInfoAboutPracticalClasseHours(Discipline discipline, Table table)
        {
            var practicalClassesHoursRow =
                _wordprocessingHelper.GetElementByInnerText<TableRow>(table, "Практические занятия, часы");

            if (discipline.PracticalClassesHours is null)
            {
                practicalClassesHoursRow.Remove();
            }
            else
            {
                PasteTextToRow(practicalClassesHoursRow, discipline.PracticalClassesHours.ToString());
            }
        }

        private void PasteInfoAboutLectureHours(Discipline discipline, Table table)
        {
            var lectureHoursRow = _wordprocessingHelper.GetElementByInnerText<TableRow>(table, "Лекции, часы");

            PasteTextToRow(lectureHoursRow, discipline.LecturesHours.ToString());
        }

        private void PasteInfoAboutSemesters(Discipline discipline, Table table)
        {
            var semesterRow = _wordprocessingHelper.GetElementByInnerText<TableRow>(table, "Семестр");

            var semesterRowText = GetInfoAboutSemesters(discipline);

            PasteTextToRow(semesterRow, semesterRowText);
        }

        private void PasteInfoAboutCourses(Discipline discipline, Table table)
        {
            var courseRow = _wordprocessingHelper.GetElementByInnerText<TableRow>(table, "Курс");

            var courseRowText = GetInformationAboutCourse(discipline);

            PasteTextToRow(courseRow, courseRowText);
        }

        private void PasteTextToRow(TableRow row, string text)
        {
            var runProperties = (row
                    .ChildElements[1]
                    .ChildElements[1]
                    .ChildElements[1]
                    .ChildElements[0] as RunProperties)?
                .CloneNode(true);

            var run = new Run(new List<OpenXmlElement>
            {
                runProperties,
                new Text(text),
            });

            var courseTextCell = row.ChildElements[2] as TableCell;
            (courseTextCell?.ChildElements[1] as Paragraph)?.AddChild(run);
        }
    }
}