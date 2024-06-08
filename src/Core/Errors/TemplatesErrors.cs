using Core.Models.Templates;
using ErrorOr;

namespace Core.Errors;

internal static class TemplatesErrors
{
    internal static ErrorOr<TemplateDto> NameDuplication
        => Error.Conflict("Template.FailedToCreate", "Template with same name already exists");
}