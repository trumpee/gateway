using Api.Models.Requests.Common;
using FluentValidation;

namespace Api.Validators.Common;

internal class VariableDescriptorValidator :
    AbstractValidator<KeyValuePair<string, VariableDescriptorRequest>>
{
    public VariableDescriptorValidator()
    {
        RuleFor(kvp => kvp)
            .Custom(NamesEquals);

        RuleFor(x => x.Value.Name)
            .MaximumLength(255);

        RuleFor(x => x.Value.Description)
            .MaximumLength(500);

        RuleFor(x => x.Value.Example)
            .MaximumLength(2048);
    }

    private void NamesEquals(
        KeyValuePair<string, VariableDescriptorRequest> value,
        FluentValidation.ValidationContext<KeyValuePair<string, VariableDescriptorRequest>> validationContext)
    {
        if (value.Key.Equals(value.Value.Name))
        {
            validationContext.AddFailure(
                "kvp.KeyNEqVariableName",
                "Variable name is not equals to KVP key");
        }
    }
}