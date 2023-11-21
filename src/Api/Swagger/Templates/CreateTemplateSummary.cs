using Api.Endpoints.Templates;
using Api.Models.Requests.Common;
using Api.Models.Requests.Template;
using Api.Models.Responses;
using FastEndpoints;

namespace Api.Swagger.Templates;

internal sealed class CreateTemplateSummary : Summary<CreateTemplateEndpoint, CreateTemplateRequest>
{
    private const string TemplateName = "Test notification template";

    private const string TemplateText = "Describe template here. Desciption can contains up to 500 symbols";

    private static ApiResponse<TemplateResponse> Successful =>
        ApiResponse<TemplateResponse>.Success(
            new TemplateResponse
            {
                Id = Guid.NewGuid().ToString("N"),
                Name = TemplateName,
                Description = TemplateText,
                Content = null,
                CreationTimestamp = DateTimeOffset.UtcNow,
                LastModifiedTimestamp = DateTimeOffset.UtcNow
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
            Description = TemplateText,
            ExcludedChannels = new[] { "sms", "pagerduty" },
            Content = new TemplateContentRequest
            {
                Subject = "Activate Your Account",
                Body =
                    "<h1>Welcome to Our Service, ${{userName}}!</h1><p>Your account has been successfully created. Please click the link below to activate your account:</p><p><a href='${{activationLink}}'>Activate Account</a></p><p>If you did not request this account, no further action is required.</p>",
                Variables = new Dictionary<string, VariableDescriptorRequest>
                {
                    {
                        "userName", new VariableDescriptorRequest
                        {
                            Name = "userName",
                            Description = "The name of the user",
                            Example = "John Doe"
                        }
                    }
                }
            }
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