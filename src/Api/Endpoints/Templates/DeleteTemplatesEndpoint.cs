using Api.Models.Requests;
using Api.Models.Responses;
using FastEndpoints;

namespace Api.Endpoints.Templates;

internal sealed class DeleteTemplatesEndpoint : Endpoint<DeleteTemplatesRequest>
{
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

        await SendAsync(ApiResponse.Success(), cancellation: ct);
    }
}