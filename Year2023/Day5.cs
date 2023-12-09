namespace AdventOfCode.Year2023;

public static class Day5
{
    private class Converter
    {
        public string? Name { get; set; }
        public long Start { get; set; }
        public long End { get; set; }
        public long Count { get; set; }
    }
    private static string[] MapTypes = { "seed-to-soil", "soil-to-fertilizer", "fertilizer-to-water", "water-to-light", "light-to-temperature", "temperature-to-humidity", "humidity-to-location", };
    private static List<Converter> Converters = new List<Converter>();

    private static string? CurrentMap;

    public static void Run()
    {
        string[] input = File.ReadAllLines("inputs/Year2023/day5.txt")[2..].ToArray();

        foreach (var line in input)
        {
            if (String.IsNullOrEmpty(line))
            {
                continue;
            }
            if (Char.IsLetter(line[0]))
            {
                CurrentMap = line.Split(" ")[0];
                continue;
            }
            Converters.Add(new Converter
            {
                Name = CurrentMap,
                Start = Convert.ToInt64(line.Split(" ")[1]),
                End = Convert.ToInt64(line.Split(" ")[0]),
                Count = Convert.ToInt64(line.Split(" ")[2])
            });
        }

        Console.WriteLine($@"
Day5 Solution
Part1 Result: {Part1()}

=============================");
    }

    private static long Part1()
    {
        List<long> seeds = File.ReadAllLines("inputs/Year2023/day5.txt")[0].Split(": ")[1].Split(" ").Select(long.Parse).ToList();
        List<long> locations = new List<long>(seeds);

        for (int i = 0; i < seeds.Count(); i++)
        {
            locations[i] = seeds[i];
            foreach (var type in MapTypes)
            {
                Converter? conv = Converters.Where(c => c.Name == type).Where(c => (c.Start <= locations[i] && c.Start + c.Count > locations[i])).FirstOrDefault();
                if (conv == null) continue;
                locations[i] = conv.End + locations[i] - conv.Start;
            }
        }

        return locations.Min();
    }
}
