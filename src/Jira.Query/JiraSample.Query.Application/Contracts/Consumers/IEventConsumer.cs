namespace JiraSample.Query.Application.Contracts.Consumers;

public interface IEventConsumer
{
    void Consume(string topic);
}
