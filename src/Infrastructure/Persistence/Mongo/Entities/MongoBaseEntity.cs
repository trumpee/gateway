using MongoDB.Bson;

namespace Infrastructure.Persistence.Mongo.Entities;

public record MongoBaseEntity
{
    public ObjectId Id { get; set; }
}