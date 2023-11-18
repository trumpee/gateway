using System.Text.RegularExpressions;
using FastEndpoints;
using FluentValidation;

namespace Api.Endpoints.Templates.Validators;

internal class UpdateTemplateValidator : Validator<UpdateTemplateRequest>
{
    private static Regex SubstitutionKeyRegex
        => new(@"\${{[a-zA-Z0-9._]*}}", RegexOptions.Compiled);

    public UpdateTemplateValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .Length(3, 64);

        RuleFor(x => x.TextTemplate)
            .NotEmpty();

        When(x => !string.IsNullOrEmpty(x.TextTemplate) &&
                  SubstitutionKeyRegex.Matches(x.TextTemplate).Any(),
            () =>
            {
                RuleFor(x => x.DataChunksDescription)
                    .NotEmpty()
                    .WithMessage(x =>
                        $"{nameof(x.DataChunksDescription)} must be non empty when substitutions exists in the template.")
                    .Custom(DataChunksDescriptionMatchedWithTemplate);
            }
        );

    }

    private void DataChunksDescriptionMatchedWithTemplate(
        Dictionary<string, string>? dataChunksDescription,
        FluentValidation.ValidationContext<UpdateTemplateRequest> context)
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