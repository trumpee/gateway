using Api.Extensions;
using Api.Models.Requests;
using Api.Models.Responses;
using Core.Models.Templates;
using ErrorOr;
using FastEndpoints;

namespace Api.Mappers.Templates;

internal class UpdateTemplateMapper :
    Mapper<UpdateTemplateRequest, ApiResponse<TemplateResponse>, ErrorOr<TemplateDtoV2>>
{
    public override ErrorOr<TemplateDtoV2> ToEntity(UpdateTemplateRequest r)
    {
        return TemplateMapper.ToDto(r);
    }

    public override ApiResponse<TemplateResponse> FromEntity(ErrorOr<TemplateDtoV2> e)
    {
        return e.ToApiResponse(TemplateMapper.ToResponse);
    }
}