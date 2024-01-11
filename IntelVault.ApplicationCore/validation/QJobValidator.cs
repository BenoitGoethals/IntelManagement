using FluentValidation;
using IntelVault.ApplicationCore.Model;
using IntelVault.Infrastructure.Workers;

namespace IntelVault.ApplicationCore.validation;

public class QJobValidator : AbstractValidator<QJobs>
{
    public QJobValidator()
    {
        RuleFor(x => x.Name).NotEmpty();

        RuleFor(x => x.Description).NotEmpty().MinimumLength(5);
        RuleFor(x => x.Group).NotEmpty().MinimumLength(3);
        RuleFor(x => x.Interval).NotEmpty().GreaterThan(1);
        RuleFor(x => x.Url).NotEmpty().MinimumLength(5);
        RuleFor(x => x.StartDate).NotEmpty().GreaterThanOrEqualTo(DateTime.Today);


    }
}