namespace Core.Models.Common;

public class VariableDescriptorDto
{
    public string? Name { get; init; }
    public string? Example { get; init; }
    public string? Description { get; init; }
    public object? Value { get; init; }
    public string? ValueType { get; init; }
}