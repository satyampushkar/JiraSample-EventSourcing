using JiraSample.Auth.Entities;

namespace JiraSample.Auth.Application.Common.Contracts.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
