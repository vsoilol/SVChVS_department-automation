using DepartmentAutomation.Application.Common.Models.WordDocument;
using DepartmentAutomation.WordDocument.Extensions.Interfaces;
using DepartmentAutomation.WordDocument.Helpers.Interfaces;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DepartmentAutomation.WordDocument.Extensions.Implementations
{
    internal class FederalStateEducationalStandardInfo : IFederalStateEducationalStandardInfo
    {
        private readonly IWordprocessingHelper _wordprocessingHelper;

        public FederalStateEducationalStandardInfo(IWordprocessingHelper wordprocessingHelper)
        {
            _wordprocessingHelper = wordprocessingHelper;
        }

        public void CreateFederalStateEducationalStandardInfo(Body body, Discipline discipline)
        {
            var paragraph =
                _wordprocessingHelper.GetElementByInnerText<Paragraph>(body, "бакалавриат по направлению подготовки");

            var specialty = discipline.Specialty;
            var text = $"{specialty.Code} {specialty.Name} " +
                       $"№ {specialty.FederalStateEducationalStandard.Code} " +
                       $"от {specialty.FederalStateEducationalStandard.Date:dd.MM.yyyy} г., " +
                       $"учебным планом рег. № {discipline.Curriculum.RegistrationNumber} от " +
                       $"{discipline.Curriculum.ApprovalDate:dd.MM.yyyy} г.";

            paragraph.AppendChild(new Run(new Text(text)));
        }
    }
}