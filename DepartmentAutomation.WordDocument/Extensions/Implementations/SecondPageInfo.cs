using System.Collections.Generic;
using System.Linq;
using DepartmentAutomation.Application.Common.Models.WordDocument;
using DepartmentAutomation.WordDocument.Extensions.Interfaces;
using DepartmentAutomation.WordDocument.Helpers.Interfaces;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DepartmentAutomation.WordDocument.Extensions.Implementations
{
    internal class SecondPageInfo : ISecondPageInfo
    {
        private readonly IWordprocessingHelper _wordprocessingHelper;
        private readonly IMonthHelper _monthHelper;

        public SecondPageInfo(IWordprocessingHelper wordprocessingHelper, IMonthHelper monthHelper)
        {
            _wordprocessingHelper = wordprocessingHelper;
            _monthHelper = monthHelper;
        }

        private static void DeleteInfoAboutGraduatingDepartment(IEnumerable<OpenXmlElement> elements)
        {
            var table = elements.FirstOrDefault() as Table;
            var elementsAfterTable = table.ElementsAfter();
            var emptyParagraph = elementsAfterTable.TakeWhile(_ => _ is not Table);

            table.Remove();

            foreach (var openXmlElement in emptyParagraph)
            {
                openXmlElement.Remove();
            }
        }

        public void CreateSecondPageInfo(Body body, EducationalProgram educationalProgram)
        {
            var elements = _wordprocessingHelper
                .GetElementByInnerText<Paragraph>(body, "Рабочая программа составлена в соответствии")
                .ElementsAfter()
                .ToList();

            _wordprocessingHelper.PasteTextIntoMark(elements, "DepartmentName",
                $"«{educationalProgram.Discipline.Department.Name}»");

            PasteProtocolInfo(elements, educationalProgram.Protocols);

            PasteDepartmentHeadInfo(elements, educationalProgram.Discipline.Department);

            PasteReviewerInfo(elements, educationalProgram.Reviewer);

            CheckGraduatingDepartmentInfo(elements, educationalProgram.Discipline.Department,
                educationalProgram.Discipline.Specialty.Department);
        }

        private void CheckGraduatingDepartmentInfo(List<OpenXmlElement> elements, Department developerDepartment,
            Department graduatingDepartment)
        {
            elements = _wordprocessingHelper.GetElementByInnerText<Paragraph>(elements, "Рабочая программа согласована")
                .ElementsAfter()
                .ToList();

            if (developerDepartment.Id == graduatingDepartment.Id)
            {
                DeleteInfoAboutGraduatingDepartment(elements);
            }
            else
            {
                _wordprocessingHelper.PasteTextIntoMark(elements, "Short",  " " + graduatingDepartment.ShortName);
                _wordprocessingHelper.PasteTextIntoMark(elements, "DepartmentHead",
                    $"{graduatingDepartment.DepartmentHead.Name[0]}." + $"{graduatingDepartment.DepartmentHead.Patronymic[0]}. " + $"{graduatingDepartment.DepartmentHead.Surname}");
            }
        }

        public void PasteReviewerInfo(IReadOnlyList<OpenXmlElement> elements, Reviewer reviewer)
        {
            var info = "Нету данных";
            
            if (!string.IsNullOrEmpty(reviewer.Name) && !string.IsNullOrEmpty(reviewer.Patronymic))
            {
                info = $"{reviewer.Position}    {reviewer.Name[0]}.{reviewer.Patronymic[0]}. {reviewer.Surname}";
            }
            
            _wordprocessingHelper.PasteTextIntoMark(elements, "Reviewers", info);
        }

        public void PasteDepartmentHeadInfo(IReadOnlyList<OpenXmlElement> elements, Department department)
        {
            _wordprocessingHelper.PasteTextIntoMark(elements, "Short", department.ShortName);

            var fio = $"{department.DepartmentHead.Name[0]}. " +
                      $"{department.DepartmentHead.Patronymic[0]}. " +
                      $"{department.DepartmentHead.Surname}";

            _wordprocessingHelper.PasteTextIntoMark(elements, "DepartmentHead", fio);
        }

        public void PasteProtocolInfo(IReadOnlyList<OpenXmlElement> elements, IEnumerable<Protocol> protocols)
        {
            var text = "";

            foreach (var protocol in protocols)
            {
                text += $"«{protocol.Date.Day}» " +
                        $"{_monthHelper.GetMonthInRussian(protocol.Date.Month)} " +
                        $"{protocol.Date.Year} г., протокол № {protocol.Number}. ";
            }

            _wordprocessingHelper.PasteTextIntoMark(elements, "ProtocolInfo", text);
        }
    }
}