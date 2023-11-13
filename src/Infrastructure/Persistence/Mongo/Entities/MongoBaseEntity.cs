using MongoDB.Bson;

namespace Infrastructure.Persistence.Mongo.Entities;

internal record MongoBaseEntity
{
    public ObjectId Id { get; set; }
}