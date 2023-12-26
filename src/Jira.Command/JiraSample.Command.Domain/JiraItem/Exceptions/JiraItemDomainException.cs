namespace JiraSample.Command.Domain.JiraItem.Exceptions;

public class JiraItemDomainException : Exception
{
    public JiraItemDomainException()
    { }

    public JiraItemDomainException(string message)
        : base(message)
    { }

    public JiraItemDomainException(string message, Exception innerException)
        : base(message, innerException)
    { }
}