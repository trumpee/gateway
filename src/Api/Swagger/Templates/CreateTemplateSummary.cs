using Api.Endpoints.Templates;
using Api.Models.Requests;
using Api.Models.Responses;
using FastEndpoints;

namespace Api.Swagger.Templates;

internal sealed class CreateTemplateSummary : Summary<CreateTemplateEndpoint, CreateTemplateRequest>
{
    private const string TemplateName = "Test notification template";

    private const string TemplateText = "This is a test template. " +
                                        "Here you can add everything what you need to deliver to user. " +
                                        "Also, variables substitution supported. Use ${{variable_name}} to replace it " +
                                        "with a value from delivery context";

    private static readonly Dictionary<string, string> TemplateSubstitutions = new()
    {
        { "variable_name", "Here you can specify substitution variable description" }
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

    public CreateTemplateSummary()
    {
        Summary = "Create new template";
        Description = "Create a new template or returns an error if a template with given name exists or not valid. " +
                      "Check the ErrorCodes section in the response body for more info.";
        ExampleRequest = new CreateTemplateRequest
        {
            Name = TemplateName,
            TextTemplate = TemplateText,
            DataChunksDescription = TemplateSubstitutions
        };

        Response(
            StatusCodes.Status200OK,
            "Template created",
            example: Successful);

        Response(
            StatusCodes.Status400BadRequest,
            "Template request validation failed. Check the details section in the response body for more info",
            example: Failed);

        Response(
            StatusCodes.Status409Conflict,
            "Template with a given name already exists",
            example: Failed);
    }
}