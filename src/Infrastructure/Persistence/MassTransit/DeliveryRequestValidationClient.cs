using MassTransit;
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
}