namespace JiraSample.Auth.Dto;

public record RegisterRequest(
    string Name,
    string Email,
    string Password
);