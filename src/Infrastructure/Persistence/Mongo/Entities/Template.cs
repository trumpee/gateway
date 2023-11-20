namespace Infrastructure.Persistence.Mongo.Entities;

public record Template : MongoBaseEntity
{
    public required string Name { get; set; }
    public required string TextTemplate { get; set; }
    public Dictionary<string, string>? DataChunksDescription { get; set; }
}

public record TemplateV2 : MongoBaseEntity
{
    public required string? Name { get; set; }
    public required string? Description { get; set; }

    public TemplateContent? Content { get; set; }

    public string[]? ExcludedChannels { get; set; }

    public DateTimeOffset? CreationTimestamp { get; set; }
    public DateTimeOffset? LastModifiedTimestamp { get; set; }
}

public class TemplateContent
{
    public string? Subject { get; set; }
    public string? Body { get; set; }

    public Dictionary<string, VariableDescriptor>? Variables { get; set; }
}

public class VariableDescriptor
{
    public string? Name { get; set; }
    public string? Example { get; set; }
    public string? Description { get; set; }
}