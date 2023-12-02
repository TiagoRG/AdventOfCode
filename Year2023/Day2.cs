namespace AdventOfCode.Year2023;

public static class Day2
{
    private static List<string> Input = File.ReadAllLines("inputs/Year2023/day2.txt").ToList();

    private class Game
    {
        public int Id { get; set; }
        public List<Dictionary<string, int>> Sets { get; set; } = new List<Dictionary<string, int>>();
    }
    private static List<Game> Games = new List<Game>();

    public static void Run()
    {
        ParseGames();
        Console.WriteLine($@"
Day2 Solution
Part1 Result: {Part1()}
Part2 Result: {Part2()}

=============================");
    }

    private static int Part1()
    {
        int sum = 0;

        foreach (var game in Games)
        {
            foreach (var set in game.Sets)
                if (set["red"] > 12 || set["green"] > 13 || set["blue"] > 14) goto cont;
            sum += game.Id;
        cont: continue;
        }

        return sum;
    }

    private static int Part2()
    {
        int sum = 0;

        foreach (var game in Games)
        {
            Dictionary<string, int> minimum = new Dictionary<string, int> {
                {"red", 0},
                {"green", 0},
                {"blue", 0}
            };
            foreach (var set in game.Sets)
            {
                foreach (var color in set.Keys)
                {
                    if (minimum[color] < set[color]) minimum[color] = set[color];
                }
            }
            sum += minimum.Values.ToArray().Product();
        }

        return sum;
    }

    private static void ParseGames()
    {
        foreach (var line in Input)
        {
            Game game = new Game();

            string[] game_sets = line.Split(": ");
            game.Id = Convert.ToInt32(game_sets[0].Split(" ")[1]);

            string[] sets = game_sets[1].Split("; ");

            foreach (var setStr in sets)
            {
                Dictionary<string, int> set = new Dictionary<string, int>();
                foreach (var color in setStr.Split(", "))
                {
                    set.Add(color.Split(" ")[1], Convert.ToInt32(color.Split(" ")[0]));
                }
                if (!set.ContainsKey("red")) set.Add("red", 0);
                if (!set.ContainsKey("green")) set.Add("green", 0);
                if (!set.ContainsKey("blue")) set.Add("blue", 0);
                game.Sets.Add(set);
            }

            Games.Add(game);
        }
    }

    private static int Product(this int[] array)
    {
        int result = 1;
        foreach (var e in array)
            result *= e;
        return result;
    }
}
