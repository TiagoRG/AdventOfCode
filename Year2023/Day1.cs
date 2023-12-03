namespace AdventOfCode.Year2023;

public class Day1
{
    private static string[] Input = File.ReadAllLines("inputs/Year2023/day1.txt");

    private static Dictionary<string, int> Part1Numbers = new Dictionary<string, int>() {
        {"1", 1},
        {"2", 2},
        {"3", 3},
        {"4", 4},
        {"5", 5},
        {"6", 6},
        {"7", 7},
        {"8", 8},
        {"9", 9},
    };

    private static Dictionary<string, int> Part2Numbers = new Dictionary<string, int>(Part1Numbers) {
        {"one", 1},
        {"two", 2},
        {"three", 3},
        {"four", 4},
        {"five", 5},
        {"six", 6},
        {"seven", 7},
        {"eight", 8},
        {"nine", 9},
    };

    public static void Run()
    {
        Console.WriteLine($@"
Day1 Solution
Part1 Result: {Solve(Part1Numbers)}
Part2 Result: {Solve(Part2Numbers)}

=============================");
    }

    private static int Solve(Dictionary<string, int> numbers)
        => Input.Select(line =>
        {
            int first = numbers.Select(num => (index: line.IndexOf(num.Key), val: num.Value))
            .Where(num => num.index >= 0)
            .MinBy(num => num.index)
            .val;

            int last = numbers.Select(num => (index: line.LastIndexOf(num.Key), val: num.Value))
            .Where(num => num.index >= 0)
            .MaxBy(num => num.index)
            .val;

            return first * 10 + last;
        }).Sum();
}
