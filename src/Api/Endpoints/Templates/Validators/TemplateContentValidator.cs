using Api.Models.Requests;
using Api.Models.Requests.Template;
using FluentValidation;

namespace Api.Endpoints.Templates.Validators;

internal class TemplateContentValidator :
    AbstractValidator<TemplateContentRequest>
{
    public TemplateContentValidator()
    {
        RuleFor(x => x.Subject)
            .MaximumLength(255);

        RuleFor(x => x.Body)
            .MaximumLength(10000);

        RuleForEach(x => x.Variables)
            .SetValidator(new VariableDescriptorValidator(), "Variables Validation");
    }
}