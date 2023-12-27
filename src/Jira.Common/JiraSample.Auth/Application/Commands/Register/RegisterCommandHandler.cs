using JiraSample.Auth.Application.Common;
using JiraSample.Auth.Application.Common.Contracts.Authentication;
using JiraSample.Auth.Application.Common.Contracts.Persistance;
using JiraSample.Auth.Application.Common.Exceptions;
using JiraSample.Auth.Entities;
using MediatR;

namespace JiraSample.Auth.Application.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthenticationResult>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }



    public async Task<AuthenticationResult> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        if (_userRepository.GetUserByEmail(command.Email) is not null)
        {
            throw new DuplicateEmailException("User with given email already exists.");
        }

        var newUser = new User
        {
            Name = command.Name,
            Email = command.Email,
            Password = command.Password
        };

        _userRepository.Add(newUser);

        var token = _jwtTokenGenerator.GenerateToken(newUser);
        return new AuthenticationResult(newUser, token);
    }
}