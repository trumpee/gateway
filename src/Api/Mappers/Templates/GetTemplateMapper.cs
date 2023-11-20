using Api.Models.Requests;
using Api.Models.Responses;
using Core.Models.Templates;
using FastEndpoints;

namespace Api.Mappers.Templates;

internal class GetTemplateMapper :
    Mapper<GetTemplatesRequest, ApiResponse<TemplateResponse>, TemplatesFilterDto>
{
    public override TemplatesFilterDto ToEntity(GetTemplatesRequest r)
    {
        return new TemplatesFilterDto
        {
            All = r.All,
            Ids = r.Ids,
            Names = r.Names,
            Page = r.Page,
            PageSize = r.PageSize
        };
    }
}