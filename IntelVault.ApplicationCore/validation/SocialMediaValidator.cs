using FluentValidation;
using IntelVault.ApplicationCore.Model;

namespace IntelVault.ApplicationCore.validation;

public class SocialMediaValidator : AbstractValidator<SocialMedia>
{
    public SocialMediaValidator()
    {
      //  RuleFor(x => x.AccountCreationDate).LessThan(DateTime.Now);
        RuleFor(x => x.Bio).NotEmpty();
        RuleFor(x => x.DisplayName).NotEmpty();
        RuleFor(x => x.Platform).NotEmpty();
        RuleFor(x => x.Username).NotEmpty();
    }
}