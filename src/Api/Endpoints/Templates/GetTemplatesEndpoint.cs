using Api.Extensions;
using Api.Mappers.Templates;
using Api.Models.Requests;
using Api.Models.Responses;
using Core.Abstractions;
using Core.Models.Templates;
using ErrorOr;
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

        var templatesStream = _templatesService.GetTemplates(Map.ToEntity(req), ct);
        await SendEventStreamAsync("templates", MapChunk(templatesStream), ct);
    }

    private IAsyncEnumerable<ApiResponse<TemplateResponse>> MapChunk(
        IAsyncEnumerable<ErrorOr<TemplateDto>> templates)
    {
        return templates.Select(r => r.ToApiResponse(ToTemplateResponse));
    }

    private TemplateResponse ToTemplateResponse(TemplateDto dto)
    {
        TemplateContentResponse? content = null;
        if (dto.Content is not null)
        {
            var variables = new Dictionary<string, VariableDescriptorResponse>();
            if (dto.Content.Variables is not null)
            {
                variables = dto.Content.Variables
                    .Select(v => new VariableDescriptorResponse
                    {
                        Name = v.Value.Name,
                        Example = v.Value.Example,
                        Description = v.Value.Description,
                    }).ToDictionary(descriptor => descriptor.Name!);
            }

            content = new TemplateContentResponse()
            {
                Subject = dto.Content.Subject,
                Body = dto.Content.Body,
                Variables = variables
            };
        }

        return new TemplateResponse
        {
            Id = dto.Id!,
            Name = dto.Name!,
            Description = dto.Description!,

            Content = content,

            CreationTimestamp = dto.CreationTimestamp.GetValueOrDefault(),
            LastModifiedTimestamp = dto.LastModifiedTimestamp.GetValueOrDefault()
        };
    }
}