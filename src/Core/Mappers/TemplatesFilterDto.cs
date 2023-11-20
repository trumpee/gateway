using Core.Models.Templates;
using Infrastructure.Persistence.Mongo.Entities;
using Infrastructure.Persistence.Mongo.Specifications;
using Infrastructure.Persistence.Mongo.Specifications.Concrete.Template;

namespace Core.Mappers;

internal static class TemplatesFilterMapper
{
    internal static Specification<Template> MapToSpec(TemplatesFilterDto dto)
    {
        if (dto.All is true)
        {
            return TemplateSpecs.All;
        }

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

        return byIdsSpec!;
    }

    private static Specification<Template>? GetFilteringByNamesSpec(TemplatesFilterDto dto)
        => dto.Names?.Any() is true
            ? TemplateSpecs.ByNames(dto.Names)
            : null;

    private static Specification<Template>? GetFilteringSpecByIds(TemplatesFilterDto dto)
        => dto.Ids?.Any() is true
            ? TemplateSpecs.ByIds(dto.Ids)
            : null;
}