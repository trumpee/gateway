using Core.Models.Common;
using Core.Models.Notifications;
using Trumpee.MassTransit.Messages.Notifications;
using Priority = Trumpee.MassTransit.Messages.Notifications.Priority;

namespace Core.Mappers.External;

internal static class DeliveryRequestMapper
{
    internal static Notification ToRequest(NotificationDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto.Content);

        return new Notification
        {
            Content = ContentNapper.ToContent(dto.Content),
            Priority = (Priority?)dto.Priority,
            Status = dto.Status,
            Timestamp = dto.DeliveryTimestamp,

            // TODO: add mapping
            // UserId = 
            // Channel = dto.
        };
    }
}

internal static class ContentNapper
{
    internal static Content ToContent(ContentDto dto)
    {
        var variables = new Dictionary<string, Variable>();
        if (dto.Variables is { Count: > 0 })
        {
            variables = dto.Variables.Values
                .Select(VariableMapper.ToVariable)
                .ToDictionary(x => x.Name, x => x);
        }

        return new Content
        {
            Subject = dto.Subject!,
            Body = dto.Body!,
            Variables = variables
        };
    }
}

internal static class VariableMapper
{
    internal static Variable ToVariable(VariableDescriptorDto dto)
    {
        return new Variable
        {
            Name = dto.Name!,
            Description = dto.Description,
            Example = dto.Example

            // TODO: add missed mapping
            // Value = "",
            // ValueType = ""
        };
    }
}