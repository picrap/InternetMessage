
using System;
using System.Collections.Generic;
using System.Linq;
using InternetMessage.Message;

namespace InternetMessage.Utility;

public static class EnumerableExtensions
{
    public static IDictionary<string, IList<InternetMessageHeaderField>> ToHeaders(this IEnumerable<InternetMessageHeaderField> headerFields)
    {
        return headerFields.GroupBy(h => h.Name)
            .ToDictionary(g => g.Key, g => (IList<InternetMessageHeaderField>)g.ToList(), StringComparer.InvariantCultureIgnoreCase);
    }
}