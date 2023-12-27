namespace JiraSample.Auth.Dto;

public record AuthenticationResponse(
    string Name,
    string Email,
    string Token
);