using System.Text.RegularExpressions;

namespace AdventOfCode.Year2023;

public static class Day4
{
    private class Card
    {
        public int CardNumber { get; set; }
        public int[]? WinningNumbers { get; set; }
        public int[]? Numbers { get; set; }
        public int ValidNumbers { get; set; }
    }

    private static List<Card> Cards = new();
    private static double Sum;

    public static void Run()
    {
        File.ReadAllLines("inputs/Year2023/day4.txt").ToList().ForEach(line =>
        {
            Card card = new Card();
            card.CardNumber = Convert.ToInt32(Regex.Split(line.Split(": ")[0], @"\s+").Last());
            string[] cardNumbers = Regex.Split(line.Split(": ")[1], @"\s+\|\s+");
            card.WinningNumbers = Regex.Split(cardNumbers.First(), @"\W+").Where(x => !string.IsNullOrEmpty(x)).Select(x => Convert.ToInt32(x)).ToArray();
            card.Numbers = Regex.Split(cardNumbers.Last(), @"\W+").Where(x => !string.IsNullOrEmpty(x)).Select(x => Convert.ToInt32(x)).ToArray();
            Cards.Add(card);
        });

        Cards.ForEach(card =>
        {
            card.ValidNumbers = 0;
            card.Numbers!.ToList().ForEach(number => card.ValidNumbers += card.WinningNumbers!.Contains(number) ? 1 : 0);
        });

        Console.WriteLine($@"
Day4 Solution
Part1 Result: {Part1()}
Part2 Result: {Part2()}

=============================");
    }

    private static double Part1()
    {
        Sum = 0;
        Cards.ForEach(card => Sum += card.ValidNumbers > 0 ? Math.Pow(2, card.ValidNumbers - 1) : 0);
        return Sum;
    }

    private static double Part2()
    {
        Sum = 0;

        Dictionary<int, int> CardInstances = new();
        Cards.ForEach(card => CardInstances.Add(card.CardNumber, 1));
        Cards.ForEach(card =>
        {
            for (int i = card.CardNumber + 1; i <= card.CardNumber + card.ValidNumbers; i++)
                CardInstances[i] += CardInstances[card.CardNumber];

            Sum += CardInstances[card.CardNumber];
        });

        return Sum;
    }
}
