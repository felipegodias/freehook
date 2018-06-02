using System.Collections.Generic;

public static class ListExtensions
{

    public static void AddIfNotContains<T>(this List<T> list, T item)
    {
        if (!list.Contains(item))
        {
            list.Add(item);
        }
    }

    public static void AddRangeIfNotContains<T>(this List<T> list, ICollection<T> items)
    {
        foreach (T item in items)
        {
            list.AddIfNotContains(item);
        }
    }

}