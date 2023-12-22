using JiraSample.Query.Domain.Entities;
using MediatR;

namespace JiraSample.Query.Application.Queries.GetJiraItem;

public record GetJiraItemQuery(Guid Id) : IRequest<JiraItemEntity>;
