using System.Linq.Expressions;
using MongoDB.Bson;

namespace Infrastructure.Persistence.Mongo.Specifications.Concrete.Template;

public class ByIdSpec : Specification<Entities.Template.Template>
{
    private readonly string _id;

    public ByIdSpec(string id)
    {
        _id = id;
    }

    public override Expression<Func<Entities.Template.Template, bool>> ToExpression()
        => t => t.Id.Equals(ObjectId.Parse(_id));
}