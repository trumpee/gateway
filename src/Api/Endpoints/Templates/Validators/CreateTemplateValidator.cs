using System.Text.RegularExpressions;
using Api.Models.Requests;
using FastEndpoints;
using FluentValidation;

namespace Api.Endpoints.Templates.Validators;

internal class CreateTemplateValidator : Validator<CreateTemplateRequest>
{
    private static Regex SubstitutionKeyRegex
        => new(@"\${{[a-zA-Z0-9._]*}}", RegexOptions.Compiled);

    public CreateTemplateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(64);

        RuleFor(x => x.TextTemplate)
            .NotEmpty();

        When(x => SubstitutionKeyRegex.Matches(x.TextTemplate).Any(),
            () =>
            {
                RuleFor(x => x.DataChunksDescription)
                    .Custom(DataChunksDescriptionMatchedWithTemplate);
            }
        );
    }

    private void DataChunksDescriptionMatchedWithTemplate(
        Dictionary<string, string>? dataChunksDescription,
        FluentValidation.ValidationContext<CreateTemplateRequest> context)
    {
        var substitutions = SubstitutionKeyRegex.Matches(context.InstanceToValidate.TextTemplate);

        if (dataChunksDescription?.Any() is null or false)
        {
            context.AddFailure("Substitutions descriptions are null or empty");
            return;
        }

        if (dataChunksDescription.Count != substitutions.Count)
        {
            context.AddFailure("Chunks with description is not equals to the chunks that have to be substituted");
            return;
        }

        foreach (var substitutionKey in substitutions.Select(substitution => substitution.Value[3..^2]))
            if (!dataChunksDescription.ContainsKey(substitutionKey))
                context.AddFailure($"Description for substitution with key {substitutionKey} missed");
    }
}