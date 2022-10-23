using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Text.RegularExpressions;

namespace Starter.Domain.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> Filter<T>(this IQueryable<T> queryable, string filter)
    {
        if (queryable == null) { throw new ArgumentNullException("queryable"); }
        if (string.IsNullOrEmpty(filter.Trim())) { throw new ArgumentNullException("filter"); }

        var containsRegex = new Regex(@"contains\((?<Property>(.*)),[ ]?(?<Value>(.*))\)");
        if (containsRegex.IsMatch(filter))
        {
            var matches = containsRegex.Match(filter);
            var property = matches.Groups["Property"];
            var value = matches.Groups["Value"];
            filter = containsRegex.Replace(
                filter,
                $"{property}.contains({value})"
            );
        }

        queryable = queryable.Where(filter);
        return queryable;
    }

    public static IQueryable<T> Order<T>(this IQueryable<T> queryable, string order)
    {
        if (queryable == null) { throw new ArgumentNullException("queryable"); }
        if (string.IsNullOrEmpty(order)) { throw new ArgumentNullException("order"); }

        queryable = queryable.OrderBy(order);
        return queryable;
    }
}