using JiraSample.Query.Application.Contracts.Repositories;
using JiraSample.Query.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JiraSample.Query.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JiraItemsTestController : ControllerBase
    {
        private readonly IJiraItemRepository jiraItemRepository;

        public JiraItemsTestController(IJiraItemRepository jiraItemRepository)
        {
            this.jiraItemRepository = jiraItemRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Postdata(JiraItemEntity jiraItemEntity)
        {
            await jiraItemRepository.CreateAsync(jiraItemEntity);

            return Ok();
        }
    }
}
