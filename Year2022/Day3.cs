namespace AdventOfCode.Year2022;

public class Day3
{
    private static readonly string[] Backpacks = File.ReadAllLines("inputs/day3.txt");

    public Day3()
    {
        Console.WriteLine($@"
Day3 Solution
Part1 Result: {Part1()}
Part2 Result: {Part2()}

=============================");
    }

    private static int Part1()
    {
        var sum = 0;

        var duplicates = new List<char>();

        foreach (var backpack in Backpacks)
        {
            var compartmentSize = backpack.Length / 2;
            var itemsInCompartment1 = new List<char>();
            var itemsInCompartment2 = new List<char>();

            for (var i = 0; i < compartmentSize; i++)
                itemsInCompartment1.Add(backpack[i]);
            for (var i = compartmentSize; i < compartmentSize * 2; i++)
                itemsInCompartment2.Add(backpack[i]);

            var duplicatedItem = itemsInCompartment1.Intersect(itemsInCompartment2).FirstOrDefault();
            duplicates.Add(duplicatedItem);
        }

        foreach (var duplicate in duplicates)
            if (char.IsUpper(duplicate))
                sum += Convert.ToInt16(duplicate) - 38;
            else
                sum += Convert.ToInt16(duplicate) - 96;

        return sum;
    }

    private static int Part2()
    {
        var sum = 0;
        var groups = new List<List<string>>();

        for (var i = 0; i < Backpacks.Length; i += 3)
        {
            var group = new List<string>();

            for (var x = 0; x < 3; x++)
                try
                {
                    group.Add(Backpacks[i + x]);
                }
                catch
                {
                    break;
                }

            if (group.All(x => x != ""))
                groups.Add(group);
        }

        var duplicates = new List<char>();
        foreach (var group in groups)
        {
            var groupArray = group.ToArray();
            duplicates.Add(groupArray[0].Intersect(groupArray[1].Intersect(groupArray[2])).FirstOrDefault());
        }

        foreach (var duplicate in duplicates)
            if (char.IsUpper(duplicate))
                sum += Convert.ToInt16(duplicate) - 38;
            else
                sum += Convert.ToInt16(duplicate) - 96;

        return sum;
    }
}