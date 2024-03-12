using ServiceStack;

using ServiceStack.FluentValidation;
using ServiceStack.FluentValidation.Results;
using System.Collections.Generic;

namespace testss.ServiceModel;

[Route("/list")]
public class MyListRequest : IPost, IReturn<MyListResponse>
{
    public required List<ListEntry> Entries { get; set; }
}

public class MyListResponse
{
    public required List<string> Result { get; set; }

    public ResponseStatus ResponseStatus { get; set; }
}

public class MyListRequestValidator : AbstractValidator<MyListRequest>
{
    public MyListRequestValidator()
    {
        RuleFor(mlr => mlr).NotNull().WithMessage("Empty request");
        RuleFor(mlr => mlr.Entries).NotNull().WithMessage("Expected property 'Entries' not found");

        When(mlr => mlr.Entries != null, () =>
        {
            RuleFor(mlr => mlr.Entries.Count).Must(c => c >= 1).WithMessage("MyListRequest must contain at least 1 entry");
        });

        RuleForEach(mlr => mlr.Entries).SetValidator(new ListEntryValidator());
    }
}
