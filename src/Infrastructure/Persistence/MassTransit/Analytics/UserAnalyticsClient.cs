using MassTransit;
using Trumpee.MassTransit;
using Trumpee.MassTransit.Messages.Analytics.Users;

namespace Infrastructure.Persistence.MassTransit.Analytics;

public class UserAnalyticsClient(ISendEndpointProvider sendEndpointProvider) : IUserAnalyticsClient
{
    private readonly ISendEndpointProvider _endpointProvider = sendEndpointProvider;
    
    public async Task SendUserSignIn(string email, CancellationToken ct)
    {
        var analyticsEvent = User.SignIn("Trumpee Gateway", email);
        await SendEvent(analyticsEvent, ct);
    }
    
    public async Task SendUserSignUp(string userId,
        string email, string name, string roles, CancellationToken ct)
    {
        var analyticsEvent = User.SignUp("Trumpee Gateway", userId, email, name, roles);
        await SendEvent(analyticsEvent, ct);
    }
    
    public async Task SendUserAction(string email,
        string userAction, string usedRole, string affectedData, CancellationToken ct)
    {
        var analyticsEvent = User.Action("Trumpee Gateway", email, userAction, usedRole, affectedData);
        await SendEvent(analyticsEvent, ct);
    }
    
    private async Task SendEvent<T>(T analyticsEvent, CancellationToken ct) where T : class
    {
        var endpoint = await _endpointProvider.GetSendEndpoint(new Uri(QueueNames.Analytics.Users));
        await endpoint.Send(analyticsEvent, ct);
    }
}