using Api.Extensions;
using Api.Models.Requests;
using Api.Models.Requests.Template;
using Api.Models.Responses;
using Core.Models.Templates;
using ErrorOr;
using FastEndpoints;

namespace Api.Mappers.Templates;

internal class UpdateTemplateMapper :
    Mapper<UpdateTemplateRequest, ApiResponse<TemplateResponse>, ErrorOr<TemplateDto>>
{
    public override ErrorOr<TemplateDto> ToEntity(UpdateTemplateRequest r)
    {
        return TemplateMapper.ToDto(r);
    }

    public override ApiResponse<TemplateResponse> FromEntity(ErrorOr<TemplateDto> e)
    {
        return e.ToApiResponse(TemplateMapper.ToResponse);
    }
}