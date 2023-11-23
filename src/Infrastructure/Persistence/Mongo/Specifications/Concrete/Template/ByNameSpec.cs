using System.Linq.Expressions;

namespace Infrastructure.Persistence.Mongo.Specifications.Concrete.Template;

public class ByNameSpec : Specification<Entities.Template.Template>
{
    private readonly string _name;

    public ByNameSpec(string name)
    {
        _name = name;
    }

    public override Expression<Func<Entities.Template.Template, bool>> ToExpression()
        => t => t.Name.Equals(_name);
}