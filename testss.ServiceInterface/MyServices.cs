using ServiceStack;
using System.Collections.Generic;
using testss.ServiceModel;

namespace testss.ServiceInterface;

public class MyServices : Service
{
    public object Any(MyListRequest request)
    {
        var l = new List<string>();

        foreach (var item in request.Entries)
        {
            l.Add(item.Prop1 + item.Prop2);
        }

        return new MyListResponse { Result = l };
    }
}