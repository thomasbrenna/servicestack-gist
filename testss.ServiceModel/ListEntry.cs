using ServiceStack;
using ServiceStack.FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testss.ServiceModel;

public class ListEntry
{
    public required string Prop1 { get; set; }

    public required int Prop2 { get; set; }

    public required ListEntryChild Child { get; set; }
}

public class ListEntryValidator : AbstractValidator<ListEntry>
{
    public ListEntryValidator()
    {
        RuleFor(h => h.Prop1).NotEmpty().WithMessage("Prop1 cannot be empty!");

        RuleFor(h => h.Prop2).Must(t => t >= 10).WithMessage("Prop2 must be at least 10!");

        RuleFor(h => h.Child).NotEmpty().WithMessage("Child cannot be empty!");
    }
}
