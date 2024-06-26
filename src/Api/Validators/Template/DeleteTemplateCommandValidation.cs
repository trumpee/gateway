﻿using Api.Models.Requests.Template;
using FastEndpoints;
using FluentValidation;

namespace Api.Validators.Template;

internal class DeleteTemplateCommandValidation : Validator<DeleteTemplatesRequest>
{
    public DeleteTemplateCommandValidation()
    {
        RuleFor(x => x)
            .Must(x => (x.Ids is not null && x.Ids.Any(id => !string.IsNullOrEmpty(id))) ||
                       (x.Names is not null && x.Names.Any(name => !string.IsNullOrEmpty(name))))
            .WithMessage("At least one searching criteria must be passed.");
    }
}