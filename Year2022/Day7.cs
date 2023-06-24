namespace AdventOfCode.Year2022;

public class Day7
{
    private static readonly Dictionary<string, List<string>> Tree = GetTree();

    public Day7()
    {
        Console.WriteLine($@"
Day7 Solution
Part1 Result: {Part1()}
Part2 Result: {Part2()}

=============================");
    }

    private static int Part1()
    {
        var sum = 0;
        foreach (var path in Tree.Keys)
        {
            var size = CalculateDirSize(path);
            if (size <= 100000)
                sum += size;
        }

        return sum;
    }

    private static int Part2()
    {
        var neededSpace = CalculateDirSize("/") - 40000000;
        if (neededSpace <= 0) return 0;

        var bigEnoughDir = new List<int>();
        foreach (var path in Tree.Keys)
        {
            var size = CalculateDirSize(path);
            if (size > neededSpace)
                bigEnoughDir.Add(size);
        }

        bigEnoughDir.Sort();
        return bigEnoughDir[0];
    }

    private static int CalculateDirSize(string path)
    {
        var size = 0;
        var dirContent = Tree[path];
        foreach (var content in dirContent)
        {
            var properties = content.Split(" ");
            if (properties[0] == "dir")
                size += CalculateDirSize(path + properties[1] + "/");
            else
                size += Convert.ToInt32(properties[0]);
        }

        return size;
    }

    private static Dictionary<string, List<string>> GetTree()
    {
        var tree = new Dictionary<string, List<string>>();
        var currentPath = "";

        var input = File.ReadLines("inputs/day7.txt");
        foreach (var line in input)
            if (line.StartsWith("$"))
            {
                var cmdLine = line.Substring(2).Split(" ");
                if (cmdLine[0] == "cd")
                {
                    var dir = cmdLine[1];
                    if (dir == "/")
                    {
                        currentPath = "/";
                    }
                    else if (dir == "..")
                    {
                        if (currentPath == "/") continue;
                        var slashIndex = currentPath
                            .Substring(0, currentPath.Length - 1)
                            .LastIndexOf('/');
                        currentPath = currentPath.Substring(0, slashIndex + 1);
                    }
                    else
                    {
                        currentPath += dir + "/";
                    }
                }
            }
            else
            {
                if (!tree.ContainsKey(currentPath))
                    tree.Add(currentPath, new List<string>());
                tree[currentPath].Add(line);
            }

        return tree;
    }
}