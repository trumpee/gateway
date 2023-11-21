namespace Api.Models.Requests.Common;

internal class VariableDescriptorRequest
{
    public required string? Name { get; init; }
    public string? Example { get; init; }
    public string? Description { get; init; }
}