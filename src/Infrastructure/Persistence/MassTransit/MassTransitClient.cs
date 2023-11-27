using MassTransit;

namespace Infrastructure.Persistence.MassTransit;

public abstract class MassTransitClient<TMessage>(ISendEndpointProvider endpointProvider) :
    IMassTransitClient<TMessage> where TMessage : class
{
    public async Task SendMessages(List<TMessage> messages, string queueName)
    {
        var endpoint = await endpointProvider.GetSendEndpoint(new Uri(queueName));
        await endpoint.SendBatch(messages);
    }
}