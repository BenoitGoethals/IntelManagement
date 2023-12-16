using FluentValidation;
using IntelVault.ApplicationCore.Model;

namespace IntelVault.ApplicationCore.validation;

public class IntelInvestigationFileValidator : AbstractValidator<IntelInvestigationFile>
{
    public IntelInvestigationFileValidator()
    {
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.CaseNumber).NotEmpty();
        RuleFor(x => x.InvestigationStatus).NotEmpty();
    }
}