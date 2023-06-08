namespace AdventOfCode.Utils;

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
}