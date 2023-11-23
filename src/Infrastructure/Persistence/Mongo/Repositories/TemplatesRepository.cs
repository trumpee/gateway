using Infrastructure.Persistence.Mongo.Abstractions;
using Infrastructure.Persistence.Mongo.Configurations;
using Infrastructure.Persistence.Mongo.Entities.Template;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Mongo.Repositories;

internal class TemplatesRepository : MongoDbRepositoryBase<Template>, ITemplatesRepository
{
    public TemplatesRepository(IOptions<MongoDbOptions> settings) : base(settings)
    {
    }
}