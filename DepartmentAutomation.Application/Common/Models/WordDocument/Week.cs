using System.Collections.Generic;
using AutoMapper;
using DepartmentAutomation.Application.Common.Mappings;

namespace DepartmentAutomation.Application.Common.Models.WordDocument
{
    public class Week : IMapFrom<Domain.Entities.Week>
    {
        public int Number { get; set; }

        public int IndependentWorkHours { get; set; }

        public int TrainingModuleNumber { get; set; }

        public List<Lesson> Lessons { get; set; }

        public List<KnowledgeAssessment> KnowledgeAssessments { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Week, Week>()
                .ForMember(dto => dto.KnowledgeAssessments,
                    opt => opt.MapFrom(x => x.KnowledgeAssessments))
                .ForMember(dto => dto.Lessons,
                    opt => opt.MapFrom(x => x.Lessons));
        }
    }
}