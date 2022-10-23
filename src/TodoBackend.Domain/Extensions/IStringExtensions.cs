namespace Starter.Domain.Extensions;

public static class StringExtensions
{
    public static bool HasValue(this string value)
    {
        if (string.IsNullOrEmpty(value) ||
            string.IsNullOrWhiteSpace(value)
           ) { return false; }

        return true;
    }
}