using AdventOfCode.Utils.Extensions;

namespace AdventOfCode.Year2022;

public class Day11
{
    private static readonly List<Monkey> Part1Monkeys = new();
    private static readonly List<Monkey> Part2Monkeys = new();

    public Day11()
    {
        File.ReadAllText("inputs/day11.txt").Split("\n\n").ToList().ForEach(monkey =>
        {
            var lines = monkey.Split("\n");

            // Getting starting items
            var items1 = new List<Item>();
            var items2 = new List<Item>();
            lines[1][(lines[1].IndexOf(':') + 2)..].Split(", ").ToList().ForEach(item =>
            {
                items1.Add(new Item(Convert.ToInt32(item)));
                items2.Add(new Item(Convert.ToInt32(item)));
            });

            // Getting operation
            var op = lines[2][19..];

            // Getting test info
            var test = Convert.ToInt32(lines[3][21..]);
            var testMonkeys = (Convert.ToInt32(lines[4][29..]), Convert.ToInt32(lines[5][30..]));

            Part1Monkeys.Add(new Monkey(items1, op, test, testMonkeys));
            Part2Monkeys.Add(new Monkey(items2, op, test, testMonkeys));
        });
        Console.WriteLine($@"
Day11 Solution
Part1 Result: {Part1()}
Part2 Result: {Part2()}

=============================");
    }

    private static ulong Part1()
    {
        var inspections = new ulong[Part1Monkeys.Count];

        for (var round = 0; round < 20; round++)
            foreach (var monkey in Part1Monkeys)
            {
                foreach (var item in monkey.Items)
                {
                    if (monkey.Operation.Contains('+'))
                        item.Value += Convert.ToInt32(monkey.Operation.Split(" ").Last());
                    else if (monkey.Operation == "old * old")
                        item.Value *= item.Value;
                    else
                        item.Value *= Convert.ToInt32(monkey.Operation.Split(" ").Last());
                    item.Value /= 3;
                    Part1Monkeys[
                        item.Value % monkey.DivisionTest == 0
                            ? monkey.TestMonkeys.Item1
                            : monkey.TestMonkeys.Item2
                    ].Items.Add(item);
                    inspections[Part1Monkeys.IndexOf(monkey)]++;
                }

                monkey.Items.Clear();
            }

        return new List<ulong>(inspections).ProductOfMax(2);
    }

    private static ulong Part2()
    {
        for (double round = 0; round < 10000; round++)
            foreach (var monkey in Part2Monkeys)
            {
                monkey.UpdateItems();
                var receiversAndItems = monkey.GetReceiversAndItems();
                foreach (var key in receiversAndItems.Keys)
                    Part2Monkeys[key].ReceiveItems(receiversAndItems[key]);
            }

        var topMonkeys = new List<ulong>();
        Part2Monkeys.ForEach(monkey => topMonkeys.Add((ulong)monkey.TotalItemsChecked));
        return topMonkeys.ProductOfMax(2);
    }

    private class Monkey
    {
        public Monkey(List<Item> items, string operation, int divisionTest, (int, int) testMonkeys)
        {
            Items = items;
            Operation = operation;
            DivisionTest = divisionTest;
            TestMonkeys = testMonkeys;
            TotalItemsChecked = 0;
        }

        public List<Item> Items { get; private set; }
        public string Operation { get; }
        public int DivisionTest { get; }
        public (int, int) TestMonkeys { get; }
        public int TotalItemsChecked { get; private set; }

        public void UpdateItems()
        {
            if (Operation.Contains('+'))
                foreach (var item in Items)
                    item.UpdateDivisibleBy(Operator.Add, Convert.ToInt32(Operation.Split(" ").Last()));
            else if (Operation == "old * old")
                foreach (var item in Items)
                    item.UpdateDivisibleBy(Operator.Square);
            else
                foreach (var item in Items)
                    item.UpdateDivisibleBy(Operator.Multiply, Convert.ToInt32(Operation.Split(" ").Last()));
            TotalItemsChecked += Items.Count;
        }

        public Dictionary<int, List<Item>> GetReceiversAndItems()
        {
            var result = new Dictionary<int, List<Item>>
            {
                [TestMonkeys.Item1] = new(),
                [TestMonkeys.Item2] = new()
            };
            Items.ForEach(item =>
            {
                if (item.DivisibleBy[DivisionTest] == 0)
                    result[TestMonkeys.Item1].Add(item);
                else
                    result[TestMonkeys.Item2].Add(item);
            });
            Items = new List<Item>();
            return result;
        }

        public void ReceiveItems(List<Item> items)
        {
            Items.AddRange(items);
        }
    }

    private class Item
    {
        public Item(int value)
        {
            Value = value;
            foreach (var i in new[] { 2, 3, 5, 7, 11, 13, 17, 19, 23 })
                DivisibleBy.Add(i, value % i);
        }

        public int Value { get; set; }
        public Dictionary<int, int> DivisibleBy { get; } = new();

        public void UpdateDivisibleBy(Operator op, int value = default)
        {
            foreach (var key in DivisibleBy.Keys)
                DivisibleBy[key] = op switch
                {
                    Operator.Add => (DivisibleBy[key] + value) % key,
                    Operator.Multiply => DivisibleBy[key] * value % key,
                    Operator.Square => DivisibleBy[key] * DivisibleBy[key] % key,
                    _ => throw new ArgumentOutOfRangeException(nameof(op), op, null)
                };
        }
    }

    private enum Operator
    {
        Add,
        Multiply,
        Square
    }
}