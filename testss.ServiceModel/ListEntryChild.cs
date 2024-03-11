using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testss.ServiceModel;

public class ListEntryChild
{
    [ValidateNotEmpty]
    public required string ChildProp { get; set; }
}
