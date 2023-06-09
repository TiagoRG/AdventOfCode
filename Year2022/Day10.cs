using AdventOfCode.Utils;

namespace AdventOfCode.Year2022;

public class Day10
{
    private static readonly List<string> CommandsList = new();
    public Day10()
    {
        Console.WriteLine("Day10 Solution");
        File.ReadAllLines("inputs/day10.txt").ToList().ForEach(line => CommandsList.Add(line));
        
        Console.WriteLine($"Part1 Result: {Part1()}");
        Console.WriteLine($"Part2 Result: {Part2()}");
        Console.WriteLine("\n=============================\n");
    }

    private static int Part1()
    {
        int sum = 0;
        int currentPower = 1;

        int listIndex = 0;
        int addPower = 0;
        int skipCycles = 0;
        
        for (int cycle = 1; cycle <= 220; cycle++)
        {
            if (skipCycles == 0)
            {
                currentPower += addPower;
                string line = CommandsList[listIndex];
                string cmd = line.Substring(0, 4);
                skipCycles = cmd == "noop" ? 0 : 1;
                addPower = cmd == "noop" ? 0 : Convert.ToInt32(line.Split(" ")[1]);
                listIndex++;
            }
            else
                skipCycles--;
            
            if ((cycle - 20) % 40 == 0)
                sum += currentPower * cycle;
        }

        return sum;
    }

    private static string Part2()
    {
        char[] crt = new char[40*6];
        int registerPos = 0;

        int listIndex = 0;
        int movePos = 0;
        int skipCycles = 0;
        
        for (int cycle = 0; cycle < 240; cycle++)
        {
            if (skipCycles == 0)
            {
                registerPos += movePos;
                string line = CommandsList[listIndex];
                string cmd = line[..4];
                skipCycles = cmd == "noop" ? 0 : 1;
                movePos = cmd == "noop" ? 0 : Convert.ToInt32(line.Split(" ")[1]);
                listIndex++;
            }
            else
                skipCycles--;

            char[] sprite = CreateSprite(registerPos);
            crt[cycle] = sprite[cycle%40];
        }

        string result = "\n";
        int charI = 0;
        crt.ToList().ForEach(c =>
        {
            result += c.ToString();
            if ((charI+1)%40==0)
                result += "\n";
            charI++;
        });
        return result;
    }

    private static char[] CreateSprite(int registerPos)
    {
        char[] result = new char[40];
        for (int i = 0; i < 40; i++)
            result[i] = i > registerPos-1 && i < registerPos+3 ? '#' : '.';
        return result;
    }
}