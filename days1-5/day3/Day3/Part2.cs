namespace Day3;

public class Part2
{
    private static int _sum = 0;
    public static void Part2Solver()
    {
        string[] backpacks = File.ReadAllText("input.txt").Split('\n');
        List<List<string>> groups = new List<List<string>>();

        for (int i = 0; i < backpacks.Length; i+=3)
        {
            List<string> group = new List<string>();

            for (int x = 0; x < 3; x++)
            {
                try
                {
                    group.Add(backpacks[i + x]);
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
                _sum += Convert.ToInt16(duplicate) - 38;
            else
                _sum += Convert.ToInt16(duplicate) - 96;
        }
        
        Console.WriteLine($"Sum is {_sum}");
    }
}