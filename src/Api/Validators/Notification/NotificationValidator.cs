using Api.Models.Requests.Notification;
using Api.Validators.Common;
using FastEndpoints;
using FluentValidation;

namespace Api.Validators.Notification;

internal class Constants
{
    internal const int MongoIdLength = 24;
}

internal class NotificationValidator : Validator<CreateNotificationRequest>
{
    private const int SendingWindowMinutes = 10;

    public NotificationValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Priority)
            .IsInEnum();

        RuleFor(x => x.DeliveryTimestamp)
            .Must(ts => ts is null || ts.Value.AddMinutes(SendingWindowMinutes) > DateTime.UtcNow)
            .WithMessage("Delivery timestamp must be 10 minutes or more in the future or null.");

        RuleFor(x => x.TemplateId)
            .NotEmpty()
            .Length(Constants.MongoIdLength)
            .When(x => x.Content is null)
            .WithMessage("Notification content can be passed instead on templateID but not with it " +
                         "because then it's not possible to understand what exactly should be used as a template");

        RuleFor(x => x.Content)
            .SetValidator(new ContentValidator()!)
            .When(x => string.IsNullOrEmpty(x.TemplateId))
            .WithMessage("TemplateID can be passed instead on notification content but not with it " +
                         "because then it's not possible to understand what exactly should be used as a template");

        RuleForEach(x => x.Recipients)
            .NotEmpty()
            .SetValidator(new RecipientValidator(), "Recipients Validation");
    }
}

internal class RecipientValidator : AbstractValidator<RecipientRequest>
{
    public RecipientValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .Length(Constants.MongoIdLength);

        RuleFor(x => x.Channel)
            .NotEmpty();
    }
}