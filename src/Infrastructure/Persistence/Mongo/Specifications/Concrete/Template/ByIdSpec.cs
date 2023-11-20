using System.Linq.Expressions;
using Infrastructure.Persistence.Mongo.Entities;
using MongoDB.Bson;

namespace Infrastructure.Persistence.Mongo.Specifications.Concrete.Template;

public class ByIdSpec : Specification<TemplateV2>
{
    private readonly string _id;

    public ByIdSpec(string id)
    {
        _id = id;
    }

    public override Expression<Func<TemplateV2, bool>> ToExpression()
        => t => t.Id.Equals(ObjectId.Parse(_id));
}