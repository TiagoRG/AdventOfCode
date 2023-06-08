namespace AdventOfCode.Year2022;

public class Day3
{
    private static readonly string[] Backpacks = File.ReadAllLines("inputs/day3.txt");
    
    public Day3()
    {
        Console.WriteLine("Day3 Solution");
        Console.WriteLine($"Part1 Result: {Part1()}");
        Console.WriteLine($"Part2 Result: {Part2()}");
        Console.WriteLine("\n=============================\n");
    }

    private static int Part1()
    {
        int sum = 0;
        
        List<char> duplicates = new List<char>();
        
        foreach (string backpack in Backpacks)
        {
            int compartmentSize = backpack.Length / 2;
            List<char> itemsInCompartment1 = new List<char>();
            List<char> itemsInCompartment2 = new List<char>();
            
            for (int i = 0; i < compartmentSize; i++)
                itemsInCompartment1.Add(backpack[i]);
            for (int i = compartmentSize; i < compartmentSize * 2; i++)
                itemsInCompartment2.Add(backpack[i]);

            char duplicatedItem = itemsInCompartment1.Intersect(itemsInCompartment2).FirstOrDefault();
            duplicates.Add(duplicatedItem);
        }

        foreach (char duplicate in duplicates)
        {
            if (Char.IsUpper(duplicate))
                sum += Convert.ToInt16(duplicate) - 38;
            else
                sum += Convert.ToInt16(duplicate) - 96;
        }

        return sum;
    }
    
    private static int Part2()
    {
        int sum = 0;
        List<List<string>> groups = new List<List<string>>();

        for (int i = 0; i < Backpacks.Length; i+=3)
        {
            List<string> group = new List<string>();

            for (int x = 0; x < 3; x++)
            {
                try
                {
                    group.Add(Backpacks[i + x]);
                }
                catch
                {
                    break;
                }
            }
            
            if (group.All(x => x != ""))
                groups.Add(group);
        }

        List<char> duplicates = new List<char>();
        foreach (List<string> group in groups)
        {
            string[] groupArray = group.ToArray();
            duplicates.Add(groupArray[0].Intersect(groupArray[1].Intersect(groupArray[2])).FirstOrDefault());
        }
        
        foreach (char duplicate in duplicates)
        {
            if (Char.IsUpper(duplicate))
                sum += Convert.ToInt16(duplicate) - 38;
            else
                sum += Convert.ToInt16(duplicate) - 96;
        }

        return sum;
    }
}