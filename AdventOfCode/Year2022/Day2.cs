namespace AdventOfCode.Year2022;

public class Day2
{
    private static readonly List<string[]> _matches = new();

    public Day2()
    {
        Console.WriteLine("Day2 Solution");
        LoadMatches();
        Console.WriteLine($"Part1 Result: {Part1()}");
        Console.WriteLine($"Part2 Result: {Part2()}");
        Console.WriteLine("\n=============================\n");
    }

    private static int Part1()
    {
        int score = 0;
        foreach (string[] match in _matches)
        {
            if (match[0] == "A")
            {
                if (match[1] == "X")
                    score += 1 + 3;
                if (match[1] == "Y")
                    score += 2 + 6;
                if (match[1] == "Z")
                    score += 3;
            }
            if (match[0] == "B")
            {
                if (match[1] == "X")
                    score += 1;
                if (match[1] == "Y")
                    score += 2 + 3;
                if (match[1] == "Z")
                    score += 3 + 6;
            }
            if (match[0] == "C")
            {
                if (match[1] == "X")
                    score += 1 + 6;
                if (match[1] == "Y")
                    score += 2;
                if (match[1] == "Z")
                    score += 3 + 3;
            }
        }

        return score;
    }

    private static int Part2()
    {
        int score = 0;
        foreach (string[] match in _matches)
        {
            if (match[0] == "A")
            {
                if (match[1] == "X")
                    score += 3;
                if (match[1] == "Y")
                    score += 1 + 3;
                if (match[1] == "Z")
                    score += 2 + 6;
            }
            if (match[0] == "B")
            {
                if (match[1] == "X")
                    score += 1;
                if (match[1] == "Y")
                    score += 2 + 3;
                if (match[1] == "Z")
                    score += 3 + 6;
            }
            if (match[0] == "C")
            {
                if (match[1] == "X")
                    score += 2;
                if (match[1] == "Y")
                    score += 3 + 3;
                if (match[1] == "Z")
                    score += 1 + 6;
            }
        }

        return score;
    }

    private static void LoadMatches()
    {
        foreach (string line in File.ReadAllLines("inputs/day2.txt"))
            _matches.Add(line.Split(" "));
    }
}