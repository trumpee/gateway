using Api.Models.Requests.Common;
using Api.Models.Responses;
using Core.Models.Common;

namespace Api.Mappers.Common;

internal static class VariableDescriptorMapper
{
    internal static VariableDescriptorDto ToDto(VariableDescriptorRequest e)
    {
        return new VariableDescriptorDto
        {
            Name = e.Name,
            Description = e.Description,
            Example = e.Example
        };
    }

    internal static VariableDescriptorResponse ToResponse(VariableDescriptorDto dto)
    {
        return new VariableDescriptorResponse
        {
            Name = dto.Name,
            Description = dto.Description,
            Example = dto.Example
        };
    }
}