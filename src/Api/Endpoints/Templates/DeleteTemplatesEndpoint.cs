using Api.Models.Requests.Template;
using Api.Models.Responses;
using Core.Abstractions;
using FastEndpoints;

namespace Api.Endpoints.Templates;

internal sealed class DeleteTemplatesEndpoint : Endpoint<DeleteTemplatesRequest>
{
    private readonly ITemplatesService _templatesService;

    public DeleteTemplatesEndpoint(ITemplatesService templatesService)
    {
        _templatesService = templatesService;
    }

    public override void Configure()
    {
        Verbs(Http.DELETE);
        Routes("api/templates");
    }

    public override async Task HandleAsync(DeleteTemplatesRequest req, CancellationToken ct)
    {   
        if (ValidationFailed)
        {
            var details = new ProblemDetails(ValidationFailures);
            var response = ApiResponse.Fail(details);
            await SendAsync(response, StatusCodes.Status400BadRequest, ct);
            return;
        }

        await _templatesService.DeleteTemplates(
            req.Ids ?? Array.Empty<string>(),
            req.Names ?? Array.Empty<string>(),
            ct);

        await SendAsync(ApiResponse.Success(), cancellation: ct);
    }
}