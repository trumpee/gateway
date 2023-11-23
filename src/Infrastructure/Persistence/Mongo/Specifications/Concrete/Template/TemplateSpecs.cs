namespace Infrastructure.Persistence.Mongo.Specifications.Concrete.Template;

public static class TemplateSpecs
{
    public static Specification<Entities.Template.Template> All => Specification<Entities.Template.Template>.True;
    public static ByNameSpec ByName(string name) => new(name);

    public static Specification<Entities.Template.Template> ByNames(string[] names)
        => names.Map(ByName).CombineOr();

    public static ByIdSpec ById(string id) => new(id);

    public static Specification<Entities.Template.Template> ByIds(string[] ids)
        => ids.Map(ById).CombineOr();
}