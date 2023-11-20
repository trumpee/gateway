﻿using System.Linq.Expressions;
using Infrastructure.Persistence.Mongo.Entities;
using MongoDB.Bson;

namespace Infrastructure.Persistence.Mongo.Specifications.Concrete.Template;

public class ByIdSpec : Specification<Entities.Template>
{
    private readonly string _id;

    public ByIdSpec(string id)
    {
        _id = id;
    }

    public override Expression<Func<Entities.Template, bool>> ToExpression()
        => t => t.Id.Equals(ObjectId.Parse(_id));
}