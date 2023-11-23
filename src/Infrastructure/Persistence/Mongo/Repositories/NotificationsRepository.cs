using Infrastructure.Persistence.Mongo.Abstractions;
using Infrastructure.Persistence.Mongo.Configurations;
using Infrastructure.Persistence.Mongo.Entities.Notifications;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Mongo.Repositories;

internal class NotificationsRepository :
    MongoDbRepositoryBase<Notification>, INotificationsRepository
{
    public NotificationsRepository(IOptions<MongoDbOptions> settings) : base(settings)
    {
    }
}