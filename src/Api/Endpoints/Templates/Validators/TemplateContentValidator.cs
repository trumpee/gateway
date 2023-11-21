using Api.Models.Requests.Common;
using FluentValidation;

namespace Api.Endpoints.Templates.Validators;

internal class TemplateContentValidator :
    AbstractValidator<ContentRequest>
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