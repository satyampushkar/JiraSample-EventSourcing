using JiraSample.Common.Contracts.Requests;
using JiraSample.Common.Contracts.Responses;
using JiraSample.Query.Application.Queries.GetJiraItem;
using JiraSample.Query.Application.Queries.GetJiraItemHistory;
using JiraSample.Query.Application.Queries.GetJiraItems;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JiraSample.Query.Api.Controllers
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

        [HttpGet("items")]
        public async Task<IActionResult> GetJiraItems()
        {
            var result = await _sender.Send(new GetJiraItemsQuery());

            GetJiraItemsResponse GetJiraItemsResponse = new(result.Select(
                item => new GetJiraItemResponse(
                    item.Id,
                    item.Name,
                    item.Description,
                    item.ItemType.ToString(),
                    item.ItemStatus.ToString(),
                    item.Asignee,
                    item.Author,
                    item.ParentId.ToString(),
                    item.CreatedDateTime,
                    item.UpdatedDateTime))
                .ToList());

            return Ok(GetJiraItemsResponse);
        }

        [HttpGet("item/{id}")]
        public async Task<IActionResult> GetJiraItem(string id)
        {
            var result = await _sender.Send(new GetJiraItemQuery(Guid.Parse(id)));

            GetJiraItemResponse GetJiraItem = new(
                result.Id,
                result.Name,
                result.Description,
                result.ItemType.ToString(),
                result.ItemStatus.ToString(),
                result.Asignee,
                result.Author,
                result.ParentId.ToString(),
                result.CreatedDateTime,
                result.UpdatedDateTime);

            return Ok(GetJiraItem);
        }

        [HttpGet("item/{id}/history")]
        public async Task<IActionResult> GetJiraItemHistory(string id)
        {
            var result = await _sender.Send(new GetJiraItemHistoryQuery(Guid.Parse(id)));

            GetJiraItemHistoryResponse GetJiraItemHistoryResponse = new(result.Select(
                item => new JiraItemHistoryResponse(
                    item.Id,
                    item.ActionPerformed,
                    item.ChangedValue,
                    item.ActionPerformedAt))
                .ToList());

            return Ok(GetJiraItemHistoryResponse);
        }
    }
}
