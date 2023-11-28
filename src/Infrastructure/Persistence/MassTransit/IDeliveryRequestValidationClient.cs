using Trumpee.MassTransit.Clients.Abstractions;
using Trumpee.MassTransit.Messages.Notifications;

namespace Infrastructure.Persistence.MassTransit;

public interface IDeliveryRequestValidationClient : IMassTransitClient<Notification>
{
}