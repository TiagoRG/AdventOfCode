using AdventOfCode.Utils;

namespace AdventOfCode.Year2022;

public class Day8
{
    private static readonly List<List<int>> TreeMap = LoadMap();

    public Day8()
    {
        Console.WriteLine("Day8 Solution");
        Console.WriteLine($"Part1 Result: {Part1()}");
        Console.WriteLine($"Part2 Result: {Part2()}");
        Console.WriteLine("\n=============================\n");
    }

    private static int Part1()
    {
        int count = 0;
        for (int i = 0; i < TreeMap.Count; i++)
            for (int j = 0; j < TreeMap[i].Count; j++)
                if (i == 0 || i == TreeMap.Count-1 || j == 0 || j == TreeMap.Count-1 
                    || TreeMap[i].Sublist(0, j).Max() < TreeMap[i][j] 
                    || TreeMap[i].Sublist(j + 1, TreeMap[i].Count).DefaultIfEmpty().Max() < TreeMap[i][j]
                    || TreeMap.GetColumn(j).Sublist(0, i).Max() < TreeMap[i][j] 
                    || TreeMap.GetColumn(j).Sublist(i + 1, TreeMap[i].Count).DefaultIfEmpty().Max() < TreeMap[i][j])
                    count++;
        return count;
    }

    private static int Part2()
    {
        int highestScore = 0;

        for (int i = 0; i < TreeMap.Count; i++)
            for (int j = 0; j < TreeMap[i].Count; j++)
            {
                if (i == 0 || j == 0) continue;

                int currentTree = TreeMap[i][j];
                int currentTreeScore = 1;
                int directionCount = 0;
                
                for (int k = i - 1; k >= 0; k--)
                {
                    directionCount++;
                    if (TreeMap[k][j] >= currentTree) break;
                }

                currentTreeScore *= directionCount;
                directionCount = 0;
                
                for (int k = j - 1; k >= 0; k--)
                {
                    directionCount++;
                    if (TreeMap[i][k] >= currentTree) break;
                }

                currentTreeScore *= directionCount;
                directionCount = 0;

                for (int k = i + 1; k < TreeMap.Count; k++)
                {
                    directionCount++;
                    if (TreeMap[k][j] >= currentTree) break;
                }

                currentTreeScore *= directionCount;
                directionCount = 0;

                for (int k = j + 1; k < TreeMap[i].Count; k++)
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
        List<List<int>> map = new List<List<int>>();
        string[] lines = File.ReadAllLines("inputs/day8.txt");
        for (int i = 0; i < lines.Length; i++)
        {
            map.Add(new List<int>());
            for (int j = 0; j < lines[i].Length; j++)
                map[i].Add(Convert.ToInt32(lines[i][j].ToString()));
        }
        return map;
    }
}