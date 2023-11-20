using System.Linq.Expressions;
using Infrastructure.Persistence.Mongo.Entities;

namespace Infrastructure.Persistence.Mongo.Specifications.Concrete.Template;

public class ByNameSpec : Specification<TemplateV2>
{
    private readonly string _name;

    public ByNameSpec(string name)
    {
        _name = name;
    }

    public override Expression<Func<TemplateV2, bool>> ToExpression()
        => t => t.Name.Equals(_name);
}