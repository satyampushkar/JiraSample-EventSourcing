namespace JiraSample.Infrastructure.Services.Publishers.Exceptions;

public class EventNotPublishedException : Exception
{
    public EventNotPublishedException(string message)
        :base(message)
    {   
    }
}
