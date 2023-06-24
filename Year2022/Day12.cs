namespace AdventOfCode.Year2022;

public class Day12
{
    private const int SOrd = -14;
    private const int EOrd = -28;
    private static short[,]? _map;
    private static (int, int) _sCoords;
    private static (int, int) _eCoords;

    /// <summary>
    ///     Slowest solution so far.
    ///     Could most certainly use some improvements :skull:
    /// </summary>
    public Day12()
    {
        Console.WriteLine($@"
Day12 Solution
Part1 Result: {Part1()}
Part2 Result: {Part2()}

=============================");
    }

    private static void PartInit()
    {
        var lines = File.ReadAllLines("inputs/day12.txt");
        var map = new short[lines.Length, lines[0].Length];

        for (var i = 0; i < lines.Length; i++)
        for (var j = 0; j < lines[i].Length; j++)
            map[i, j] = (short)(Convert.ToInt16(lines[i][j]) - 97);

        _map = map;
        _sCoords = FindCoords(map, SOrd);
        _eCoords = FindCoords(map, EOrd);
    }

    private static int Part1()
    {
        PartInit();
        var distances = GetIndexes(_map!).Where(item => item != _sCoords)
            .ToDictionary(index => (index.Item1, index.Item2), _ => short.MaxValue);
        distances[(_sCoords.Item1, _sCoords.Item2)] = 0;

        _map![_sCoords.Item1, _sCoords.Item2] = 0;
        _map[_eCoords.Item1, _eCoords.Item2] = (short)(Convert.ToInt16('z') - 97);

        var visited = new List<(int, int)>();
        var current = _sCoords;
        var unvisited = GetIndexes(_map).ToList();

        while (true)
            try
            {
                Dijkstra(current, distances, visited, Part.Part1);
                unvisited.Remove(current);
                current = distances.Where(pair => unvisited.Contains(pair.Key)).MinBy(pair => pair.Value).Key;
            }
            catch (InvalidOperationException)
            {
                return distances[_eCoords];
            }
    }

    private static int Part2()
    {
        PartInit();
        var distances = GetIndexes(_map!).ToDictionary(index => (index.Item1, index.Item2), _ => short.MaxValue);
        distances[(_eCoords.Item1, _eCoords.Item2)] = 0;

        _map![_sCoords.Item1, _sCoords.Item2] = 0;
        _map[_eCoords.Item1, _eCoords.Item2] = (short)(Convert.ToInt16('z') - 97);

        var visited = new List<(int, int)>();
        var current = _eCoords;
        var unvisited = GetIndexes(_map).ToList();
        var aIndexes = FindAllCoords(_map, 0).ToList();

        while (true)
        {
            if (aIndexes.Contains(Dijkstra(current, distances, visited, Part.Part2)))
                return distances.Where(pair => aIndexes.Contains(pair.Key)).Min(pair => pair.Value);
            unvisited.Remove(current);
            current = distances.Where(pair => unvisited.Contains(pair.Key)).MinBy(pair => pair.Value).Key;
        }
    }

    private static (int, int) Dijkstra((int, int) current, IDictionary<(int, int), short> distances,
        ICollection<(int, int)> visited, Part part)
    {
        var nextCoords = new Dictionary<char, (int, int)>
        {
            ['D'] = (current.Item1 + 1, current.Item2),
            ['U'] = (current.Item1 - 1, current.Item2),
            ['R'] = (current.Item1, current.Item2 + 1),
            ['L'] = (current.Item1, current.Item2 - 1)
        };

        if (current.Item1 < _map!.GetLength(0) - 1
            && IsStepPossible(current, nextCoords['D'], part)
            && !visited.Contains(nextCoords['D']))
            UpdateDistances(distances, current, nextCoords['D']);

        if (current.Item1 > 0
            && IsStepPossible(current, nextCoords['U'], part)
            && !visited.Contains(nextCoords['U']))
            UpdateDistances(distances, current, nextCoords['U']);

        if (current.Item2 < _map.GetLength(1) - 1
            && IsStepPossible(current, nextCoords['R'], part)
            && !visited.Contains(nextCoords['R']))
            UpdateDistances(distances, current, nextCoords['R']);

        if (current.Item2 > 0
            && IsStepPossible(current, nextCoords['L'], part)
            && !visited.Contains(nextCoords['L']))
            UpdateDistances(distances, current, nextCoords['L']);

        visited.Add(current);
        return current;
    }

    private static bool IsStepPossible((int, int) currentCoords, (int, int) nextCoord, Part part)
    {
        return part switch
        {
            Part.Part1 => _map![nextCoord.Item1, nextCoord.Item2] - 1 <= _map[currentCoords.Item1, currentCoords.Item2],
            Part.Part2 => _map![nextCoord.Item1, nextCoord.Item2] + 1 >= _map[currentCoords.Item1, currentCoords.Item2],
            _ => throw new ArgumentOutOfRangeException(nameof(part), part, null)
        };
    }

    private static void UpdateDistances(IDictionary<(int, int), short> distances, (int, int) currentCoords,
        (int, int) nextCoords)
    {
        if (distances[(nextCoords.Item1, nextCoords.Item2)] == short.MaxValue ||
            distances[(nextCoords.Item1, nextCoords.Item2)] > distances[(currentCoords.Item1, currentCoords.Item2)])
            distances[(nextCoords.Item1, nextCoords.Item2)] =
                (short)(distances[(currentCoords.Item1, currentCoords.Item2)] + 1);
    }

    /// <summary>
    ///     Finds the first occurrence of a value in a 2D array
    /// </summary>
    /// <param name="array">The 2D array</param>
    /// <param name="value">The value to be found</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Array can't be null</exception>
    private static (int, int) FindCoords(short[,] array, int value)
    {
        if (array == null) throw new ArgumentNullException(nameof(array));
        foreach (var index in GetIndexes(array))
            if (array[index.Item1, index.Item2] == value)
                return (index.Item1, index.Item2);
        return (-1, -1);
    }

    /// <summary>
    ///     Finds all coordinates of a value in a 2D array
    /// </summary>
    /// <param name="array">The 2D array</param>
    /// <param name="value">The value to be found</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Array can't be null</exception>
    private static IEnumerable<(int, int)> FindAllCoords(short[,] array, int value)
    {
        if (array == null) throw new ArgumentNullException(nameof(array));
        return from index in GetIndexes(array)
            where array[index.Item1, index.Item2] == value
            select (index.Item1, index.Item2);
    }

    /// <summary>
    ///     Returns an enumerable of all indexes of a 2D array
    /// </summary>
    /// <param name="array">The 2D array</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Array can't be null</exception>
    private static IEnumerable<(int, int)> GetIndexes(short[,] array)
    {
        if (array == null) throw new ArgumentNullException(nameof(array));
        for (var i = 0; i < array.GetLength(0); i++)
        for (var j = 0; j < array.GetLength(1); j++)
            yield return (i, j);
    }

    private enum Part
    {
        Part1,
        Part2
    }
}