using Core.Models.Templates;
using Infrastructure.Persistence.Mongo.Entities;
using MongoDB.Bson;

namespace Core.Mappers;

internal static class TemplateMapper
{
    internal static Template ToEntity(TemplateDto dto)
    {
        // TODO: Is it possible to avoid using ObjectID here? The end goal is to avoid using Mongo specific staff at the Core project.
        ObjectId? id = !string.IsNullOrEmpty(dto.Id)
            ? ObjectId.Parse(dto.Id)
            : null;

        if (id is null)
        {
            return new Template
            {
                Name = dto.Name,
                TextTemplate = dto.TextTemplate,
                DataChunksDescription = dto.DataChunksDescription
            };
        }

        return new Template
        {
            Id = id.Value,
            Name = dto.Name,
            TextTemplate = dto.TextTemplate,
            DataChunksDescription = dto.DataChunksDescription
        };

    }

    internal static TemplateDto ToDto(Template ent) =>
        new()
        {
            Id = ent.Id.ToString()!,
            Name = ent.Name,
            TextTemplate = ent.TextTemplate,
            DataChunksDescription = ent.DataChunksDescription
        };
}