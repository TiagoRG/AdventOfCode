namespace AdventOfCode.Year2022;

public class Day7
{
    private static readonly Dictionary<string, List<string>> Tree = GetTree();
    
    public Day7()
    {
        Console.WriteLine("Day7 Solution");
        Console.WriteLine($"Part1 Result: {Part1()}");
        Console.WriteLine($"Part2 Result: {Part2()}");
        Console.WriteLine("\n=============================\n");
    }

    private static int Part1()
    {
        int sum = 0;
        foreach (string path in Tree.Keys)
        {
            int size = CalculateDirSize(path);
            if (size <= 100000)
                sum += size;
        }

        return sum;
    }
    
    private static int Part2()
    {
        int neededSpace = CalculateDirSize("/") - 40000000;
        if (neededSpace <= 0)
        {
            return 0;
        }

        List<int> bigEnoughDir = new List<int>();
        foreach (string path in Tree.Keys)
        {
            int size = CalculateDirSize(path);
            if (size > neededSpace)
                bigEnoughDir.Add(size);
        }

        bigEnoughDir.Sort();
        return bigEnoughDir[0];
    }

    private static int CalculateDirSize(string path)
    {
        int size = 0;
        List<string> dirContent = Tree[path];
        foreach (string content in dirContent)
        {
            string[] properties = content.Split(" ");
            if (properties[0] == "dir")
                size += CalculateDirSize(path + properties[1] + "/");
            else
                size += Convert.ToInt32(properties[0]);
        }

        return size;
    }

    private static Dictionary<string, List<string>> GetTree()
    {
        Dictionary<string, List<string>> tree = new Dictionary<string, List<string>>();
        string currentPath = "";

        IEnumerable<string> input = File.ReadLines("inputs/day7.txt");
        foreach (string line in input)
        {
            if (line.StartsWith("$"))
            {
                string[] cmdLine = line.Substring(2).Split(" ");
                if (cmdLine[0] == "cd")
                {
                    string dir = cmdLine[1];
                    if (dir == "/")
                        currentPath = "/";
                    else if (dir == "..")
                    {
                        if (currentPath == "/") continue;
                        int slashIndex = currentPath
                            .Substring(0, currentPath.Length - 1)
                            .LastIndexOf('/');
                        currentPath = currentPath.Substring(0, slashIndex + 1);
                    }
                    else
                        currentPath += dir + "/";
                }
            }
            else
            {
                if (!tree.ContainsKey(currentPath))
                    tree.Add(currentPath, new List<string>());
                tree[currentPath].Add(line);
            }
        }

        return tree;
    }
}