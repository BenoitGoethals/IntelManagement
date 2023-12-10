using FluentValidation;
using IntelVault.ApplicationCore.Model;

namespace IntelVault.ApplicationCore.validation;

public class IntelDocumentValidator : AbstractValidator<IntelDocument>
{
    public IntelDocumentValidator()
    {
        RuleFor(x => x.Description).NotEmpty();
    }
}