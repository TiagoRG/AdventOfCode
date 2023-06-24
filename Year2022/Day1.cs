namespace AdventOfCode.Year2022;

public class Day1
{
    private static readonly List<int> CaloriesPerElf = GetCaloriesPerElf();

    public Day1()
    {
        Console.WriteLine($@"
Day1 Solution
Part1 Result: {Part1()}
Part2 Result: {Part2()}

=============================");
    }

    private static int Part1()
    {
        return CaloriesPerElf.Max();
    }

    private static int Part2()
    {
        return CaloriesPerElf.Order().Reverse().ToArray()[..3].Sum();
    }

    private static List<int> GetCaloriesPerElf()
    {
        var calories = File.ReadAllLines("inputs/day1.txt");
        var caloriesPerDay = new int[calories.Length];
        var index = 0;
        foreach (var calorie in calories)
            if (calorie == "")
                index++;
            else
                caloriesPerDay[index] += Convert.ToInt32(calorie);

        var elfCount = caloriesPerDay.ToList().IndexOf(0);
        return caloriesPerDay.ToList().GetRange(0, elfCount);
    }
}