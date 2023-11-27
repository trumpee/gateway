using MassTransit;

namespace Infrastructure.Persistence.MassTransit;

public class MassTransitClient<TMessage> : IMassTransitClient<TMessage> where TMessage : class
{
    private readonly ISendEndpointProvider _endpointProvider;

    public MassTransitClient(ISendEndpointProvider endpointProvider)
    {
        _endpointProvider = endpointProvider;
    }

    public async Task SendMessages(List<TMessage> messages)
    {
        var endpoint = await _endpointProvider.GetSendEndpoint(new Uri("queue:validation"));
        await endpoint.SendBatch(messages);
    }
}

public interface IMassTransitClient<TMessage>
{
    Task SendMessages(List<TMessage> messages);
}