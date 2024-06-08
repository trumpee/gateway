using Api.Mappers.Notifications;
using Api.Models.Requests.Notification;
using Api.Models.Responses;
using Core.Abstractions;
using FastEndpoints;

namespace Api.Endpoints.Notifications;

internal sealed class CreateNotificationEndpoint(INotificationsService notificationsService) :
    Endpoint<CreateNotificationRequest, ApiResponse<NotificationResponse>, CreateNotificationMapper>
{
    private readonly INotificationsService _notificationsService = notificationsService;
    
    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("api/notification");
    }

    public override async Task HandleAsync(CreateNotificationRequest req, CancellationToken ct)
    {
        if (ValidationFailed)
        {
            var details = new ProblemDetails(ValidationFailures);
            var response = ApiResponse<NotificationResponse>.Fail(details);
            await SendAsync(response, StatusCodes.Status400BadRequest, ct);
            return;
        }

        var dto = Map.ToEntity(req).Value;

        var errorOrNotificationDto = await _notificationsService.CreateNotification(dto, ct);
        var apiResponse = Map.FromEntity(errorOrNotificationDto);
        await SendAsync(apiResponse, cancellation: ct);
    }
}