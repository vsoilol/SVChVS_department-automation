using System.Collections.Generic;
using System.Linq;
using DepartmentAutomation.Application.Common.Models.WordDocument;
using DepartmentAutomation.Domain.Enums;
using DepartmentAutomation.WordDocument.Extensions.Interfaces;
using DepartmentAutomation.WordDocument.Helpers.Interfaces;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DepartmentAutomation.WordDocument.Extensions.Implementations
{
    internal class WeeksTable : IWeeksTable
    {
        private const string TableFontSize = "18";

        private readonly Dictionary<int, int> _semestersIndependentWorkHours = new ();

        private readonly ITableHelper _tableHelper;
        private readonly IWordprocessingHelper _wordprocessingHelper;

        private bool _isPracticalLessons;
        private bool _isLaboratoryLessons;

        public WeeksTable(ITableHelper tableHelper, IWordprocessingHelper wordprocessingHelper)
        {
            _tableHelper = tableHelper;
            _wordprocessingHelper = wordprocessingHelper;
        }

        public void CreateWeeksTable(Body body, Discipline discipline)
        {
            var table = _wordprocessingHelper.GetElementByInnerText<Table>(body, "№ недели");

            _isPracticalLessons = discipline.PracticalClassesHours is not null;
            _isLaboratoryLessons = discipline.LaboratoryClassesHours is not null;

            var cellsNumber = 10;
            var headerRow = table.ChildElements.GetItem(2) as TableRow;

            if (!_isPracticalLessons)
            {
                RemoveCellWithHours(headerRow, "Практические");
                cellsNumber -= 2;
            }

            if (!_isLaboratoryLessons)
            {
                RemoveCellWithHours(headerRow, "Лабораторные");
                cellsNumber -= 2;
            }

            foreach (var semester in discipline.Semesters)
            {
                _semestersIndependentWorkHours.Add(semester.Number, 0);

                var row = new TableRow();

                var semesterName =
                    _tableHelper.CreateCellWithFontSizeAndJustification($"Семестр {semester.Number}",
                        JustificationValues.Left,
                        TableFontSize);

                var gridSpan1 = new GridSpan { Val = cellsNumber };
                semesterName.Append(gridSpan1);

                row.Append(semesterName);
                table.Append(row);

                var firstModuleWeeks = semester.Weeks.Where(_ => _.TrainingModuleNumber == 1);
                AddWeeksToTable(table, firstModuleWeeks.ToList(), cellsNumber);

                var secondModuleWeeks = semester.Weeks.Where(_ => _.TrainingModuleNumber == 2);
                AddWeeksToTable(table, secondModuleWeeks.ToList(), cellsNumber);

                PasteAdditionalInfo(discipline, semester, table);

                var weeks = semester.Weeks;

                table.Append(CreateHoursResultRow(weeks, semester.Number));
            }
        }

        private void PasteAdditionalInfo(Discipline discipline, Semester semester, Table table)
        {
            switch (semester.KnowledgeCheckType)
            {
                case KnowledgeCheckType.Credit:
                    PasteCreditKnowledgeAssessments(table, semester.Number);
                    break;
                case KnowledgeCheckType.Exam:
                    PasteExamKnowledgeAssessments(table, semester.Number, semester.Weeks.Max(_ => _.Number),
                        semester.ExamEndWeek);
                    break;
            }

            if (discipline.CourseProjectSemester is not null && discipline.CourseProjectSemester == semester.Number)
            {
                _semestersIndependentWorkHours[semester.Number] += 36;
                PasteCourseProjectInfo(table, semester.CourseProjectEndWeek,
                    "Выполнение курсового проекта");
            }

            if (discipline.CourseWorkSemester is not null && discipline.CourseWorkSemester == semester.Number)
            {
                _semestersIndependentWorkHours[semester.Number] += 36;
                PasteCourseProjectInfo(table, semester.CourseProjectEndWeek,
                    "Выполнение курсовой работы");
            }
        }

        private void PasteCourseProjectInfo(Table table, int courseProjectWeekEnd, string text)
        {
            var weekInfoRow = new TableRow();

            var numberWeekCell = _tableHelper.CreateCellWithFontSizeAndJustification(
                $"1-{courseProjectWeekEnd}",
                JustificationValues.Center,
                TableFontSize, TableVerticalAlignmentValues.Center);

            weekInfoRow.Append(numberWeekCell);

            var courseProjectTextCell = _tableHelper.CreateCellWithFontSizeAndJustification(
                text,
                JustificationValues.Left,
                TableFontSize, TableVerticalAlignmentValues.Center);

            weekInfoRow.Append(courseProjectTextCell);

            weekInfoRow.Append(_tableHelper.CreateEmptyCell(TableFontSize));

            if (_isLaboratoryLessons)
            {
                weekInfoRow.Append(_tableHelper.CreateEmptyCell(TableFontSize),
                    _tableHelper.CreateEmptyCell(TableFontSize));
            }

            if (_isPracticalLessons)
            {
                weekInfoRow.Append(_tableHelper.CreateEmptyCell(TableFontSize),
                    _tableHelper.CreateEmptyCell(TableFontSize));
            }

            var independentWorkHoursCell = _tableHelper.CreateCellWithFontSizeAndJustification(
                "36",
                JustificationValues.Center,
                TableFontSize, TableVerticalAlignmentValues.Center);

            weekInfoRow.Append(independentWorkHoursCell);

            weekInfoRow.Append(_tableHelper.CreateEmptyCell(TableFontSize),
                _tableHelper.CreateEmptyCell(TableFontSize));

            table.Append(weekInfoRow);
        }

        private void PasteExamKnowledgeAssessments(Table table, int semesterNumber, int maxWeekNumber,
            int examWeekEnd)
        {
            _semestersIndependentWorkHours[semesterNumber] += 36;

            var weekInfoRow = new TableRow();

            var numberWeekCell = _tableHelper.CreateCellWithFontSizeAndJustification(
                $"{maxWeekNumber + 1}-{examWeekEnd}",
                JustificationValues.Center,
                TableFontSize, TableVerticalAlignmentValues.Center);

            weekInfoRow.Append(numberWeekCell);

            weekInfoRow.Append(_tableHelper.CreateEmptyCell(TableFontSize), _tableHelper.CreateEmptyCell(TableFontSize));

            if (_isLaboratoryLessons)
            {
                weekInfoRow.Append(_tableHelper.CreateEmptyCell(TableFontSize),
                    _tableHelper.CreateEmptyCell(TableFontSize));
            }

            if (_isPracticalLessons)
            {
                weekInfoRow.Append(_tableHelper.CreateEmptyCell(TableFontSize),
                    _tableHelper.CreateEmptyCell(TableFontSize));
            }

            var knowledgeAssessmentsCell = _tableHelper.CreateCellWithFontSizeAndJustification(
                "ПА (экзамен)",
                JustificationValues.Center,
                TableFontSize, TableVerticalAlignmentValues.Center);

            var marksCell = _tableHelper.CreateCellWithFontSizeAndJustification(
                "40",
                JustificationValues.Center,
                TableFontSize, TableVerticalAlignmentValues.Center);

            var independentWorkHoursCell = _tableHelper.CreateCellWithFontSizeAndJustification(
                "36",
                JustificationValues.Center,
                TableFontSize, TableVerticalAlignmentValues.Center);

            weekInfoRow.Append(independentWorkHoursCell, knowledgeAssessmentsCell, marksCell);

            table.Append(weekInfoRow);
        }

        private void PasteCreditKnowledgeAssessments(Table table, int semesterNumber)
        {
            var rows = table
                .FirstOrDefault(_ => _.InnerText.Contains($"Семестр {semesterNumber}"))
                .ElementsAfter()
                .Last();

            var index = rows.ChildElements.Count - 2;
            var knowledgeAssessmentsCell = rows.ChildElements[index] as TableCell;
            knowledgeAssessmentsCell.Append(_wordprocessingHelper
                .CreateParagraphWithText("ПА (зачет)",
                    TableFontSize, JustificationValues.Center));
        }

        private void RemoveCellWithHours(TableRow headerRow, string cellName)
        {
            var cell = headerRow.Descendants<TableCell>()
                .FirstOrDefault(c => c.InnerText.Contains(cellName));

            var hoursCell = cell
                .ElementsAfter()
                .FirstOrDefault(_ => _.InnerText.Contains("Часы")) as TableCell;

            cell.Remove();
            hoursCell.Remove();
        }

        private void AddWeeksToTable(Table table, List<Week> weeks, int cellsNumber)
        {
            var moduleNumber = weeks.First().TrainingModuleNumber;

            var row = new TableRow();

            var semesterNameCell = _tableHelper.CreateCellWithFontSizeAndJustification($"Модуль {moduleNumber}",
                JustificationValues.Left,
                TableFontSize);

            semesterNameCell.Append(new GridSpan { Val = cellsNumber });
            row.Append(semesterNameCell);
            table.Append(row);

            foreach (var week in weeks)
            {
                var weekInfoRow = new TableRow();

                PasteNumberWeekInfo(week, weekInfoRow);

                PasteLessonInfo(week, weekInfoRow);

                if (week.Lessons.Any())
                {
                    PasteIndependentWorkHoursInfo(week, weekInfoRow);

                    PasteKnowledgeAssessmentsInfo(week, weekInfoRow);
                }
                else
                {
                    weekInfoRow.Append(_tableHelper.CreateEmptyCell(TableFontSize));
                    weekInfoRow.Append(_tableHelper.CreateEmptyCell(TableFontSize));
                    weekInfoRow.Append(_tableHelper.CreateEmptyCell(TableFontSize));
                }

                table.Append(weekInfoRow);
            }
        }

        private void PasteNumberWeekInfo(Week week, TableRow weekInfoRow)
        {
            var numberWeekCell = _tableHelper.CreateCellWithFontSizeAndJustification(week.Number.ToString(),
                JustificationValues.Center,
                TableFontSize, TableVerticalAlignmentValues.Center);

            weekInfoRow.Append(numberWeekCell);
        }

        private void PasteKnowledgeAssessmentsInfo(Week week, TableRow weekInfoRow)
        {
            var knowledgeAssessmentsCell = _tableHelper.CreateCellWithAlignment(TableVerticalAlignmentValues.Center);
            var marksCell = _tableHelper.CreateCellWithAlignment(TableVerticalAlignmentValues.Center);

            foreach (var assessment in week.KnowledgeAssessments)
            {
                knowledgeAssessmentsCell
                    .Append(_wordprocessingHelper.CreateParagraphWithText(assessment.KnowledgeControlForm.ShortName,
                        TableFontSize, JustificationValues.Center));

                marksCell
                    .Append(_wordprocessingHelper.CreateParagraphWithText(assessment.MaxMark.ToString(),
                        TableFontSize, JustificationValues.Center));
            }

            if (!week.KnowledgeAssessments.Any())
            {
                knowledgeAssessmentsCell
                    .Append(_wordprocessingHelper.CreateEmptyParagraph(TableFontSize));

                marksCell
                    .Append(_wordprocessingHelper.CreateEmptyParagraph(TableFontSize));
            }

            weekInfoRow.Append(knowledgeAssessmentsCell, marksCell);
        }

        private void PasteIndependentWorkHoursInfo(Week week, TableRow weekInfoRow)
        {
            var independentWorkHoursCell = _tableHelper.CreateCellWithFontSizeAndJustification(
                week.IndependentWorkHours.ToString(),
                JustificationValues.Center,
                TableFontSize, TableVerticalAlignmentValues.Center);

            weekInfoRow.Append(independentWorkHoursCell);
        }

        private void PasteLessonInfo(Week week, TableRow weekInfoRow)
        {
            AddLessonToTable(week, weekInfoRow, LessonType.Lecture, "Тема {0}. {1}");

            if (_isPracticalLessons)
            {
                AddLessonToTable(week, weekInfoRow, LessonType.Practical, "Пр. р. {0}. {1}");
            }

            if (_isLaboratoryLessons)
            {
                AddLessonToTable(week, weekInfoRow, LessonType.Laboratory, "Лр. р. {0}. {1}");
            }
        }

        private void AddLessonToTable(Week week, TableRow weekInfoRow, LessonType lessonType, string text)
        {
            var lesson = week.Lessons.FirstOrDefault(_ => _.LessonType == lessonType);

            var lessonNameCell = _tableHelper.CreateEmptyCell(TableFontSize);
            var lessonHoursCell = _tableHelper.CreateEmptyCell(TableFontSize);

            if (lesson is not null)
            {
                lessonNameCell = _tableHelper.CreateCellWithFontSizeAndJustification(
                    string.Format(text, lesson.Number, lesson.Name),
                    JustificationValues.Left,
                    TableFontSize, TableVerticalAlignmentValues.Center);

                lessonHoursCell = _tableHelper.CreateCellWithFontSizeAndJustification(lesson.Hours.ToString(),
                    JustificationValues.Center,
                    TableFontSize, TableVerticalAlignmentValues.Center);
            }

            weekInfoRow.Append(lessonNameCell, lessonHoursCell);
        }

        private TableRow CreateHoursResultRow(IReadOnlyList<Week> weeks, int semesterNumber)
        {
            var resultsRow = new TableRow();

            var resultsTextCell =
                _tableHelper.CreateCellWithFontSizeAndJustification("Итого", JustificationValues.Left, TableFontSize);

            resultsTextCell.Append(new GridSpan { Val = 2 });

            var lessons = weeks.SelectMany(_ => _.Lessons).ToList();

            var lectureResultCell = CreateCellWithLessonHoursInfo(LessonType.Lecture, lessons);

            resultsRow.Append(resultsTextCell, lectureResultCell);

            if (_isPracticalLessons)
            {
                var emptyCell = _tableHelper.CreateEmptyCell(TableFontSize);

                var practicalLessonsResultCell =
                    CreateCellWithLessonHoursInfo(LessonType.Practical, lessons);

                resultsRow.Append(emptyCell);
                resultsRow.Append(practicalLessonsResultCell);
            }

            if (_isLaboratoryLessons)
            {
                var emptyCell = _tableHelper.CreateEmptyCell(TableFontSize);

                var laboratoryLessonsResultCell =
                    CreateCellWithLessonHoursInfo(LessonType.Laboratory, lessons);

                resultsRow.Append(emptyCell);
                resultsRow.Append(laboratoryLessonsResultCell);
            }

            var independentWorkHours = weeks
                .Sum(_ => _.IndependentWorkHours);
            independentWorkHours += _semestersIndependentWorkHours[semesterNumber];

            var independentWorkHoursCell = _tableHelper.CreateCellWithFontSizeAndJustification(independentWorkHours.ToString(),
                JustificationValues.Center,
                TableFontSize);

            resultsRow.Append(independentWorkHoursCell);

            var emptyCell1 = _tableHelper.CreateEmptyCell(TableFontSize);
            resultsRow.Append(emptyCell1);

            var marksResult = weeks
                .Select(_ => _.KnowledgeAssessments
                    .Sum(assessment => assessment.MaxMark))
                .Sum()
                .ToString();

            var marksResultCell = _tableHelper.CreateCellWithFontSizeAndJustification(marksResult,
                JustificationValues.Center,
                TableFontSize);

            resultsRow.Append(marksResultCell);

            return resultsRow;
        }

        private TableCell CreateCellWithLessonHoursInfo(LessonType lessonType, IEnumerable<Lesson> lessons)
        {
            var lessonsHours = lessons
                .Where(lesson => lesson.LessonType == lessonType)
                .Sum(_ => _.Hours)
                .ToString();

            var lessonsHoursCell =
                _tableHelper.CreateCellWithFontSizeAndJustification(lessonsHours, JustificationValues.Center,
                    TableFontSize);

            return lessonsHoursCell;
        }
    }
}