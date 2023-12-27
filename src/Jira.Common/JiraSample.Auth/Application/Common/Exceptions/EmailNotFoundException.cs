namespace JiraSample.Auth.Application.Common.Exceptions;

public class EmailNotFoundException : Exception
{
    public EmailNotFoundException(string? message) : base(message)
    {
    }
}
