using Core.Models.Common;
using Trumpee.MassTransit.Messages.Notifications;

namespace Core.Mappers.External;

internal static class VariableMapper
{
    internal static Variable ToVariable(VariableDescriptorDto dto)
    {
        return new Variable
        {
            Name = dto.Name!,
            Description = dto.Description,
            Example = dto.Example,
            Value = dto.Value,
            ValueType = dto.ValueType
        };
    }
}