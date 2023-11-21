using Api.Mappers.Templates;
using Api.Models.Requests.Template;
using Api.Models.Responses;
using Core.Abstractions;
using FastEndpoints;

namespace Api.Endpoints.Templates;

internal sealed class CreateTemplateEndpoint :
    Endpoint<CreateTemplateRequest, ApiResponse<TemplateResponse>, CreateTemplatesMapper>
{
    private readonly ITemplatesService _templatesService;

    public CreateTemplateEndpoint(ITemplatesService templatesService)
    {
        _templatesService = templatesService;
    }

    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("api/templates");
    }

    public override async Task HandleAsync(CreateTemplateRequest req, CancellationToken ct)
    {
        if (ValidationFailed)
        {
            var details = new ProblemDetails(ValidationFailures);
            var response = ApiResponse<TemplateResponse>.Fail(details);
            await SendAsync(response, StatusCodes.Status400BadRequest, ct);
            return;
        }

        var dto = Map.ToEntity(req).Value;

        var errorOrTemplateDto = await _templatesService.CreateTemplate(dto, ct);
        var apiResponse = Map.FromEntity(errorOrTemplateDto);
        await SendAsync(apiResponse, cancellation: ct);
    }
}
