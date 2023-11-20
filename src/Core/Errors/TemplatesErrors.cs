using Core.Models.Templates;
using ErrorOr;

namespace Core.Errors;

internal static class TemplatesErrors
{
    internal static ErrorOr<TemplateDtoV2> NameDuplication
        => Error.Conflict("Template.NameDuplication", "Template with same name already exists");
}