namespace Infrastructure.Persistence.Mongo.Entities.Common;

public class VariableDescriptor
{
    public string? Name { get; init; }
    public string? Example { get; init; }
    public string? Description { get; init; }
    public string? Value { get; init; }
    public string? ValueType { get; init; }
}