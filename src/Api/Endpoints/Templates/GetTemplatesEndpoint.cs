using Api.Models.Requests;
using Api.Models.Responses;
using FastEndpoints;

namespace Api.Endpoints.Templates;

internal sealed class GetTemplatesEndpoint :
    Endpoint<GetTemplatesRequest, ApiResponse<TemplateResponse[]>>
{
    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("api/templates");
    }

    public override async Task HandleAsync(GetTemplatesRequest req, CancellationToken ct)
    {
        if (ValidationFailed)
        {
            var details = new ProblemDetails(ValidationFailures);
            var response = ApiResponse<TemplateResponse[]>.Fail(details);
            await SendAsync(response, StatusCodes.Status400BadRequest, ct);
            return;
        }

        var empty = Array.Empty<TemplateResponse>();
        var selectedTemplates = ApiResponse<TemplateResponse[]>.Success(empty);
        await SendAsync(selectedTemplates, cancellation: ct);
    }
}