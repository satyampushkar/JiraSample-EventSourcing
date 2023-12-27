using JiraSample.Auth.Application.Common.Contracts.Authentication;
using JiraSample.Auth.Application.Common.Contracts.Persistance;
using JiraSample.Auth.Application.Common;
using JiraSample.Auth.Entities;
using MediatR;
using JiraSample.Auth.Application.Common.Exceptions;

namespace JiraSample.Auth.Application.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthenticationResult>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<AuthenticationResult> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        if (_userRepository.GetUserByEmail(query.Email) is not User user)
        {
            throw new EmailNotFoundException("User with given email does not exist.");
        }

        if (user.Password != query.Password)
        {
            throw new InvalidLoginCredentialsException("Invalid Credentials");
        }

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}
