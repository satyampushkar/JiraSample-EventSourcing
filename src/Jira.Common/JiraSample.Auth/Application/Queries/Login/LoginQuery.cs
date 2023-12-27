using JiraSample.Auth.Application.Common;
using MediatR;

namespace JiraSample.Auth.Application.Queries.Login;

public record LoginQuery(
    string Email,
    string Password) : IRequest<AuthenticationResult>;
