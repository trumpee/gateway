using Api.Mappers.Templates;
using Api.Models.Requests.Template;
using Api.Models.Responses;
using Core.Abstractions;
using FastEndpoints;

namespace Api.Endpoints.Templates;

internal sealed class GetTemplatesEndpoint :
    Endpoint<GetTemplatesRequest, ApiResponse<TemplateResponse[]>, GetTemplateMapper>
{
    private readonly ITemplatesService _templatesService;

    public GetTemplatesEndpoint(ITemplatesService templatesService)
    {
        _templatesService = templatesService;
    }

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

        var templates = (await _templatesService.GetTemplates(Map.ToEntity(req), ct))
            .Where(t => !t.IsError)
            .Select(e => TemplateMapper.ToResponse(e.Value))
            .ToArray();

        var apiResponse = ApiResponse<TemplateResponse[]>.Success(templates);
        await SendAsync(apiResponse, cancellation: ct);
    }
}