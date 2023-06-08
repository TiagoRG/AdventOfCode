namespace AdventOfCode.Year2022;

public class Day9
{
    private static readonly List<(char, int)> Moves = new();
    private static readonly Dictionary<char, List<int>> Position = new()
    {
        ['H'] = new List<int>(),
        ['T'] = new List<int>()
    };
    
    public Day9()
    {
        Console.WriteLine("Day9 Solution");
        File.ReadAllLines("inputs/day9.txt").ToList().ForEach(line =>
        {
            string[] split = line.Split(" ");
            Moves.Add((Convert.ToChar(split[0]), Convert.ToInt32(split[1])));
        });
        Console.WriteLine($"Part1 Result: {Part1()}");
        Console.WriteLine($"Part2 Result: {Part2()}");
        Console.WriteLine("\n=============================\n");
    }

    private static int Part1()
    {
        Position['H'] = new[] {0, 0}.ToList();
        Position['T'] = new[] {0, 0}.ToList();
        HashSet<(int, int)> usedPositions = new HashSet<(int, int)>();

        foreach ((char, int) move in Moves)
        {
            List<int> head = Position['H'];
            List<int> tail = Position['T'];
            for (int i = 0; i < move.Item2; i++)
            {
                if (move.Item1 == 'U')
                {
                    head[1]++;
                    if (ValidatePosition()) continue;
                    tail[1]++;
                    tail[0] += head[0] - tail[0];
                }
                else if (move.Item1 == 'D')
                {
                    head[1]--;
                    if (ValidatePosition()) continue;
                    tail[1]--;
                    tail[0] += head[0] - tail[0];
                }
                else if (move.Item1 == 'R')
                {
                    head[0]++;
                    if (ValidatePosition()) continue;
                    tail[0]++;
                    tail[1] += head[1] - tail[1];
                }
                else if (move.Item1 == 'L')
                {
                    head[0]--;
                    if (ValidatePosition()) continue;
                    tail[0]--;
                    tail[1] += head[1] - tail[1];
                }
                usedPositions.Add((tail[0], tail[1]));
            }
        }
        return usedPositions.Count;
    }

    private static int Part2()
    {
        Position['H'] = new[] {0, 0}.ToList();
        Position['T'] = new[] {0, 0}.ToList();
        HashSet<(int, int)> usedPositions = new HashSet<(int, int)>();

        foreach ((char, int) move in Moves)
        {
            
        }
        
        return usedPositions.Count;
    }

    private static bool ValidatePosition()
        => Position['H'][0] - Position['T'][0] is > -2 and < 2 && Position['H'][1] - Position['T'][1] is > -2 and < 2;
}