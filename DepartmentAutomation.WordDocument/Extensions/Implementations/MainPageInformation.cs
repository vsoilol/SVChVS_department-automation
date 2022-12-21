using System.Collections.Generic;
using System.Text;
using DepartmentAutomation.Application.Common.Models.WordDocument;
using DepartmentAutomation.WordDocument.Extensions.Interfaces;
using DepartmentAutomation.WordDocument.Helpers.Interfaces;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DepartmentAutomation.WordDocument.Extensions.Implementations
{
    internal class MainPageInformation : IMainPageInformation
    {
        private readonly IWordprocessingHelper _wordprocessingHelper;

        public MainPageInformation(IWordprocessingHelper wordprocessingHelper)
        {
            _wordprocessingHelper = wordprocessingHelper;
        }

        public void CreateMainPage(Body body, Discipline discipline)
        {
            var trainingDirectionInfo = $"{discipline.Specialty.Code} " +
                                        $"{discipline.Specialty.Name}";

            _wordprocessingHelper.PasteTextIntoMark(body, "TrainingDirection", trainingDirectionInfo);

            _wordprocessingHelper.PasteTextIntoMark(body, "Profile", discipline.Specialty.ProfileName);

            _wordprocessingHelper.PasteTextIntoMark(body, "Qualification", discipline.Specialty.Qualification);

            _wordprocessingHelper.PasteTextIntoMark(body, "LearningForm", discipline.Specialty.LearningForm);

            _wordprocessingHelper.PasteTextIntoMark(body, "DepartmentName", discipline.Department.Name);

            _wordprocessingHelper.PasteTextIntoMark(body, "TeachersInfo", GetTeachersInfo(discipline.Teachers));

            _wordprocessingHelper.PasteTextIntoMark(body, "DisciplineName", discipline.Name.ToUpper());
        }

        private static string GetTeachersInfo(IReadOnlyList<Teacher> teachers)
        {
            var stringBuilder = new StringBuilder();

            foreach (var teacher in teachers)
            {
                stringBuilder.Append(teacher.Name[0] + "." + teacher.Patronymic[0] + ". " + teacher.Surname + ", ");
                stringBuilder.Append(teacher.AcademicDegree is null
                    ? teacher.PositionShort
                    : teacher.AcademicDegreeShort);

                if (teachers[^1] != teacher)
                {
                    stringBuilder.Append("; ");
                }
            }

            return stringBuilder.ToString();
        }
    }
}