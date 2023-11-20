using Core.Models.Templates;
using ErrorOr;

namespace Core.Abstractions;

public interface ITemplatesService
{
    Task<ErrorOr<TemplateDtoV2>> CreateTemplate(TemplateDtoV2 dto, CancellationToken ct);
    Task DeleteTemplates(string[] ids, string[] names, CancellationToken ct);
    IAsyncEnumerable<ErrorOr<TemplateDtoV2>> GetTemplates(TemplatesFilterDto dto, CancellationToken ct);
    Task<ErrorOr<TemplateDtoV2>> UpdateTemplate(TemplateDtoV2 dto, CancellationToken ct);
} 