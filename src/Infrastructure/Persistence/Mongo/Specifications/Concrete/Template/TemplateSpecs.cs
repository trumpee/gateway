using Infrastructure.Persistence.Mongo.Entities;

namespace Infrastructure.Persistence.Mongo.Specifications.Concrete.Template;

public static class TemplateSpecs
{
    public static ByNameSpec ByName(string name) => new(name);

    public static Specification<TemplateV2> ByNames(string[] names)
        => names.Map(ByName).CombineOr();

    public static ByIdSpec ById(string id) => new(id);

    public static Specification<TemplateV2> ByIds(string[] ids)
        => ids.Map(ById).CombineOr();
}