using System.Collections.Generic;
using System.Linq;
using DepartmentAutomation.Application.Common.Models.WordDocument;
using DepartmentAutomation.Domain.Enums;
using DepartmentAutomation.WordDocument.Extensions.Interfaces;
using DepartmentAutomation.WordDocument.Helpers.Interfaces;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DepartmentAutomation.WordDocument.Extensions.Implementations
{
    internal class TrainingCourseFormTable : ITrainingCourseFormTable
    {
        private const string TableFontSize = "20";
        private readonly ITableHelper _tableHelper;

        private readonly IWordprocessingHelper _wordprocessingHelper;

        public TrainingCourseFormTable(ITableHelper tableHelper, IWordprocessingHelper wordprocessingHelper)
        {
            _tableHelper = tableHelper;
            _wordprocessingHelper = wordprocessingHelper;
        }

        public void CreateTrainingCourseFormTable(Body body, IReadOnlyList<TrainingCourseForm> trainingCourseForms)
        {
            if (!trainingCourseForms.Any())
            {
                return;
            }
            
            var table = _wordprocessingHelper.GetElementByInnerText<Table>(body, "Форма проведения занятия");

            var isLestures =
                trainingCourseForms.Any(_ => _.Lessons.Any(lesson => lesson.LessonType == LessonType.Lecture));
            var isPracticalLesson =
                trainingCourseForms.Any(_ => _.Lessons.Any(lesson => lesson.LessonType == LessonType.Practical));
            var isLaboratoryLesson =
                trainingCourseForms.Any(_ => _.Lessons.Any(lesson => lesson.LessonType == LessonType.Laboratory));

            CheckHeader(table, isLestures, isPracticalLesson, isLaboratoryLesson);

            for (var i = 0; i < trainingCourseForms.Count(); i++)
            {
                PasteTrainingCourseFormInfo(table, trainingCourseForms.ElementAt(i), i + 1, isLestures,
                    isPracticalLesson, isLaboratoryLesson);
            }

            PasteResultHoursInfo(table, trainingCourseForms, isLestures, isPracticalLesson, isLaboratoryLesson);
        }

        private void PasteResultHoursInfo(Table table, IReadOnlyList<TrainingCourseForm> trainingCourseForms,
            bool isLecture,
            bool isPracticalLesson,
            bool isLaboratoryLesson)
        {
            var row = new TableRow();
            row.Append(_tableHelper.CreateEmptyCell(TableFontSize));

            var textResultCell =
                _tableHelper.CreateCellWithParagraph(_wordprocessingHelper.CreateBoldParagraph("ИТОГО", TableFontSize));
            row.Append(textResultCell);

            if (isLecture)
            {
                var lecture =
                    trainingCourseForms.SelectMany(_ => _.Lessons.Where(item => item.LessonType == LessonType.Lecture));
                PasteInfoAboutTotalLessonHours(row, lecture);
            }

            if (isPracticalLesson)
            {
                var practicalLesson =
                    trainingCourseForms.SelectMany(_ =>
                        _.Lessons.Where(lesson => lesson.LessonType == LessonType.Practical));
                PasteInfoAboutTotalLessonHours(row, practicalLesson);
            }

            if (isLaboratoryLesson)
            {
                var laboratoryLessons =
                    trainingCourseForms.SelectMany(_ =>
                        _.Lessons.Where(lesson => lesson.LessonType == LessonType.Laboratory));
                PasteInfoAboutTotalLessonHours(row, laboratoryLessons);
            }

            var allLessons = trainingCourseForms.SelectMany(_ => _.Lessons);
            PasteInfoAboutTotalLessonHours(row, allLessons, JustificationValues.Center);

            table.Append(row);
        }

        private void PasteInfoAboutTotalLessonHours(TableRow row, IEnumerable<Lesson> lessons,
            JustificationValues justificationValues = JustificationValues.Left)
        {
            var lessonHours = lessons.Sum(_ => _.Hours);
            var lessonHoursCell = _tableHelper.CreateCellWithFontSizeAndJustification(lessonHours.ToString(),
                justificationValues, TableFontSize);
            row.Append(lessonHoursCell);
        }

        private void PasteTrainingCourseFormInfo(Table table,
            TrainingCourseForm trainingCourseForm,
            int number,
            bool isLecture,
            bool isPracticalLesson,
            bool isLaboratoryLesson)
        {
            var row = new TableRow();

            var numberCell = _tableHelper
                .CreateCellWithFontSizeAndJustification(number.ToString(),
                    JustificationValues.Left,
                    TableFontSize);
            row.Append(numberCell);

            var nameCell = _tableHelper
                .CreateCellWithFontSizeAndJustification(trainingCourseForm.Name, JustificationValues.Left,
                    TableFontSize);
            row.Append(nameCell);

            if (isLecture)
            {
                PasteLessonsInfo(row,
                    trainingCourseForm.Lessons.Where(_ => _.LessonType == LessonType.Lecture).ToList(),
                    "Темы");
            }

            if (isPracticalLesson)
            {
                PasteLessonsInfo(row, trainingCourseForm.Lessons.Where(_ => _.LessonType == LessonType.Practical).ToList(),
                    "Пр.р.№");
            }

            if (isLaboratoryLesson)
            {
                PasteLessonsInfo(row, trainingCourseForm.Lessons.Where(_ => _.LessonType == LessonType.Laboratory).ToList(),
                    "Л.р.№");
            }

            var allHoursCell = _tableHelper
                .CreateCellWithFontSizeAndJustification(trainingCourseForm.Lessons.Sum(_ => _.Hours).ToString(),
                    JustificationValues.Center,
                    TableFontSize,
                    TableVerticalAlignmentValues.Center);
            row.Append(allHoursCell);

            table.Append(row);
        }

        private void PasteLessonsInfo(TableRow row, IReadOnlyList<Lesson> lessons, string startText)
        {
            if (lessons.Any())
            {
                var lessonsNumber = GetLessonsNumber(lessons.ToList());

                var lessonCell = _tableHelper
                    .CreateCellWithFontSizeAndJustification($"{startText} {lessonsNumber}",
                        JustificationValues.Left,
                        TableFontSize,
                        TableVerticalAlignmentValues.Center);
                row.Append(lessonCell);
                return;
            }

            row.Append(_tableHelper.CreateEmptyCell(TableFontSize));
        }

        private string GetLessonsNumber(List<Lesson> lessons)
        {
            lessons = lessons.OrderBy(_ => _.Number).ToList();
            return $"{lessons.First().Number}-{lessons.Last().Number}";
        }

        private void CheckHeader(Table table, bool isLestures, bool isPracticalLesson, bool isLaboratoryLesson)
        {
            var header = table.ChildElements.GetItem(3) as TableRow;
            var classesTypeCell =
                _wordprocessingHelper.GetElementByInnerText<TableCell>(table, "Вид аудиторных занятий");

            var columnNumber = 3;

            if (!isLestures)
            {
                CheckLessonColumn(table, header, classesTypeCell, "Лекции", ref columnNumber);
            }

            if (!isPracticalLesson)
            {
                CheckLessonColumn(table, header, classesTypeCell, "Практические занятия", ref columnNumber);
            }

            if (!isLaboratoryLesson)
            {
                CheckLessonColumn(table, header, classesTypeCell, "Лабораторные занятия", ref columnNumber);
            }
        }

        private void CheckLessonColumn(Table table, TableRow header, TableCell classesTypeCell, string lessonName,
            ref int columnNumber)
        {
            _tableHelper.DeleteCell(header, lessonName);
            var element = (table.ChildElements.GetItem(1) as TableGrid)?.ChildElements.FirstOrDefault();
            element?.Remove();

            var tableCellProperties = classesTypeCell.GetFirstChild<TableCellProperties>();

            var lessonColumnWidth =
                int.Parse(tableCellProperties.GetFirstChild<TableCellWidth>().Width) / --columnNumber;

            tableCellProperties.GetFirstChild<GridSpan>().Val = columnNumber;

            var lessonColumns = header.ChildElements.Skip(2).Take(columnNumber);

            foreach (var column in lessonColumns)
            {
                column.GetFirstChild<TableCellProperties>().GetFirstChild<TableCellWidth>().Width =
                    lessonColumnWidth.ToString();
            }
        }
    }
}