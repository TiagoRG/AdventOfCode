namespace Day3;

public class Part1
{
    private static int _sum = 0;
    public static void Part1Solver()
    {
        string[] backpacks = File.ReadAllText("input.txt").Split('\n');
        List<char> duplicates = new List<char>();
        
        foreach (string backpack in backpacks)
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

        duplicates.RemoveAt(300);
        
        foreach (char duplicate in duplicates)
        {
            if (Char.IsUpper(duplicate))
                _sum += Convert.ToInt16(duplicate) - 38;
            else
                _sum += Convert.ToInt16(duplicate) - 96;
        }
        
        Console.WriteLine($"Sum is {_sum}");
    }
}