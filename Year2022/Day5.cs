using AdventOfCode.Utils.Extensions;

namespace AdventOfCode.Year2022;

public class Day5
{
    private static readonly Dictionary<int, List<char>> Crates = new()
    {
        [1] = new List<char>(),
        [2] = new List<char>(),
        [3] = new List<char>(),
        [4] = new List<char>(),
        [5] = new List<char>(),
        [6] = new List<char>(),
        [7] = new List<char>(),
        [8] = new List<char>(),
        [9] = new List<char>()
    };

    private static readonly List<(int, int, int)> Moves = new();

    public Day5()
    {
        var input = File.ReadAllText("inputs/day5.txt").Split("\n\n");
        LoadCrates(input[0]);
        LoadMoves(input[1]);
        Console.WriteLine($@"
Day5 Solution
Part1 Result: {Parts(Part.Part1)}
Part2 Result: {Parts(Part.Part2)}

=============================");
    }

    private static string Parts(Part part)
    {
        var crates = Crates.Clone();
        foreach (var move in Moves)
        {
            var cratesMoved = crates[move.Item2].Sublist(0, move.Item1);
            for (var i = 0; i < move.Item1; i++)
                crates[move.Item2].Remove(crates[move.Item2][0]);
            foreach (var crate in part == Part.Part1 ? cratesMoved : cratesMoved.Reversed())
                crates[move.Item3].Insert(0, crate);
        }

        var result = "";
        foreach (var crateList in crates.Values)
            result += crateList[0];
        return result;
    }

    private static void LoadCrates(string crates)
    {
        var crate_lines = crates.Split("\n");
        foreach (var line in crate_lines)
        {
            var firstCrate = 0;
            if (line[firstCrate] == '[')
                Crates[1].Add(line[1]);
            while (line.Substring(firstCrate + 1).Contains('['))
            {
                firstCrate = line.IndexOf('[', firstCrate + 1);
                Crates[firstCrate / 4 + 1].Add(line[firstCrate + 1]);
            }
        }
    }

    private static void LoadMoves(string moves)
    {
        var move_lines = moves.Split("\n");
        foreach (var line in move_lines)
        {
            var move = line.Substring(5);
            var moved = move.Split(" from ");
            var crates = moved[1].Split(" to ");
            Moves.Add((Convert.ToInt32(moved[0]), Convert.ToInt32(crates[0]), Convert.ToInt32(crates[1])));
        }
    }

    private enum Part
    {
        Part1,
        Part2
    }
}