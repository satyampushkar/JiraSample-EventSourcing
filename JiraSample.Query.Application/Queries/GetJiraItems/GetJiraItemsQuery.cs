using JiraSample.Query.Domain.Entities;
using MediatR;

namespace JiraSample.Query.Application.Queries.GetJiraItems;

public record GetJiraItemsQuery() : IRequest<List<JiraItemEntity>>;