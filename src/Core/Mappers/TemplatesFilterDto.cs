using Core.Models.Templates;
using Infrastructure.Persistence.Mongo.Entities;
using Infrastructure.Persistence.Mongo.Specifications;
using Infrastructure.Persistence.Mongo.Specifications.Concrete.Template;

namespace Core.Mappers;

internal static class TemplatesFilterMapper
{
    internal static Specification<TemplateV2> MapToSpec(TemplatesFilterDto dto)
    {
        var byNamesSpec = GetFilteringByNamesSpec(dto);
        var byIdsSpec = GetFilteringSpecByIds(dto);

        if (byNamesSpec != null && byIdsSpec != null)
        {
            return byNamesSpec & byIdsSpec;
        }

        if (byNamesSpec != null)
        {
            return byNamesSpec;
        }

        return byIdsSpec;
    }

    private static Specification<TemplateV2>? GetFilteringByNamesSpec(TemplatesFilterDto dto)
        => dto.Names?.Any() is true
            ? TemplateSpecs.ByNames(dto.Names)
            : null;

    private static Specification<TemplateV2>? GetFilteringSpecByIds(TemplatesFilterDto dto)
        => dto.Ids?.Any() is true
            ? TemplateSpecs.ByIds(dto.Ids)
            : null;
}