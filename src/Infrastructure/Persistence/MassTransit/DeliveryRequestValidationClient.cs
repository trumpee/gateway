using MassTransit;
using Trumpee.MassTransit;
using Trumpee.MassTransit.Clients;
using Trumpee.MassTransit.Messages.Notifications;

namespace Infrastructure.Persistence.MassTransit;

public class DeliveryRequestValidationClient :
    MassTransitClient<Notification>, IDeliveryRequestValidationClient
{
    public DeliveryRequestValidationClient(ISendEndpointProvider endpointProvider) :
        base(endpointProvider)
    {
    }

    public new Task SendMessages(
        IEnumerable<Notification> messages,
        string _)
    {
        return base.SendMessages(messages, QueueNames.ValidationQueueName);
    }
}