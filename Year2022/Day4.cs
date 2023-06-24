namespace AdventOfCode.Year2022;

public class Day4
{
    public Day4()
    {
        var lines = File.ReadAllLines("inputs/day4.txt");
        var containedCount = 0;
        var intersectedCount = 0;
        foreach (var line in lines)
        {
            if (IsContained(line))
                containedCount++;
            if (IsIntersected(line))
                intersectedCount++;
        }

        Console.WriteLine($@"
Day4 Solution
Part1 Result: {containedCount}
Part2 Result: {intersectedCount}

=============================");
    }

    private static bool IsContained(string line)
    {
        var limits = GetLimits(line);
        return (limits[0][0] >= limits[1][0] && limits[0][1] <= limits[1][1])
               || (limits[1][0] >= limits[0][0] && limits[1][1] <= limits[0][1]);
    }

    private static bool IsIntersected(string line)
    {
        var limits = GetLimits(line);
        return (limits[0][1] >= limits[1][0] && limits[0][0] <= limits[1][1])
               || (limits[1][1] >= limits[0][0] && limits[1][0] <= limits[0][1]);
    }

    private static int[][] GetLimits(string line)
    {
        var pair = line.Split(",");
        var pair1 = pair[0].Split("-");
        var pair2 = pair[1].Split("-");
        return new[]
        {
            new[] { Convert.ToInt32(pair1[0]), Convert.ToInt32(pair1[1]) },
            new[] { Convert.ToInt32(pair2[0]), Convert.ToInt32(pair2[1]) }
        };
    }
}