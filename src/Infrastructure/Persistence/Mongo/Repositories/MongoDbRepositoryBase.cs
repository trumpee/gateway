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

    public async Task<IAsyncEnumerable<T>> FilterBy(Specification<T> spec, int page, int pageSize)
    {
        var pageIndex = page - 1;
        var findOp = _collection.Find(spec.ToExpression() ?? FilterDefinition<T>.Empty)
            .Limit(pageSize)
            .Skip(pageIndex * pageSize);

        return (await findOp.ToCursorAsync(CancellationToken.None)).ToAsyncEnumerable();
    }

    public Task<T?> FirstOrDefault(string id)
        => _collection.Find(x => x.Id.Equals(id)).FirstOrDefaultAsync()!;

    public Task<T?> FirstOrDefault(Specification<T> spec)
        => _collection.Find(spec.ToExpression()).FirstOrDefaultAsync()!;

    public Task InsertOne(T document)
        => _collection.InsertOneAsync(document);


    public Task InsertMany(IEnumerable<T> documents)
        => _collection.InsertManyAsync(documents);

    public Task DeleteOne(string id)
        => _collection.FindOneAndDeleteAsync(x => x.Id.Equals(id));

    public Task DeleteBySpec(Specification<T> spec)
        => _collection.DeleteManyAsync(spec.ToExpression());

    public Task Replace(T entity) =>
        _collection.ReplaceOneAsync(
            e => e.Id.Equals(entity.Id),
            entity, cancellationToken: CancellationToken.None);
}