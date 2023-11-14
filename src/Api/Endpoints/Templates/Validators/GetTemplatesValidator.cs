using Api.Models.Requests;
using FastEndpoints;
using FluentValidation;

namespace Api.Endpoints.Templates.Validators;

public class GetTemplateQueryValidation : Validator<GetTemplatesRequest>
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