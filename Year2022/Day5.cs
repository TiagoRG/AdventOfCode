using AdventOfCode.Utils;

namespace AdventOfCode.Year2022;

public class Day5
{
    private static readonly Dictionary<int, List<char>> Crates = new()
    {
        [1] = new(),
        [2] = new(),
        [3] = new(),
        [4] = new(),
        [5] = new(),
        [6] = new(),
        [7] = new(),
        [8] = new(),
        [9] = new()
    };

    private static readonly List<(int, int, int)> Moves = new();

    public Day5()
    {
        Console.WriteLine("Day5 Solution");

        string[] input = File.ReadAllText("inputs/day5.txt").Split("\n\n");
        LoadCrates(input[0]);
        LoadMoves(input[1]);

        Console.WriteLine($"Part1 Result: {Parts(Part.Part1)}");
        Console.WriteLine($"Part2 Result: {Parts(Part.Part2)}");
        Console.WriteLine("\n=============================\n");
    }

    private static string Parts(Part part)
    {
        Dictionary<int, List<char>> crates = Crates.Clone();
        foreach ((int, int, int) move in Moves)
        {
            List<char> cratesMoved = crates[move.Item2].Sublist(0, move.Item1);
            for (int i = 0; i < move.Item1; i++)
                crates[move.Item2].Remove(crates[move.Item2][0]);
            foreach (char crate in part == Part.Part1 ? cratesMoved : cratesMoved.Reversed())
                crates[move.Item3].Insert(0, crate);
        }

        string result = "";
        foreach (List<char> crateList in crates.Values)
            result += crateList[0];
        return result;
    }

    private static void LoadCrates(string crates)
    {
        string[] crate_lines = crates.Split("\n");
        foreach (string line in crate_lines)
        {
            int firstCrate = 0;
            if (line[firstCrate] == '[')
                Crates[1].Add(line[1]);
            while (line.Substring(firstCrate+1).Contains('['))
            {
                firstCrate = line.IndexOf('[', firstCrate + 1);
                Crates[firstCrate / 4 + 1].Add(line[firstCrate + 1]);
            }
        }
    }

    private static void LoadMoves(string moves)
    {
        string[] move_lines = moves.Split("\n");
        foreach (string line in move_lines)
        {
            string move = line.Substring(5);
            string[] moved = move.Split(" from ");
            string[] crates = moved[1].Split(" to ");
            Moves.Add((Convert.ToInt32(moved[0]), Convert.ToInt32(crates[0]), Convert.ToInt32(crates[1])));
        }
    }

    private enum Part
    {
        Part1, Part2
    }
}