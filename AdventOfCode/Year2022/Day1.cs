namespace AdventOfCode.Year2022;

public class Day1
{
    private static List<int> _caloriesPerElf = new();
    
    public Day1()
    {
        Console.WriteLine("\nDay1 Solution\n");
        LoadCaloriesPerElf();
        Console.WriteLine($"Part1\nResult: {Part1()}\n");
        Console.WriteLine($"Part2\nResult: {Part2()}\n");
        Console.WriteLine("=============================\n");
    }

    private static int Part1()
    {
        return _caloriesPerElf.Max();
    }

    private static int Part2()
    {
        List<int> top3 = new List<int>();
        for (int i = 0; i < 3; i++)
        {
            top3.Add(_caloriesPerElf.Max());
            _caloriesPerElf.Remove(_caloriesPerElf.Max());
        }

        return top3.Sum();
    }

    private static void LoadCaloriesPerElf()
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
        _caloriesPerElf = caloriesPerDay.ToList().GetRange(0, elfCount);
    }
}