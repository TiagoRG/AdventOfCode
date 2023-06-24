namespace AdventOfCode.Year2022;

public class Day6
{
    private static readonly string Input = File.ReadAllText("inputs/day6.txt");

    public Day6()
    {
        Console.WriteLine($@"
Day6 Solution
Part1 Result: {GetValidMarkerIndex(4)}
Part2 Result: {GetValidMarkerIndex(14)}

=============================");
    }

    private static int? GetValidMarkerIndex(int size)
    {
        for (var i = 0; i < Input.Length - size; i++)
            if (ValidateMarker(Input.Substring(i, size)))
                return i + size;
        return null;
    }

    private static bool ValidateMarker(string marker)
    {
        return !marker.Where((c1, i) => marker.Where((c2, j) => c1 == c2 && i != j).Any()).Any();
    }
}