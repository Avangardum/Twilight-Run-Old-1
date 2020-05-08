using System;
using System.Collections.Generic;

public static class TwilightRunExtensions
{
    public static void ThrowIfNull(this object obj, string name)
    {
        if (obj == null) throw new ArgumentException($"Argument {name} is null");
    }

    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        source.ThrowIfNull("source");
        action.ThrowIfNull("action");
        foreach (T element in source)
        {
            action(element);
        }
    }
}
