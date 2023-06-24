namespace AdventOfCode.Utils.Extensions;

public static class LinqExtensions
{
    /// <summary>
    ///     Returns a slice of the given list
    /// </summary>
    /// <param name="list">The list you pretend to slice</param>
    /// <param name="startIndex">The first index of the slice</param>
    /// <param name="endIndex">The index after the last index of the slice</param>
    /// <returns></returns>
    public static List<T> Sublist<T>(this List<T> list, int startIndex, int endIndex = default)
    {
        var result = new List<T>();
        if (endIndex == default)
            endIndex = list.Count;
        for (var i = startIndex; i < endIndex; i++)
            result.Add(list[i]);
        return result;
    }

    /// <summary>
    ///     Returns a column of a give matrix list
    /// </summary>
    /// <param name="list">2 dimensions list</param>
    /// <param name="column">Column index</param>
    /// <returns></returns>
    public static List<T> GetColumn<T>(this List<List<T>> list, int column)
    {
        var result = new List<T>();
        foreach (var row in list) result.AddRange(row.Where((t, i) => i == column));
        return result;
    }

    /// <summary>
    ///     Reverses a given list
    /// </summary>
    /// <param name="list">The list to reverse</param>
    /// <returns>The reversed list</returns>
    public static List<T> Reversed<T>(this List<T> list)
    {
        var reversed = new List<T>();
        for (var i = 0; i < list.Count; i++)
            reversed.Add(list[list.Count - 1 - i]);
        return reversed;
    }

    /// <summary>
    ///     Clones a list of elements
    /// </summary>
    /// <param name="original">The original list</param>
    /// <returns>A copy of the original list</returns>
    public static List<T> Clone<T>(this List<T> original)
    {
        var ret = new List<T>(original.Count);
        foreach (var element in original)
            ret.Add(element);
        return ret;
    }

    public static Dictionary<TKey, TValue> Clone<TKey, TValue>
        (this Dictionary<TKey, TValue> original)
        where TValue : ICloneable
        where TKey : notnull
    {
        var ret = new Dictionary<TKey, TValue>(original.Count, original.Comparer);
        foreach (var entry in original)
            ret.Add(entry.Key, (TValue)entry.Value.Clone());
        return ret;
    }

    public static Dictionary<TKey, List<TValue>> Clone<TKey, TValue>
        (this Dictionary<TKey, List<TValue>> original)
        where TKey : notnull
    {
        var ret = new Dictionary<TKey, List<TValue>>(original.Count, original.Comparer);
        foreach (var entry in original)
            ret.Add(entry.Key, entry.Value.Clone());
        return ret;
    }

    public static void Fill<T>(this List<T> list, int count, T element)
        where T : ICloneable
    {
        for (var i = 0; i < count; i++)
            list.Add(element);
    }

    public static void Fill<T>(this List<List<T>> list, int count, List<T> element)
    {
        for (var i = 0; i < count; i++)
            list.Add(element.Clone());
    }

    public static void Set<T>(this List<T> list, int index, T element)
    {
        var backup = list.Clone();
        list.Clear();
        for (var i = 0; i < list.Count; i++) list.Add(i == index ? element : list[i]);
    }
}