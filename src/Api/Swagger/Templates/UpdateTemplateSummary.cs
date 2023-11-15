using Api.Endpoints.Templates;
using Api.Models.Responses;
using FastEndpoints;

namespace Api.Swagger.Templates;

internal class UpdateTemplateSummary : Summary<UpdateTemplateEndpoint, UpdateTemplateRequest>
{
    private const string TemplateName = "Updated template title";

    private const string TemplateText =
        "This is an updated message template with a substitution of the timestamp. ${{timestamp}} ";

    private static readonly Dictionary<string, string> TemplateSubstitutions = new()
    {
        { "timestamp", "Message delivery request creation timestamp" }
    };

    private static ApiResponse<TemplateResponse> Successful =>
        ApiResponse<TemplateResponse>.Success(
            new TemplateResponse
            {
                Id = Guid.NewGuid().ToString("N"),
                Name = TemplateName,
                TextTemplate = TemplateText,
                DataChunksDescription = TemplateSubstitutions
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
            TextTemplate = TemplateText,
            DataChunksDescription = TemplateSubstitutions
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