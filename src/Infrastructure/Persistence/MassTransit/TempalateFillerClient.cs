using MassTransit;
using Trumpee.MassTransit;
using Trumpee.MassTransit.Clients;
using Trumpee.MassTransit.Messages.Notifications;

namespace Infrastructure.Persistence.MassTransit;

public class TemplateFillerClient :
    MassTransitClient<Notification>, ITemplateFillerClient
{
    public TemplateFillerClient(ISendEndpointProvider endpointProvider) :
        base(endpointProvider)
    {
    }

    public new Task SendMessages(
        IEnumerable<Notification> messages,
        string _)
    {
        return base.SendMessages(messages, QueueNames.TemplateFillerQueueName);
    }
}