using Infrastructure.Persistence.Mongo.Entities.Template;

namespace Infrastructure.Persistence.Mongo.Abstractions;

public interface ITemplatesRepository : IMongoRepository<Template>
{
}