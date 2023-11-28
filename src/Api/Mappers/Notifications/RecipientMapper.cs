using Api.Models.Requests.Notification;
using Core.Models.Notifications;

namespace Api.Mappers.Notifications;

internal static class RecipientMapper
{
    internal static RecipientDto ToDto(RecipientRequest r) =>
        new()
        {
            UserId = r.UserId,
            Channel = r.Channel
        };
}