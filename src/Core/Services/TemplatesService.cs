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
    private readonly ITemplatesRepository _templatesRepository;

    public TemplatesService(
        ITemplatesRepository templatesRepository)
    {
        _templatesRepository = templatesRepository;
    }

    public async Task<ErrorOr<TemplateDto>> CreateTemplate(TemplateDto dto, CancellationToken ct)
    {
        var isExists = await _templatesRepository
            .FirstOrDefault(TemplateSpecs.ByName(dto.Name!)) is not null;
        if (isExists)
        {
            return TemplatesErrors.NameDuplication;
        }

        ct.ThrowIfCancellationRequested();

        var template = TemplateMapper.ToEntity(dto);
        await _templatesRepository.InsertOne(template);

        dto = dto with { Id = template.Id.ToString() };
        return dto;
    }

    public Task DeleteTemplates(string[] ids, string[] names, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        var deletionSpec = TemplateSpecs.ByIds(ids) &
                           TemplateSpecs.ByNames(names);

        return _templatesRepository.DeleteBySpec(deletionSpec);
    }

    public async IAsyncEnumerable<ErrorOr<TemplateDto>> GetTemplates(
        TemplatesFilterDto dto, [EnumeratorCancellation] CancellationToken ct)
    {
        var spec = TemplatesFilterMapper.MapToSpec(dto);

        var templates = await _templatesRepository.FilterBy(spec, dto.Page, dto.PageSize);
        await foreach (var template in templates.WithCancellation(ct))
        {
            yield return TemplateMapper.ToDto(template);
        }
    }

    public async Task<ErrorOr<TemplateDto>> UpdateTemplate(TemplateDto dto, CancellationToken ct)
    {
        if (dto.Id is null)
        {
            ArgumentException.ThrowIfNullOrEmpty(dto.Id);
        }

        await _templatesRepository.Replace(TemplateMapper.ToEntity(dto));

        var updatedDocument = await _templatesRepository
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