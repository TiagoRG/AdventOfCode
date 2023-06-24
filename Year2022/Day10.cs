namespace AdventOfCode.Year2022;

public class Day10
{
    private static readonly List<string> CommandsList = new();

    public Day10()
    {
        File.ReadAllLines("inputs/day10.txt").ToList().ForEach(line => CommandsList.Add(line));
        Console.WriteLine($@"
Day10 Solution
Part1 Result: {Part1()}
Part2 Result: {Part2()}
=============================");
    }

    private static int Part1()
    {
        var sum = 0;
        var currentPower = 1;

        var listIndex = 0;
        var addPower = 0;
        var skipCycles = 0;

        for (var cycle = 1; cycle <= 220; cycle++)
        {
            if (skipCycles == 0)
            {
                currentPower += addPower;
                var line = CommandsList[listIndex];
                var cmd = line.Substring(0, 4);
                skipCycles = cmd == "noop" ? 0 : 1;
                addPower = cmd == "noop" ? 0 : Convert.ToInt32(line.Split(" ")[1]);
                listIndex++;
            }
            else
            {
                skipCycles--;
            }

            if ((cycle - 20) % 40 == 0)
                sum += currentPower * cycle;
        }

        return sum;
    }

    private static string Part2()
    {
        var crt = new char[40 * 6];
        var registerPos = 0;

        var listIndex = 0;
        var movePos = 0;
        var skipCycles = 0;

        for (var cycle = 0; cycle < 240; cycle++)
        {
            if (skipCycles == 0)
            {
                registerPos += movePos;
                var line = CommandsList[listIndex];
                var cmd = line[..4];
                skipCycles = cmd == "noop" ? 0 : 1;
                movePos = cmd == "noop" ? 0 : Convert.ToInt32(line.Split(" ")[1]);
                listIndex++;
            }
            else
            {
                skipCycles--;
            }

            var sprite = CreateSprite(registerPos);
            crt[cycle] = sprite[cycle % 40];
        }

        var result = "\n";
        var charI = 0;
        crt.ToList().ForEach(c =>
        {
            result += c.ToString();
            if ((charI + 1) % 40 == 0)
                result += "\n";
            charI++;
        });
        return result;
    }

    private static char[] CreateSprite(int registerPos)
    {
        var result = new char[40];
        for (var i = 0; i < 40; i++)
            result[i] = i > registerPos - 1 && i < registerPos + 3 ? '#' : ' ';
        return result;
    }
}