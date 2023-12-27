namespace JiraSample.Auth.Dto;

public record LoginRequest(
    string Email,
    string Password
);
