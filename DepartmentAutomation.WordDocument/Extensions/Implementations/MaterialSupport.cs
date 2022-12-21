using System.Collections.Generic;
using System.Linq;
using DepartmentAutomation.Application.Common.Models.WordDocument;
using DepartmentAutomation.WordDocument.Extensions.Interfaces;
using DepartmentAutomation.WordDocument.Helpers.Interfaces;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DepartmentAutomation.WordDocument.Extensions.Implementations
{
    internal class MaterialSupport : IMaterialSupport
    {
        private readonly IWordprocessingHelper _wordprocessingHelper;

        public MaterialSupport(IWordprocessingHelper wordprocessingHelper)
        {
            _wordprocessingHelper = wordprocessingHelper;
        }

        public void PasteInfoAboutMaterialSupport(Body body, IReadOnlyList<Audience> audiences)
        {
            var audiencesText = audiences
                .Select(_ => $"«{_.Number}/{_.BuildingNumber}», рег. номер {_.RegistrationNumber}");

            var resultText = string.Join("; ", audiencesText);

            _wordprocessingHelper.PasteTextIntoMark(body, "materialSupport", resultText);
        }
    }
}