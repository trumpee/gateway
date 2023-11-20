namespace Infrastructure.Persistence.Mongo.Entities;

public record Template : MongoBaseEntity
{
    public required string? Name { get; set; }
    public required string? Description { get; set; }

    public TemplateContent? Content { get; set; }

    public string[]? ExcludedChannels { get; set; }

    public DateTimeOffset? CreationTimestamp { get; set; }
    public DateTimeOffset? LastModifiedTimestamp { get; set; }
}