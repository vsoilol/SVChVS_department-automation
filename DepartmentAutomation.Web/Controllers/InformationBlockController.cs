using System.Collections.Generic;
using System.Threading.Tasks;
using DepartmentAutomation.Application.Common.Attributes;
using DepartmentAutomation.Application.Contracts.Responses;
using DepartmentAutomation.Application.Contracts.Responses.BriefDtos;
using DepartmentAutomation.Application.Features.InformationBlocks.Commands.CreateInformationBlockContent;
using DepartmentAutomation.Application.Features.InformationBlocks.Commands.DeleteInformationBlockContent;
using DepartmentAutomation.Application.Features.InformationBlocks.Commands.UpdateInformationBlockContent;
using DepartmentAutomation.Application.Features.InformationBlocks.Queries.GetAdditionalBlocksNameByProgramId;
using DepartmentAutomation.Application.Features.InformationBlocks.Queries.GetBlockByNumber;
using DepartmentAutomation.Application.Features.InformationBlocks.Queries.GetEditableBlocksByProgramId;
using DepartmentAutomation.Application.Features.InformationBlocks.Queries.GetLastBlocksByProgramId;
using DepartmentAutomation.Application.Features.InformationBlocks.Queries.GetMainBlocksByProgramId;
using DepartmentAutomation.Application.Features.InformationBlocks.Queries.GetNotChoosenBlocksByProgramId;
using DepartmentAutomation.Application.Features.InformationBlocks.Queries.GetTemplatesByBlockId;
using DepartmentAutomation.Domain.Enums;
using DepartmentAutomation.Web.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DepartmentAutomation.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [AuthorizeRoles(Role.Teacher)]
    public class InformationBlockController : ApiControllerBase
    {
        [HttpGet(ApiRoutes.InformationBlock.GetMainBlocksByProgramId)]
        public async Task<ActionResult<List<InformationBlockDto>>> GetMainBlocksByProgramIdAsync(
            [FromRoute] int educationalProgramId)
        {
            return await Mediator.Send(new GetMainBlocksByProgramIdQuery { EducationalProgramId = educationalProgramId });
        }
        
        [HttpGet(ApiRoutes.InformationBlock.GetBlockByNumber)]
        public async Task<ActionResult<InformationBlockDto>> GetBlockByNumberAsync(
            [FromQuery] GetBlockByNumberQuery query)
        {
            return await Mediator.Send(query);
        }
        
        [HttpGet(ApiRoutes.InformationBlock.GetAdditionalBlocksNameByProgramId)]
        public async Task<ActionResult<List<InformationBlockName>>> GetAdditionalBlocksNameByProgramIdAsync(
            [FromRoute] int educationalProgramId)
        {
            return await Mediator
                .Send(new GetAdditionalBlocksNameByProgramIdQuery{EducationalProgramId = educationalProgramId});
        }

        [HttpGet(ApiRoutes.InformationBlock.GetTemplatesByInformationBlockId)]
        public async Task<ActionResult<List<string>>> GetTemplatesByInformationBlockIdAsync(
            [FromRoute] int informationBlockId)
        {
            return await Mediator.Send(new GetTemplatesByBlockIdQuery { InformationBlockId = informationBlockId });
        }

        [HttpPut(ApiRoutes.InformationBlock.Base)]
        public async Task<ActionResult> UpdateContentAsync([FromBody] UpdateInformationBlockContentCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPost(ApiRoutes.InformationBlock.Base)]
        public async Task<ActionResult> CreateContentAsync([FromBody] CreateInformationBlockContentCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete(ApiRoutes.InformationBlock.Base)]
        public async Task<ActionResult> DeleteContentAsync([FromQuery] DeleteInformationBlockContentCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpGet(ApiRoutes.InformationBlock.GetEditableBlocksByProgramId)]
        public async Task<ActionResult<List<InformationBlockDto>>> GetEditableBlocksByProgramIdAsync(
            [FromRoute] int educationalProgramId)
        {
            return await Mediator.Send(new GetEditableBlocksByProgramIdQuery { EducationalProgramId = educationalProgramId });
        }

        [HttpGet(ApiRoutes.InformationBlock.GetLastBlocksByProgramId)]
        public async Task<ActionResult<List<InformationBlockDto>>> GetLastBlocksByProgramIdAsync(
            [FromRoute] int educationalProgramId)
        {
            return await Mediator.Send(new GetLastBlocksByProgramIdQuery { EducationalProgramId = educationalProgramId });
        }

        [HttpGet(ApiRoutes.InformationBlock.GetNotChoosenBlocksByProgramId)]
        public async Task<ActionResult<List<InformationBlockWithoutContentDto>>> GetNotChoosenBlocksByProgramIdAsync(
            [FromRoute] int educationalProgramId)
        {
            return await Mediator.Send(new GetNotChoosenBlocksByProgramIdQuery { EducationalProgramId = educationalProgramId });
        }
    }
}