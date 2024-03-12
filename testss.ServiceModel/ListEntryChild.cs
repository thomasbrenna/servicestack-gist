using ServiceStack;
using ServiceStack.FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testss.ServiceModel;

public class ListEntryChild
{
    public required string ChildProp { get; set; }
}

public class ListEntryChildValidator : AbstractValidator<ListEntryChild>
{
    public ListEntryChildValidator()
    {
        RuleFor(lec => lec.ChildProp).NotEmpty().WithMessage("ChildProp cannot be empty!");

        RuleFor(lec => lec.ChildProp).Must(p => p == "MUST BE THIS").WithMessage("ChildProp must be one of: 'MUST BE THIS'");
    }
}
