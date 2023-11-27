namespace Infrastructure.Persistence.MassTransit;

public interface IMassTransitClient<TMessage>
{
    Task SendMessages(List<TMessage> messages, string queueName);
}