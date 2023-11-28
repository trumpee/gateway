using Api.Endpoints.Templates;
using Api.Models.Requests.Template;
using Api.Models.Responses;
using FastEndpoints;

namespace Api.Swagger.Templates;

internal sealed class UpdateTemplateSummary : Summary<UpdateTemplateEndpoint, UpdateTemplateRequest>
{
    private const string TemplateName = "Updated template title";

    private const string TemplateText =
        "This is an updated message template with a substitution of the timestamp. ${{timestamp}} ";

    private static ApiResponse<TemplateResponse> Successful =>
        ApiResponse<TemplateResponse>.Success(
            new TemplateResponse
            {
                Id = Guid.NewGuid().ToString("N"),
                Name = TemplateName,
                Description = TemplateText
            });

    private static ApiResponse<TemplateResponse> Failed =>
        ApiResponse<TemplateResponse>.Fail(
            new ProblemDetails());

    public UpdateTemplateSummary()
    {
        Summary = "Patch existing template";
        Description = "Patches existed template or returns an error. " +
                      "Check the ErrorCodes section in the response body for more info.";
        ExampleRequest = new UpdateTemplateRequest
        {
            Name = TemplateName,
            Description = ""
        };

        Response(
            StatusCodes.Status200OK,
            "Template updated",
            example: Successful);

        Response(
            StatusCodes.Status400BadRequest,
            "Template request validation failed. Check the details section in the response body for more info",
            example: Failed);
    }
}