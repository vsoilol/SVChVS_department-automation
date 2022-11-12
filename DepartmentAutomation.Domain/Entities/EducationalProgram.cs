using DepartmentAutomation.Domain.Contracts;
using DepartmentAutomation.Domain.Entities.CompetenceInfo;
using DepartmentAutomation.Domain.Entities.EvaluationToolInfo;
using DepartmentAutomation.Domain.Entities.InformationBlockInfo;
using DepartmentAutomation.Domain.Entities.KnowledgeControlFormInfo;
using DepartmentAutomation.Domain.Entities.LiteratureInfo;
using DepartmentAutomation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepartmentAutomation.Domain.Entities
{
    /// <summary>
    /// Учебная программа
    /// </summary>
    public class EducationalProgram : Entity<int>
    {
        /// <summary>
        /// Дата утверждения
        /// </summary>
        public DateTime? ApprovalDate { get; set; }

        /// <summary>
        /// Рассмотрена и рекомендована к утверждению
        /// </summary>
        public DateTime? ApprovalRecommendedDate { get; set; }

        public int? ProtocolNumber { get; set; }

        [Required]
        public int DisciplineId { get; set; }

        [ForeignKey(nameof(DisciplineId))]
        public virtual Discipline Discipline { get; set; }

        public virtual List<TrainingCourseForm> TrainingCourseForms { get; set; }

        public virtual List<MethodicalRecommendation> MethodicalRecommendations { get; set; }

        public virtual List<EvaluationTool> EvaluationTools { get; set; }

        public virtual List<InformationBlockContent> InformationBlockContents { get; set; }

        public virtual List<LiteratureTypeInfo> LiteratureTypeInfos { get; set; }

        public virtual List<Inspector> Inspectors { get; set; }

        public virtual List<Week> Weeks { get; set; }

        public virtual List<KnowledgeControlForm> KnowledgeControlForms { get; set; }

        public virtual List<Lesson> Lessons { get; set; }

        public virtual List<CompetenceFormationLevel> CompetenceFormationLevels { get; set; }

        public virtual List<Audience> Audiences { get; set; }

        public virtual List<Protocol> Protocols { get; set; }

        public int? ReviewerId { get; set; }

        [ForeignKey(nameof(ReviewerId))]

        public virtual Reviewer Reviewer { get; set; }
    }
}