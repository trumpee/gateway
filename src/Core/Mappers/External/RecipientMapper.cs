using Core.Models.Notifications;
using Trumpee.MassTransit.Messages.Notifications;

namespace Core.Mappers.External;

internal static class RecipientMapper
{
    internal static Recipient ToRequest(RecipientDto d) =>
        new()
        {
            UserId = d.UserId!,
            Channel = d.Channel!
        };
}