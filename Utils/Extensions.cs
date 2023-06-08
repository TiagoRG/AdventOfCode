namespace AdventOfCode.Utils;

public static class Extensions
{
    /// <summary>
    /// Returns a slice of the given list
    /// </summary>
    /// <param name="list">The list you pretend to slice</param>
    /// <param name="startIndex">The first index of the slice</param>
    /// <param name="endIndex">The index after the last index of the slice</param>
    /// <returns></returns>
    public static List<T> Sublist<T>(this List<T> list, int startIndex, int endIndex)
    {
        List<T> result = new List<T>();
        for (int i = startIndex; i < endIndex; i++)
            result.Add(list[i]);
        return result;
    }

    /// <summary>
    /// Returns a column of a give matrix list
    /// </summary>
    /// <param name="list">2 dimensions list</param>
    /// <param name="column">Column index</param>
    /// <returns></returns>
    public static List<T> GetColumn<T>(this List<List<T>> list, int column)
    {
        List<T> result = new List<T>();
        foreach (List<T> row in list) result.AddRange(row.Where((t, i) => i == column));
        return result;
    }

    /// <summary>
    /// Reverses a given list
    /// </summary>
    /// <param name="list">The list to reverse</param>
    /// <returns>The reversed list</returns>
    public static List<T> Reversed<T>(this List<T> list)
    {
        List<T> reversed = new List<T>();
        for (int i = 0; i < list.Count; i++)
            reversed.Add(list[list.Count-1-i]);
        return reversed;
    }

    /// <summary>
    /// Clones a list of elements
    /// </summary>
    /// <param name="original">The original list</param>
    /// <returns>A copy of the original list</returns>
    public static List<T> Clone<T>(this List<T> original)
    {
        List<T> ret = new List<T>(original.Count);
        foreach (T element in original)
            ret.Add(element);
        return ret;
    }

    public static Dictionary<TKey, TValue> Clone<TKey, TValue>
        (this Dictionary<TKey, TValue> original)
        where TValue : ICloneable
        where TKey : notnull
    {
        Dictionary<TKey, TValue> ret = new Dictionary<TKey, TValue>(original.Count, original.Comparer);
        foreach (KeyValuePair<TKey, TValue> entry in original)
            ret.Add(entry.Key, (TValue) entry.Value.Clone());
        return ret;
    }
    
    public static Dictionary<TKey, List<TValue>> Clone<TKey, TValue>
        (this Dictionary<TKey, List<TValue>> original)
        where TKey : notnull
    {
        Dictionary<TKey, List<TValue>> ret = new Dictionary<TKey, List<TValue>>(original.Count, original.Comparer);
        foreach (KeyValuePair<TKey, List<TValue>> entry in original)
            ret.Add(entry.Key, entry.Value.Clone());
        return ret;
    }
}