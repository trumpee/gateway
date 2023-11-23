using Core.Models.Templates;
using ErrorOr;

namespace Core.Abstractions;

public interface ITemplatesService
{
    Task<ErrorOr<TemplateDto>> CreateTemplate(TemplateDto dto, CancellationToken ct);
    Task DeleteTemplates(string[] ids, string[] names, CancellationToken ct);
    ValueTask<List<ErrorOr<TemplateDto>>> GetTemplates(TemplatesFilterDto dto, CancellationToken ct);
    Task<ErrorOr<TemplateDto>> UpdateTemplate(TemplateDto dto, CancellationToken ct);
} 