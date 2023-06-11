using AdventOfCode.Utils.Extensions;

namespace AdventOfCode.Year2022;

public class Day9
{
    public Day9()
    {
        Console.WriteLine("Day9 Solution");
        List<(char, int)> moves = new List<(char, int)>();
        File.ReadAllLines("inputs/day9.txt").ToList().ForEach(line =>
        {
            string[] split = line.Split(" ");
            moves.Add((Convert.ToChar(split[0]), Convert.ToInt32(split[1])));
        });
        Console.WriteLine($"Part1 Result: {GetPositionCount(moves, 2)}");
        Console.WriteLine($"Part2 Result: {GetPositionCount(moves, 10)}");
        Console.WriteLine("\n=============================\n");
    }

    private static int GetPositionCount(List<(char, int)> moves, int knots)
    {
        List<List<int>> currentPositions = new List<List<int>>();
        currentPositions.Fill(knots, new List<int>(new[]{0,0}));
        HashSet<(int, int)> tailHistory = new HashSet<(int, int)>();

        foreach ((char, int) move in moves)
            for (int moveN = 0; moveN < move.Item2; moveN++)
            {
                int[] vector = GetVector(move.Item1);
                int[] previousTailPosition = currentPositions[0].ToArray(); 
                currentPositions[0] = UpdateHead(vector, currentPositions[0]);
                for (int tailN = 0; tailN < knots-1; tailN++)
                {
                    int[] nextPreviousTailPosition = currentPositions[tailN + 1].ToArray();
                    currentPositions[tailN + 1] = UpdateTail(currentPositions[tailN], currentPositions[tailN + 1], previousTailPosition);
                    previousTailPosition = nextPreviousTailPosition;
                    if (tailN == knots - 2)
                        tailHistory.Add((currentPositions[knots - 1][0], currentPositions[knots - 1][1]));
                }
            }

        return tailHistory.Count;
    }

    private static List<int> UpdateHead(int[] vector, List<int> currentPosition)
    {
        currentPosition[0] += vector[0];
        currentPosition[1] += vector[1];
        return new List<int> { currentPosition[0], currentPosition[1] };
    }

    private static List<int> UpdateTail(List<int> currentHeadPosition, List<int> currentTailPosition, int[] previousTailPosition)
    {
        List<int> head = new List<int>
        {
            currentHeadPosition[0] - previousTailPosition[0],
            currentHeadPosition[1] - previousTailPosition[1]
        };

        if (Math.Abs(currentHeadPosition[0] - currentTailPosition[0]) > 1 ||
            Math.Abs(currentHeadPosition[1] - currentTailPosition[1]) > 1)
        {
            if (previousTailPosition[0] == currentTailPosition[0] ||
                previousTailPosition[1] == currentTailPosition[1])
                return new List<int>
                {
                    currentTailPosition[0] + head[0],
                    currentTailPosition[1] + head[1]
                };
            
            List<int> difference = new List<int>
            {
                currentHeadPosition[0] - currentTailPosition[0],
                currentHeadPosition[1] - currentTailPosition[1]
            };

            return new List<int>
            {
                currentTailPosition[0] + difference[0].Signal() * Math.Min(Math.Abs(difference[0]), 1),
                currentTailPosition[1] + difference[1].Signal() * Math.Min(Math.Abs(difference[1]), 1)
            };
        }

        return currentTailPosition;
    }

    private static int[] GetVector(char direction)
    {
        return direction switch
        {
            'U' => new[] { 1, 0 },
            'D' => new[] { -1, 0 },
            'R' => new[] { 0, 1 },
            'L' => new[] { 0, -1 },
            _ => new[] { 0, 0 }
        };
    }
}