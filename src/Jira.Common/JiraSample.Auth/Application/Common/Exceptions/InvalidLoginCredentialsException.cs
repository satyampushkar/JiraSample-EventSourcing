namespace JiraSample.Auth.Application.Common.Exceptions;

public class InvalidLoginCredentialsException : Exception
{
    public InvalidLoginCredentialsException(string? message) : base(message)
    {
    }
}
