using Infrastructure.Persistence.Mongo.Abstractions;
using Infrastructure.Persistence.Mongo.Configurations;
using Infrastructure.Persistence.Mongo.Entities;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Mongo.Repositories;

internal class TemplatesV2Repository : MongoDbRepositoryBase<TemplateV2>, ITemplatesV2Repository
{
    public TemplatesV2Repository(IOptions<MongoDbOptions> settings) : base(settings)
    {
    }
}