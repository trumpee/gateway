using Api.Models.Requests.Common;
using FluentValidation;

namespace Api.Validators.Common;

internal class ContentValidator :
    AbstractValidator<ContentRequest>
{
    public ContentValidator()
    {
        RuleFor(x => x.Subject)
            .MaximumLength(255);

        RuleFor(x => x.Body)
            .MaximumLength(10000);

        RuleForEach(x => x.Variables)
            .SetValidator(new VariableDescriptorValidator(), "Variables Validation");
    }
}