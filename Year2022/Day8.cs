using AdventOfCode.Utils.Extensions;

namespace AdventOfCode.Year2022;

public class Day8
{
    private static readonly List<List<int>> TreeMap = LoadMap();

    public Day8()
    {
        Console.WriteLine($@"
Day8 Solution
Part1 Result: {Part1()}
Part2 Result: {Part2()}

=============================");
    }

    private static int Part1()
    {
        var count = 0;
        for (var i = 0; i < TreeMap.Count; i++)
        for (var j = 0; j < TreeMap[i].Count; j++)
            if (i == 0 || i == TreeMap.Count - 1 || j == 0 || j == TreeMap.Count - 1
                || TreeMap[i].Sublist(0, j).Max() < TreeMap[i][j]
                || TreeMap[i].Sublist(j + 1, TreeMap[i].Count).DefaultIfEmpty().Max() < TreeMap[i][j]
                || TreeMap.GetColumn(j).Sublist(0, i).Max() < TreeMap[i][j]
                || TreeMap.GetColumn(j).Sublist(i + 1, TreeMap[i].Count).DefaultIfEmpty().Max() < TreeMap[i][j])
                count++;
        return count;
    }

    private static int Part2()
    {
        var highestScore = 0;

        for (var i = 0; i < TreeMap.Count; i++)
        for (var j = 0; j < TreeMap[i].Count; j++)
        {
            if (i == 0 || j == 0) continue;

            var currentTree = TreeMap[i][j];
            var currentTreeScore = 1;
            var directionCount = 0;

            for (var k = i - 1; k >= 0; k--)
            {
                directionCount++;
                if (TreeMap[k][j] >= currentTree) break;
            }

            currentTreeScore *= directionCount;
            directionCount = 0;

            for (var k = j - 1; k >= 0; k--)
            {
                directionCount++;
                if (TreeMap[i][k] >= currentTree) break;
            }

            currentTreeScore *= directionCount;
            directionCount = 0;

            for (var k = i + 1; k < TreeMap.Count; k++)
            {
                directionCount++;
                if (TreeMap[k][j] >= currentTree) break;
            }

            currentTreeScore *= directionCount;
            directionCount = 0;

            for (var k = j + 1; k < TreeMap[i].Count; k++)
            {
                directionCount++;
                if (TreeMap[i][k] >= currentTree) break;
            }

            currentTreeScore *= directionCount;
            highestScore = Math.Max(highestScore, currentTreeScore);
        }

        return highestScore;
    }

    private static List<List<int>> LoadMap()
    {
        var map = new List<List<int>>();
        var lines = File.ReadAllLines("inputs/day8.txt");
        for (var i = 0; i < lines.Length; i++)
        {
            map.Add(new List<int>());
            for (var j = 0; j < lines[i].Length; j++)
                map[i].Add(Convert.ToInt32(lines[i][j].ToString()));
        }

        return map;
    }
}