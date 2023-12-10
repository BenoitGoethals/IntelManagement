using FluentValidation;
using IntelVault.ApplicationCore.Model;

namespace IntelVault.ApplicationCore.validation;

public class CybIntValidator : AbstractValidator<CybInt>
{
    public CybIntValidator()
    {
       // RuleFor(x =>x.ImpactAssessment).NotEmpty();
        RuleFor(x => x.IncidentDescription).NotEmpty();
      //  RuleFor(x => x.Timeline).Must(c=>c.Count>1);
       // RuleFor(x => x.IncidentType).NotEmpty();
      
      
    }
}