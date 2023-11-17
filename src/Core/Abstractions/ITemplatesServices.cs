using Core.Models.Templates;
using ErrorOr;

namespace Core.Abstractions;

public interface ITemplatesService
{
    Task<ErrorOr<TemplateDto>> CreateTemplate(TemplateDto dto, CancellationToken ct);
    Task DeleteTemplate(string id, CancellationToken ct);
    IAsyncEnumerable<ErrorOr<TemplateDto>> GetTemplates(TemplatesFilterDto dto, CancellationToken ct);
}