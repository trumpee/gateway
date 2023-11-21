﻿using Api.Models.Requests;
using Api.Models.Requests.Template;
using FastEndpoints;
using FluentValidation;

namespace Api.Endpoints.Templates.Validators;

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
                .SetValidator(new TemplateContentValidator()!, "Template Content Validator");
        });
    }
}