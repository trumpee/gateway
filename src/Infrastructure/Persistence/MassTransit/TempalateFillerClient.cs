using MassTransit;
using Trumpee.MassTransit;
using Trumpee.MassTransit.Clients;
using Trumpee.MassTransit.Messages.Notifications;

namespace Infrastructure.Persistence.MassTransit;

public class TemplateFillerClient(ISendEndpointProvider endpointProvider) :
    MassTransitClient<Notification>(endpointProvider), ITemplateFillerClient
{
    public new Task SendMessages(
        IEnumerable<Notification> messages,
        string _)
    {
        return base.SendMessages(messages, QueueNames.Services.TemplateFillerQueueName);
    }
}