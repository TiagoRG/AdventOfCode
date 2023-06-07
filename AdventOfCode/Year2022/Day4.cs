namespace AdventOfCode.Year2022;

public class Day4
{
    public Day4()
    {
        Console.WriteLine("Day4 Solution");

        string[] lines = File.ReadAllLines("inputs/day4.txt");
        int containedCount = 0;
        int intersectedCount = 0;
        foreach (string line in lines)
        {
            if (IsContained(line))
                containedCount++;
            if (IsIntersected(line))
                intersectedCount++;
        }
        
        Console.WriteLine($"Part1 Result: {containedCount}");
        Console.WriteLine($"Part2 Result: {intersectedCount}");
        Console.WriteLine("\n=============================\n");
    }

    public static bool IsContained(string line)
    {
        int[][] limits = GetLimits(line);
        return (limits[0][0] >= limits[1][0] && limits[0][1] <= limits[1][1])
            || (limits[1][0] >= limits[0][0] && limits[1][1] <= limits[0][1]);
    }
    
    public static bool IsIntersected(string line)
    {
        int[][] limits = GetLimits(line);
        return (limits[0][1] >= limits[1][0] && limits[0][0] <= limits[1][1])
               || (limits[1][1] >= limits[0][0] && limits[1][0] <= limits[0][1]);
    }

    public static int[][] GetLimits(string line)
    {
        string[] pair = line.Split(",");
        string[] pair1 = pair[0].Split("-");
        string[] pair2 = pair[1].Split("-");
        return new []
        {
            new [] {Convert.ToInt32(pair1[0]), Convert.ToInt32(pair1[1])},
            new [] {Convert.ToInt32(pair2[0]), Convert.ToInt32(pair2[1])}
        };
    }
}