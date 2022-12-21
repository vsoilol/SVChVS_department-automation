using System.Collections.Generic;
using DepartmentAutomation.Application.Common.Mappings;
using DepartmentAutomation.Domain.Enums;

namespace DepartmentAutomation.Application.Common.Models.WordDocument
{
    public class CompetenceFormationLevel : IMapFrom<Domain.Entities.CompetenceInfo.CompetenceFormationLevel>
    {
        public int LevelNumber { get; set; }

        public FormationLevel FormationLevel { get; set; }

        /// <summary>
        ///     Содержательное описание уровня
        /// </summary>
        public string FactualDescription { get; set; }

        /// <summary>
        ///     Результаты обучения
        /// </summary>
        public string LearningOutcomes { get; set; }

        public List<EvaluationToolType> EvaluationToolTypes { get; set; }
    }
}