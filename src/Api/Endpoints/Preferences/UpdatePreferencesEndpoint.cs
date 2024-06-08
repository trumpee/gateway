using Api.Mappers.UserPreferences;
using Api.Models.Requests.UserPreferences;
using Api.Models.Responses;
using Core.Abstractions;
using FastEndpoints;

namespace Api.Endpoints.Preferences;

public class UpdatePreferencesEndpoint(IUserPreferencesService userPreferencesService) :
    Endpoint<UpdateUserPreferencesRequest, ApiResponse, UpdateUserPreferencesMapper>
{
    private readonly IUserPreferencesService _preferencesService = userPreferencesService;

    public override void Configure()
    {
        Verbs(Http.PUT);
        Routes("api/preferences");
    }
    
    public override async Task HandleAsync(UpdateUserPreferencesRequest req, CancellationToken ct)
    {
        var dto = Map.ToEntity(req).Value;
        var result = await _preferencesService.UpdateUserPreferences(dto, ct);

        var apiResponse = Map.FromEntity(result);
        await SendAsync(apiResponse, cancellation: ct);
    }
}