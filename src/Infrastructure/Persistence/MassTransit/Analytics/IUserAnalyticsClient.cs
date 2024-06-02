namespace Infrastructure.Persistence.MassTransit.Analytics;

public interface IUserAnalyticsClient
{
    Task SendUserSignIn(string email, CancellationToken ct);
    Task SendUserSignUp(string userId, string email, string name, string roles, CancellationToken ct);
    Task SendUserAction(string email, string userAction, string usedRole, string affectedData, CancellationToken ct);
}