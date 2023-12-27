namespace JiraSample.Auth.Application.Common.Exceptions;

public class DuplicateEmailException : Exception
{
    public DuplicateEmailException(string? message) : base(message)
    {
    }
}
