using Infrastructure.Persistence.Mongo.Entities;

namespace Infrastructure.Persistence.Mongo.Abstractions;

public interface ITemplatesRepository : IMongoRepository<Template>
{
}