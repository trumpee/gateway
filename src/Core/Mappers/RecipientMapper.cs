using Core.Models.Notifications;
using Infrastructure.Persistence.Mongo.Entities.Notifications;

namespace Core.Mappers;

internal static class RecipientMapper
{
    internal static RecipientDto ToDto(Recipient e) =>
        new()
        {
            UserId = e.UserId,
            Channel = e.Channel
        };

    internal static Recipient ToEntity(RecipientDto r) =>
        new()
        {
            UserId = r.UserId,
            Channel = r.Channel
        };
}