using System.Collections.Generic;
using System.Linq;
using DepartmentAutomation.Application.Common.Models.WordDocument;
using DepartmentAutomation.Domain.Enums;
using DepartmentAutomation.WordDocument.Extensions.Interfaces;
using DepartmentAutomation.WordDocument.Helpers.Interfaces;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DepartmentAutomation.WordDocument.Extensions.Implementations
{
    internal class MainPageTable : IMainPageTable
    {
        private readonly IWordprocessingHelper _wordprocessingHelper;

        public MainPageTable(IWordprocessingHelper wordprocessingHelper)
        {
            _wordprocessingHelper = wordprocessingHelper;
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

        private static string GetInfoAboutSemesters(Discipline discipline)
        {
            var semesters = discipline.Semesters
                .Select(_ => _.Number)
                .Distinct()
                .OrderBy(_ => _);

            var semesterRowText = string.Join(", ", semesters);
            return semesterRowText;
        }

        public void CreateMainTable(Body body, Discipline discipline)
        {
            var table = body.Descendants<Table>()
                .FirstOrDefault(tbl => tbl.InnerText.Contains("Форма обучения"));

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
                .OrderBy(_ => _)
                .ToList();

            var creditSemestersRow = _wordprocessingHelper.GetElementByInnerText<TableRow>(table, rowText);

            if (!creditSemesters.Any())
            {
                creditSemestersRow.Remove();
            }
            else
            {
                var creditSemestersRowText = string.Join(", ", creditSemesters);
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
            (courseTextCell.ChildElements[1] as Paragraph).AddChild(run);
        }
    }
}