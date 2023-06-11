namespace AdventOfCode.Utils;

public static class MathTools
{
    public static int GreatCommonDivider(int x, int y)
    {
        if (x % y == 0)
            return y;
        return GreatCommonDivider(y, x % y);
    }
}