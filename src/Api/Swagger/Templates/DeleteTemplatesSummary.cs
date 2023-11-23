using Api.Endpoints.Templates;
using Api.Models.Requests.Template;
using Api.Models.Responses;
using FastEndpoints;

namespace Api.Swagger.Templates;

internal sealed class DeleteTemplatesSummary : Summary<DeleteTemplatesEndpoint>
{
    private static ApiResponse Successful =>
        ApiResponse.Success();

    private static ApiResponse Failed =>
        ApiResponse.Fail(new ProblemDetails());

    public DeleteTemplatesSummary()
    {
        Summary = "Delete a bunch of templates";
        Description = "Using one of the accepted query headers delete one or several templates";
        ExampleRequest = new DeleteTemplatesRequest
        {
            Ids = Array.Empty<string>(),
            Names = Array.Empty<string>()
        };

        Response(StatusCodes.Status200OK,
            "Templates deleted",
            example: Successful);

        Response(StatusCodes.Status400BadRequest,
            "Using one of the accepted query headers delete one or several template",
            example: Failed);
    }
}