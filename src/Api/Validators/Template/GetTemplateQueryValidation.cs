using Api.Models.Requests.Template;
using FastEndpoints;
using FluentValidation;

namespace Api.Validators.Template;

internal class GetTemplateQueryValidation : Validator<GetTemplatesRequest>
{
    public GetTemplateQueryValidation()
    {
        RuleFor(x => x)
            .Must(x => (x.Ids is not null && x.Ids.Any(id => !string.IsNullOrEmpty(id))) ||
                       (x.Names is not null && x.Names.Any(name => !string.IsNullOrEmpty(name))) ||
                       x.All is true)
            .WithMessage("At least one searching query parameter must be passed.");
    }
}