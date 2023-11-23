using Infrastructure.Persistence.Mongo.Entities.Notifications;

namespace Infrastructure.Persistence.Mongo.Abstractions;

public interface INotificationsRepository : IMongoRepository<Notification>
{
}