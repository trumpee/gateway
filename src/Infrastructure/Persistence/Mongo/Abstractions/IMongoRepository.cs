using Infrastructure.Persistence.Mongo.Specifications;
using MongoDB.Bson;

namespace Infrastructure.Persistence.Mongo.Abstractions;

public interface IMongoRepository<TEntity>
{
    ObjectId GetObjectId();

    Task<IAsyncEnumerable<TEntity>> FilterBy(Specification<TEntity> spec);
    Task<TEntity?> FirstOrDefault(Specification<TEntity> spec);

    Task InsertOne(TEntity document);
    Task InsertMany(IEnumerable<TEntity> documents);

    Task DeleteOne(ObjectId id);
    Task DeleteBySpec(Specification<TEntity> spec);

    Task<ObjectId> Replace(TEntity entity);
}