namespace AdventOfCode.Utils.Extensions;

public static class MathExtensions
{
    public static int Signal(this int x)
    {
        return x switch
        {
            < 0 => -1,
            > 0 => 1,
            _ => 0
        };
    }

    public static bool IsDigit(this string s)
    {
        try
        {
            Convert.ToInt32(s);
        }
        catch (FormatException ignored)
        {
            return false;
        }

        return true;
    }

    public static ulong ProductOfMax(this List<ulong> list, int maxCount)
    {
        List<ulong> maxList = new List<ulong>(maxCount);

        foreach (ulong number in list)
        {
            if (maxList.Count < maxList.Capacity)
                maxList.Add(number);
            else
            if (number > maxList.Min())
            {
                maxList.RemoveAt(0);
                maxList.Add(number);
            }
            maxList.Sort();
        }

        ulong product = 1;
        maxList.ForEach(n => product*=n);
        return product;
    }

    public static int LeastCommonMultiplier(this List<int> list)
    {
        int lcm = 1;
        foreach (int i in list)
            lcm *= i / MathTools.GreatCommonDivider(lcm, i);
        return lcm;
    }
}