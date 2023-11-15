using Api.Models.Responses;
using FastEndpoints;

namespace Api.Endpoints.Templates;

internal sealed class UpdateTemplateEndpoint : Endpoint<UpdateTemplateRequest, ApiResponse<TemplateResponse>>
{
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

        var templateResponse = new TemplateResponse
        {
            Id = Guid.NewGuid().ToString("N"),
            Name = req.Name ?? "old name",
            TextTemplate = req.TextTemplate ?? "old template",
            DataChunksDescription = req.DataChunksDescription
        };

        var successResponse = ApiResponse<TemplateResponse>.Success(templateResponse);
        await SendAsync(successResponse, cancellation: ct);
    }
}