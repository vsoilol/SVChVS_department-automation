using DepartmentAutomation.Application.Common.Mappings;
using DepartmentAutomation.Domain.Entities.InformationBlockInfo;

namespace DepartmentAutomation.Application.Contracts.Responses
{
    public class InformationBlockWithoutContentDto : IMapFrom<InformationBlock>
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public string Name { get; set; }
    }
}