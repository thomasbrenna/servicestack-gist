using ServiceStack;

using ServiceStack.FluentValidation;
using System.Collections.Generic;

namespace testss.ServiceModel;

[Route("/list")]
public class MyListRequest : IGet, IReturn<MyListResponse>
{
    [ValidateNotEmpty]
    public required List<ListEntry> Entries { get; set; }
}

public class MyListResponse
{
    public required List<string> Result { get; set; }

    public ResponseStatus ResponseStatus { get; set; }
}

