using JiraSample.Auth.Entities;

namespace JiraSample.Auth.Application.Common;

public record AuthenticationResult(
    User User,
    string Token
);