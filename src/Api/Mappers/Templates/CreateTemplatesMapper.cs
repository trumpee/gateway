using Api.Extensions;
using Api.Models.Requests;
using Api.Models.Responses;
using Core.Models.Templates;
using ErrorOr;
using FastEndpoints;

namespace Api.Mappers.Templates;

internal class CreateTemplatesMapper :
    Mapper<CreateTemplateRequest, ApiResponse<TemplateResponse>, ErrorOr<TemplateDto>>
{
    public override ErrorOr<TemplateDto> ToEntity(CreateTemplateRequest r)
    {
        return new TemplateDto
        {
            Name = r.Name,
            TextTemplate = r.TextTemplate,
            DataChunksDescription = r.DataChunksDescription
        };
    }

    public override ApiResponse<TemplateResponse> FromEntity(ErrorOr<TemplateDto> e)
    {
        return e.ToApiResponse(ToTemplateResponse);

        TemplateResponse ToTemplateResponse(TemplateDto dto)
        {
            return new TemplateResponse
            {
                Id = dto.Id!,
                Name = dto.Name,
                TextTemplate = dto.TextTemplate,
                DataChunksDescription = dto.DataChunksDescription
            };
        }
    }
}