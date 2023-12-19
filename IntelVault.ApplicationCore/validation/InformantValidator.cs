using FluentValidation;
using IntelVault.ApplicationCore.Model;

namespace IntelVault.ApplicationCore.validation;

public class InformantValidator : AbstractValidator<Informant>
{
    public InformantValidator()
    {
        RuleFor(x => x.InformantName).NotEmpty();
        RuleFor(x => x.ReliabilityRating).NotEmpty();
        RuleFor(x => x.Email).EmailAddress();

    }
    
}