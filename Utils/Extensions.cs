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
}