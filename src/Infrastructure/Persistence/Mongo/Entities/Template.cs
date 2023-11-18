namespace Infrastructure.Persistence.Mongo.Entities;

public record Template : MongoBaseEntity
{
    public required string Name { get; set; }
    public required string TextTemplate { get; set; }
    public Dictionary<string, string>? DataChunksDescription { get; set; }
}