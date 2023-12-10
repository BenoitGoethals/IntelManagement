using FluentValidation;
using IntelVault.ApplicationCore.Model;

namespace IntelVault.ApplicationCore.validation;

public class HumIntValidator : AbstractValidator<HumInt>
{
    public HumIntValidator()
    {
        RuleFor(x => x.ContactName).NotEmpty();
        RuleFor(x => x.AccessLevel).NotEmpty();
        RuleFor(x => x.AccuracyOfDetails).NotEmpty();
        RuleFor(x => x.AssessmentAndAnalysis).NotEmpty();
        RuleFor(x => x.ClassificationHandlingInstructions).NotEmpty();
        RuleFor(x => x.ContactTitle).NotEmpty();
        RuleFor(x => x.ReliabilityCredibility).NotEmpty();
        RuleFor(x => x.ContactPhoneNumber).NotEmpty();
        RuleFor(x => x.OperationalStatus).NotEmpty();
        RuleFor(x => x.ContactEmail).EmailAddress();
        RuleFor(x => x.HumIntType).NotNull();
    }
}