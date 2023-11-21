using Api.Models.Requests.Template;
using Api.Validators.Common;
using FastEndpoints;
using FluentValidation;

namespace Api.Validators.Template;

internal class UpdateTemplateValidator : Validator<UpdateTemplateRequest>
{
    public UpdateTemplateValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(64);

        RuleFor(x => x.Description)
            .MaximumLength(500)
            .NotEmpty();

        When(x => x.Content is not null, () =>
        {
            RuleFor(x => x.Content)
                .NotEmpty()
                .SetValidator(new ContentValidator()!, "Template Content Validator");
        });
    }
}