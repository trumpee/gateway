using Infrastructure.Persistence.Mongo.Entities.Common;

namespace Infrastructure.Persistence.Mongo.Entities.Template;

public record Template : MongoBaseEntity
{
    public required string? Name { get; init; }
    public required string? Description { get; init; }

    public Content? Content { get; init; }

    public string[]? ExcludedChannels { get; init; }

    public DateTimeOffset? CreationTimestamp { get; init; }
    public DateTimeOffset? LastModifiedTimestamp { get; init; }
}