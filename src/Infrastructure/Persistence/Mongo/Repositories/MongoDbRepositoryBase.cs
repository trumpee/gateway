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
    protected readonly IMongoCollection<T> Collection;

    protected MongoDbRepositoryBase(
        IOptions<MongoDbOptions> settings)
    {
        var settingValue = settings.Value;
        var database = new MongoClient(settingValue.ConnectionString).GetDatabase(settingValue.DatabaseName);
        Collection = database.GetCollection<T>(typeof(T).Name);
    }

    public ObjectId GetObjectId() => ObjectId.GenerateNewId();

    public async Task<IAsyncEnumerable<T>> FilterBy(Specification<T> spec, int page, int pageSize)
    {
        var pageIndex = page - 1;
        var findOp = Collection.Find(spec.ToExpression() ?? FilterDefinition<T>.Empty)
            .Limit(pageSize)
            .Skip(pageIndex * pageSize);

        return (await findOp.ToCursorAsync(CancellationToken.None)).ToAsyncEnumerable();
    }

    public Task<T?> FirstOrDefault(string id)
        => Collection.Find(x => x.Id.Equals(id)).FirstOrDefaultAsync()!;

    public Task<T?> FirstOrDefault(Specification<T> spec)
        => Collection.Find(spec.ToExpression()).FirstOrDefaultAsync()!;

    public Task InsertOne(T document)
        => Collection.InsertOneAsync(document);


    public Task InsertMany(IEnumerable<T> documents)
        => Collection.InsertManyAsync(documents);

    public Task DeleteOne(string id)
        => Collection.FindOneAndDeleteAsync(x => x.Id.Equals(id));

    public Task DeleteBySpec(Specification<T> spec)
        => Collection.DeleteManyAsync(spec.ToExpression());

    public Task Replace(T entity) =>
        Collection.ReplaceOneAsync(
            e => e.Id.Equals(entity.Id),
            entity, cancellationToken: CancellationToken.None);
}