using JiraSample.Auth.Application.Commands.Register;
using JiraSample.Auth.Application.Queries.Login;
using JiraSample.Auth.Dto;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace JiraSample.Auth.Controllers;

[Route("api/auth")]
[ApiController]
[EnableCors("AllowWebAppAuthRequests")]
public class AuthenticationController : ControllerBase
{
    private readonly ISender _sender;

    public AuthenticationController(IMediator mediator)
    {
        _sender = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var authResult = await _sender.Send(new RegisterCommand(request.Name,
                                                                request.Email,
                                                                request.Password));

        return CreatedAtAction(
            nameof(Register), 
            new AuthenticationResponse(authResult.User.Name,
                                       authResult.User.Email,
                                       authResult.Token));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var authResult = await _sender.Send(new LoginQuery(request.Email, request.Password));

        return AcceptedAtAction(
            nameof(Login), 
            new AuthenticationResponse(authResult.User.Name,
                                       authResult.User.Email,
                                       authResult.Token));
    }
}
