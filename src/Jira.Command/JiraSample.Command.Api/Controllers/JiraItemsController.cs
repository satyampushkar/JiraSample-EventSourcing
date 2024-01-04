using JiraSample.Application.Commands.CreateJiraItem;
using JiraSample.Application.Commands.UpdateJiraItem;
using JiraSample.Command.Application.Commands.PatchJiraItem;
using JiraSample.Common.Contracts;
using JiraSample.Common.Contracts.Requests;
using JiraSample.Common.Contracts.Responses;
using JiraSample.Common.Enums;
using JiraSample.Domain.JiraItem.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace JiraSample.Api.Controllers
{
    [Route("api/jira")]
    [ApiController]
    public class JiraItemsController : ControllerBase
    {
        private readonly ISender _sender;

        public JiraItemsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("item")]
        public async Task<IActionResult> AddJiraItem(CreateJiraItemRequest request)
        {
            var result = await _sender.Send(new CreateJiraItemCommand(request.Name,
                                                                      request.Description,
                                                                      JiraItemType.FromName(request.ItemType),
                                                                      request.Author,
                                                                      request.Asignee,
                                                                      request.ParentId));

            CreateJiraItemResponse createJiraItemResponse = new(result.Id.ToString(), result.Name, result.Description);

            return Created(nameof(AddJiraItem), createJiraItemResponse);
        }

        [HttpPut("item/{jiraId}")]
        public async Task<IActionResult> UpdateJiraItem(string jiraId, UpdateJiraItemRequest request)
        {
            await _sender.Send(new UpdateJiraItemCommand(Guid.Parse(jiraId),
                                                         request.Name,
                                                         request.Description,
                                                         JiraItemType.FromName(request.ItemType),
                                                         JiraItemStatus.FromName(request.ItemStatus),
                                                         request.Asignee,
                                                         request.ParentId));

            return NoContent();
        }

        [HttpPatch("item/{jiraId}")]
        public async Task<IActionResult> PatchJiraItem(string jiraId, JsonPatchDocument request)
        {
            await _sender.Send(new PatchJiraItemCommand(Guid.Parse(jiraId), request));

            return NoContent();
        }
    }
}
