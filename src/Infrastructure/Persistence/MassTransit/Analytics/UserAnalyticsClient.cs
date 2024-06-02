using MassTransit;
using Trumpee.MassTransit;
using Trumpee.MassTransit.Messages.Analytics.Users;
using Trumpee.MassTransit.Messages.Analytics.Users.Payloads;

namespace Infrastructure.Persistence.MassTransit.Analytics;

public class UserAnalyticsClient(ISendEndpointProvider sendEndpointProvider) : IUserAnalyticsClient
{
    private readonly ISendEndpointProvider _endpointProvider = sendEndpointProvider;

    private static readonly string SignUpQueue = QueueNames.Analytics.Users(typeof(UserSignUpPayload));
    private static readonly string SignIpQueue = QueueNames.Analytics.Users(typeof(UserSignInPayload));
    private static readonly string UserActionQueue = QueueNames.Analytics.Users(typeof(UserActionPayload));

    public async Task SendUserSignIn(string email, CancellationToken ct)
    {
        var analyticsEvent = User.SignIn("Trumpee Gateway", email);
        await SendEvent(analyticsEvent, SignIpQueue, ct);
    }

    public async Task SendUserSignUp(string userId,
        string email, string name, string roles, CancellationToken ct)
    {
        var analyticsEvent = User.SignUp("Trumpee Gateway", userId, email, name, roles);
        await SendEvent(analyticsEvent, SignUpQueue, ct);
    }

    public async Task SendUserAction(string email,
        string userAction, string usedRole, string affectedData, CancellationToken ct)
    {
        var analyticsEvent = User.Action("Trumpee Gateway", email, userAction, usedRole, affectedData);
        await SendEvent(analyticsEvent, UserActionQueue, ct);
    }

    private async Task SendEvent<T>(T analyticsEvent, string queueName, CancellationToken ct) where T : class
    {
        var endpoint = await _endpointProvider.GetSendEndpoint(new Uri(queueName));
        await endpoint.Send(analyticsEvent, ct);
    }
}