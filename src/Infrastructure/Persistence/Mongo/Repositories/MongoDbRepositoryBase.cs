using Infrastructure.Persistence.Mongo.Abstractions;
using Infrastructure.Persistence.Mongo.Configurations;
using Infrastructure.Persistence.Mongo.Specifications;
using Microsoft.Extensions.Options;
using MongoAsyncEnumerableAdapter;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Persistence.Mongo.Repositories;

internal abstract class MongoDbRepositoryBase<T> : IMongoRepository<T>
    where T : Entities.MongoBaseEntity
{
    private readonly IMongoCollection<T> _collection;

    protected MongoDbRepositoryBase(
        IOptions<MongoDbOptions> settings)
    {
        var settingValue = settings.Value;
        var database = new MongoClient(settingValue.ConnectionString).GetDatabase(settingValue.DatabaseName);
        _collection = database.GetCollection<T>(typeof(T).Name);
    }

    public ObjectId GetObjectId() => ObjectId.GenerateNewId();

    public async Task<IAsyncEnumerable<T>> FilterBy(Specification<T> spec)
    {
        var findOp = _collection.Find(spec.ToExpression() ?? FilterDefinition<T>.Empty);
        return (await findOp.ToCursorAsync(CancellationToken.None)).ToAsyncEnumerable();
    }

    public Task<T?> FirstOrDefault(Specification<T> spec)
        => _collection.Find(spec.ToExpression()).FirstOrDefaultAsync()!;

    public Task InsertOne(T document)
        => _collection.InsertOneAsync(document);


    public Task InsertMany(IEnumerable<T> documents)
        => _collection.InsertManyAsync(documents);

    public Task DeleteOne(ObjectId id)
        => _collection.FindOneAndDeleteAsync(x => x.Id.Equals(id));

    public Task DeleteBySpec(Specification<T> spec)
        => _collection.DeleteManyAsync(spec.ToExpression());

    public async Task<ObjectId> Replace(T entity)
        => (await _collection.ReplaceOneAsync(e => e.Id.Equals(entity.Id), entity)).UpsertedId.AsObjectId;
}