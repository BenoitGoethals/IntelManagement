﻿using FluentValidation;
using IntelVault.ApplicationCore.Model;
using IntelVault.Infrastructure.Workers;
using Quartz;
using System.Text.RegularExpressions;

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
        RuleFor(x => x.EndDate).NotEmpty().GreaterThanOrEqualTo(DateTime.Today);
        RuleFor(x => x.CronTab).Must(IsValidSchedule).WithMessage("Not valid");


    }

    public static bool IsValidSchedule(string? schedule)
    {

        var valid = schedule != null && CronExpression.IsValidExpression(schedule);
        // Some expressions are parsed as valid by the above method but they are not valid, like "* * * ? * *&54".
        //In order to avoid such invalid expressions an additional check is required, that is done using the below regex.

        var regex = @"^\s*($|#|\w+\s*=|(\?|\*|(?:[0-5]?\d)(?:(?:-|\/|\,)(?:[0-5]?\d))?(?:,(?:[0-5]?\d)(?:(?:-|\/|\,)(?:[0-5]?\d))?)*)\s+(\?|\*|(?:[0-5]?\d)(?:(?:-|\/|\,)(?:[0-5]?\d))?(?:,(?:[0-5]?\d)(?:(?:-|\/|\,)(?:[0-5]?\d))?)*)\s+(\?|\*|(?:[01]?\d|2[0-3])(?:(?:-|\/|\,)(?:[01]?\d|2[0-3]))?(?:,(?:[01]?\d|2[0-3])(?:(?:-|\/|\,)(?:[01]?\d|2[0-3]))?)*)\s+(\?|\*|(?:0?[1-9]|[12]\d|3[01])(?:(?:-|\/|\,)(?:0?[1-9]|[12]\d|3[01]))?(?:,(?:0?[1-9]|[12]\d|3[01])(?:(?:-|\/|\,)(?:0?[1-9]|[12]\d|3[01]))?)*)\s+(\?|\*|(?:[1-9]|1[012])(?:(?:-|\/|\,)(?:[1-9]|1[012]))?(?:L|W)?(?:,(?:[1-9]|1[012])(?:(?:-|\/|\,)(?:[1-9]|1[012]))?(?:L|W)?)*|\?|\*|(?:JAN|FEB|MAR|APR|MAY|JUN|JUL|AUG|SEP|OCT|NOV|DEC)(?:(?:-)(?:JAN|FEB|MAR|APR|MAY|JUN|JUL|AUG|SEP|OCT|NOV|DEC))?(?:,(?:JAN|FEB|MAR|APR|MAY|JUN|JUL|AUG|SEP|OCT|NOV|DEC)(?:(?:-)(?:JAN|FEB|MAR|APR|MAY|JUN|JUL|AUG|SEP|OCT|NOV|DEC))?)*)\s+(\?|\*|(?:[0-6])(?:(?:-|\/|\,|#)(?:[0-6]))?(?:L)?(?:,(?:[0-6])(?:(?:-|\/|\,|#)(?:[0-6]))?(?:L)?)*|\?|\*|(?:MON|TUE|WED|THU|FRI|SAT|SUN)(?:(?:-)(?:MON|TUE|WED|THU|FRI|SAT|SUN))?(?:,(?:MON|TUE|WED|THU|FRI|SAT|SUN)(?:(?:-)(?:MON|TUE|WED|THU|FRI|SAT|SUN))?)*)(|\s)+(\?|\*|(?:|\d{4})(?:(?:-|\/|\,)(?:|\d{4}))?(?:,(?:|\d{4})(?:(?:-|\/|\,)(?:|\d{4}))?)*))$";

        return schedule != null && valid && Regex.IsMatch(schedule, regex);
    }
}