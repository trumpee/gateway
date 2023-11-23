﻿using Core.Models.Common;
using Infrastructure.Persistence.Mongo.Entities.Common;

namespace Core.Mappers;

internal static class VariableDescriptorMapper
{
    internal static VariableDescriptorDto ToDto(VariableDescriptor e)
    {
        return new VariableDescriptorDto
        {
            Name = e.Name,
            Description = e.Description,
            Example = e.Example
        };
    }

    internal static VariableDescriptor ToEntity(VariableDescriptorDto dto)
    {
        return new VariableDescriptor
        {
            Name = dto.Name,
            Description = dto.Description,
            Example = dto.Example
        };
    }
}