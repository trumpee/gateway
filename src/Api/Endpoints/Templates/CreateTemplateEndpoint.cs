using Api.Models.Requests;
using Api.Models.Responses;
using FastEndpoints;

namespace Api.Endpoints.Templates;

internal class CreateTemplateEndpoint : Endpoint<CreateTemplateRequest, ApiResponse<TemplateResponse>>
{
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

        var templateResponse = new TemplateResponse
        {
            Id = Guid.NewGuid().ToString("N"),
            Name = req.Name,
            TextTemplate = req.TextTemplate,
            DataChunksDescription = req.DataChunksDescription
        };

        var successResponse = ApiResponse<TemplateResponse>.Success(templateResponse);
        await SendAsync(successResponse, cancellation: ct);
    }
}