using JiraSample.Auth.Application.Common;
using MediatR;

namespace JiraSample.Auth.Application.Commands.Register;

public record RegisterCommand(
    string Name,
    string Email,
    string Password) : IRequest<AuthenticationResult>;
