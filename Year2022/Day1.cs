namespace AdventOfCode.Year2022;

public class Day1
{
    private static readonly List<int> CaloriesPerElf = GetCaloriesPerElf();
    
    public Day1()
    {
        Console.WriteLine("\nDay1 Solution");
        Console.WriteLine($"Part1 Result: {Part1()}");
        Console.WriteLine($"Part2 Result: {Part2()}");
        Console.WriteLine("\n=============================\n");
    }

    private static int Part1()
    {
        return CaloriesPerElf.Max();
    }

    private static int Part2()
    {
        List<int> top3 = new List<int>();
        for (int i = 0; i < 3; i++)
        {
            top3.Add(CaloriesPerElf.Max());
            CaloriesPerElf.Remove(CaloriesPerElf.Max());
        }

        return top3.Sum();
    }

    private static List<int> GetCaloriesPerElf()
    {
        string[] calories = File.ReadAllLines("inputs/day1.txt");
        int[] caloriesPerDay = new int[calories.Length];
        var index = 0;
        foreach (string calorie in calories)
        {
            if (calorie == "")
                index++;
            else
                caloriesPerDay[index] += Convert.ToInt32(calorie);
        }

        int elfCount = caloriesPerDay.ToList().IndexOf(0);
        return caloriesPerDay.ToList().GetRange(0, elfCount);
    }
}