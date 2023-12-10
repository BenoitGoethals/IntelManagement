using FluentValidation;
using IntelVault.ApplicationCore.Model;

namespace IntelVault.ApplicationCore.validation;

public class PersonOfInterestValidator : AbstractValidator<PersonOfInterest>
{
    public PersonOfInterestValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
      
        RuleFor(x => x.PoliticalGroup).NotEmpty();
        RuleFor(x => x.Address).NotEmpty();


    }
}