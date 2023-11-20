using Infrastructure.Persistence.Mongo.Entities;

namespace Infrastructure.Persistence.Mongo.Abstractions;

public interface ITemplatesV2Repository : IMongoRepository<TemplateV2>
{
}