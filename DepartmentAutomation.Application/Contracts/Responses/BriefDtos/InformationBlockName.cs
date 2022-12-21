using AutoMapper;
using DepartmentAutomation.Application.Common.Mappings;
using DepartmentAutomation.Domain.Entities.InformationBlockInfo;

namespace DepartmentAutomation.Application.Contracts.Responses.BriefDtos
{
    public class InformationBlockName : IMapFrom<InformationBlock>
    {
        public string Name { get; set; }
        
        public string Number { get; set; }
    }
}