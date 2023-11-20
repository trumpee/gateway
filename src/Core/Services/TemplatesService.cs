using System.Runtime.CompilerServices;
using Core.Abstractions;
using Core.Errors;
using Core.Mappers;
using Core.Models.Templates;
using ErrorOr;
using Infrastructure.Persistence.Mongo.Abstractions;
using Infrastructure.Persistence.Mongo.Specifications.Concrete.Template;

namespace Core.Services;

internal class TemplatesService : ITemplatesService
{
    private readonly ITemplatesV2Repository _templatesV2Repository;

    public TemplatesService(
        ITemplatesV2Repository templatesV2Repository)
    {
        _templatesV2Repository = templatesV2Repository;
    }

    public async Task<ErrorOr<TemplateDtoV2>> CreateTemplate(TemplateDtoV2 dto, CancellationToken ct)
    {
        var isExists = await _templatesV2Repository
            .FirstOrDefault(TemplateSpecs.ByName(dto.Name!)) is not null;
        if (isExists)
        {
            return TemplatesErrors.NameDuplication;
        }

        ct.ThrowIfCancellationRequested();

        var template = TemplateMapper.ToEntity(dto);
        await _templatesV2Repository.InsertOne(template);

        dto = dto with { Id = template.Id.ToString() };
        return dto;
    }

    public Task DeleteTemplates(string[] ids, string[] names, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        var deletionSpec = TemplateSpecs.ByIds(ids) &
                           TemplateSpecs.ByNames(names);

        return _templatesV2Repository.DeleteBySpec(deletionSpec);
    }

    public async IAsyncEnumerable<ErrorOr<TemplateDtoV2>> GetTemplates(
        TemplatesFilterDto dto, [EnumeratorCancellation] CancellationToken ct)
    {
        var spec = TemplatesFilterMapper.MapToSpec(dto);

        var templates = await _templatesV2Repository.FilterBy(spec, dto.Page, dto.PageSize);
        await foreach (var template in templates.WithCancellation(ct))
        {
            yield return TemplateMapper.ToDto(template);
        }
    }

    public async Task<ErrorOr<TemplateDtoV2>> UpdateTemplate(TemplateDtoV2 dto, CancellationToken ct)
    {
        if (dto.Id is null)
        {
            ArgumentException.ThrowIfNullOrEmpty(dto.Id);
        }

        await _templatesV2Repository.Replace(TemplateMapper.ToEntity(dto));

        var updatedDocument = await _templatesV2Repository
            .FirstOrDefault(TemplateSpecs.ById(dto.Id));
        if (updatedDocument is null)
        {
            return Error.Unexpected(
                "Template.UpdateFailed",
                $"Template with ID {dto.Id} was not updated");
        }

        return TemplateMapper.ToDto(updatedDocument);
    }
}