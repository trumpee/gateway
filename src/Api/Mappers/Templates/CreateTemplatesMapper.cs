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
        return TemplateMapper.ToDto(r);
    }

    public override ApiResponse<TemplateResponse> FromEntity(ErrorOr<TemplateDto> e)
    {
        return e.ToApiResponse(TemplateMapper.ToResponse);
    }
}