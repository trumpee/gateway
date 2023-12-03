using Core.Models.Common;
using Trumpee.MassTransit.Messages.Notifications;

namespace Core.Mappers.External;

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