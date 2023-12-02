namespace AdventOfCode.Year2023;

public class Day1
{
    private static List<string> Input = File.ReadAllLines("inputs/Year2023/day1.txt").ToList();
    private static int FirstNum;
    private static int LastNum;

    public static void Run()
    {
        Console.WriteLine($@"
Day1 Solution
Part1 Result: {Part1()}
Part2 Result: {Part2()}

=============================");
    }

    private static int Part1()
    {
        List<int> numbers = new List<int>();
        Input.ForEach(line =>
        {
            List<char> chars = line.ToList();
            numbers.Add(Convert.ToInt32($"{chars.First(c => Char.IsDigit(c))}{chars.Last(c => Char.IsDigit(c))}"));
        });

        return numbers.Sum();
    }

    private static int Part2()
    {
        string[] validStrNums = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

        List<int> numbers = new List<int>();
        Input.ForEach(line =>
        {
            int min = line.Length;
            int max = -1;
            foreach (var str in validStrNums)
            {
                if (!line.Contains(str)) continue;

                int firstIndex = line.IndexOf(str);
                if (firstIndex < min)
                {
                    min = firstIndex;
                    FirstNum = ParseInt(str);
                }

                int lastIndex = line.LastIndexOf(str);
                if (lastIndex > max)
                {
                    max = lastIndex;
                    LastNum = ParseInt(str);
                }
            }

            numbers.Add(Convert.ToInt32($"{FirstNum}{LastNum}"));
        });

        return numbers.Sum();
    }

    private static int ParseInt(string str)
    {
        return (str) switch
        {
            "one" => 1,
            "two" => 2,
            "three" => 3,
            "four" => 4,
            "five" => 5,
            "six" => 6,
            "seven" => 7,
            "eight" => 8,
            "nine" => 9,
            _ => Convert.ToInt32(str),
        };
    }
}
