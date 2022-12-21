using System.Collections.Generic;
using System.Linq;
using DepartmentAutomation.Application.Common.Models.WordDocument;
using DepartmentAutomation.WordDocument.Extensions.Interfaces;
using DepartmentAutomation.WordDocument.Helpers.Interfaces;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DepartmentAutomation.WordDocument.Extensions.Implementations
{
    internal class MethodicalRecommendationBlock : IMethodicalRecommendationBlock
    {
        private readonly IWordprocessingHelper _wordprocessingHelper;

        public MethodicalRecommendationBlock(IWordprocessingHelper wordprocessingHelper)
        {
            _wordprocessingHelper = wordprocessingHelper;
        }

        public void CreateMethodicalRecommendationBlock(Body body,
            IReadOnlyList<MethodicalRecommendation> methodicalRecommendations)
        {
            var paragraph = _wordprocessingHelper.GetElementByInnerText<Paragraph>(body, "Методические рекомендации");

            for (var i = 0; i < methodicalRecommendations.Count(); i++)
            {
                paragraph = paragraph
                    .InsertAfterSelf(CreateMethodicalRecommendationInfoParagraph(body, methodicalRecommendations.ElementAt(i), i + 1));
            }
        }

        private Paragraph CreateMethodicalRecommendationInfoParagraph(Body body,
            MethodicalRecommendation methodicalRecommendation, int number)
        {
            return _wordprocessingHelper
                .CreateParagraphWithText($"{number}.   {methodicalRecommendation.Content}", body, new RunProperties());
        }
    }
}