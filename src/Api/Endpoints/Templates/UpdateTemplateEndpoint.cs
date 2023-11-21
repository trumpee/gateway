using Api.Mappers.Templates;
using Api.Models.Requests;
using Api.Models.Requests.Template;
using Api.Models.Responses;
using Core.Abstractions;
using FastEndpoints;

namespace Api.Endpoints.Templates;

internal sealed class UpdateTemplateEndpoint :
    Endpoint<UpdateTemplateRequest, ApiResponse<TemplateResponse>, UpdateTemplateMapper>
{
    private readonly ITemplatesService _templatesService;

    public UpdateTemplateEndpoint(ITemplatesService templatesService)
    {
        _templatesService = templatesService;
    }

    public override void Configure()
    {
        Verbs(Http.PUT);
        Routes("api/templates");
    }

    public override async Task HandleAsync(UpdateTemplateRequest req, CancellationToken ct)
    {
        if (ValidationFailed)
        {
            var details = new ProblemDetails(ValidationFailures);
            var response = ApiResponse<TemplateResponse>.Fail(details);
            await SendAsync(response, StatusCodes.Status400BadRequest, ct);
            return;
        }

        var errorOrUpdated = await _templatesService
            .UpdateTemplate(Map.ToEntity(req).Value, ct);
        var apiResponse = Map.FromEntity(errorOrUpdated);

        await SendAsync(apiResponse, cancellation: ct);
    }
}