using AdventOfCode.Utils.Extensions;

namespace AdventOfCode.Year2022;

public class Day11
{
    private static readonly List<Monkey> Part1Monkeys = new();
    private static readonly List<Monkey> Part2Monkeys = new();

    public Day11()
    {
        Console.WriteLine("Day11 Solution");
        
        File.ReadAllText("inputs/day11.txt").Split("\n\n").ToList().ForEach(monkey =>
        {
            string[] lines = monkey.Split("\n");
            
            // Getting starting items
            List<Item> items1 = new List<Item>();
            List<Item> items2 = new List<Item>();
            lines[1][(lines[1].IndexOf(':') + 2)..].Split(", ").ToList().ForEach(item =>
            {
                items1.Add(new(Convert.ToInt32(item)));
                items2.Add(new(Convert.ToInt32(item)));
            });
            
            // Getting operation
            string op = lines[2][19..];
            
            // Getting test info
            int test = Convert.ToInt32(lines[3][21..]);
            (int, int) testMonkeys = (Convert.ToInt32(lines[4][29..]), Convert.ToInt32(lines[5][30..]));

            Part1Monkeys.Add(new Monkey(items1, op, test, testMonkeys));
            Part2Monkeys.Add(new Monkey(items2, op, test, testMonkeys));
        });
        
        Console.WriteLine($"Part1 Result: {Part1()}");
        Console.WriteLine($"Part2 Result: {Part2()}");
        Console.WriteLine("\n=============================\n");
    }

    private static ulong Part1()
    {
        ulong[] inspections = new ulong[Part1Monkeys.Count];
        
        for (int round = 0; round < 20; round++)
            foreach (Monkey monkey in Part1Monkeys)
            {
                foreach (Item item in monkey.Items)
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
                Dictionary<int, List<Item>> receiversAndItems = monkey.GetReceiversAndItems();
                foreach (int key in receiversAndItems.Keys)
                    Part2Monkeys[key].ReceiveItems(receiversAndItems[key]);
            }
        List<ulong> topMonkeys = new List<ulong>();
        Part2Monkeys.ForEach(monkey => topMonkeys.Add((ulong) monkey.TotalItemsChecked));
        return topMonkeys.ProductOfMax(2);
    }

    private class Monkey
    {
        public List<Item> Items { get; set; }
        public string Operation { get; }
        public int DivisionTest { get; }
        public (int, int) TestMonkeys { get; }
        public int TotalItemsChecked { get; private set; }

        public Monkey(List<Item> items, string operation, int divisionTest, (int, int) testMonkeys)
        {
            Items = items;
            Operation = operation;
            DivisionTest = divisionTest;
            TestMonkeys = testMonkeys;
            TotalItemsChecked = 0;
        }

        public void UpdateItems()
        {
            if (Operation.Contains('+'))
                foreach (Item item in Items)
                    item.UpdateDivisibleBy(Operator.Add, Convert.ToInt32(Operation.Split(" ").Last()));
            else if (Operation == "old * old")
                foreach (Item item in Items)
                    item.UpdateDivisibleBy(Operator.Square);
            else
                foreach (Item item in Items)
                    item.UpdateDivisibleBy(Operator.Multiply, Convert.ToInt32(Operation.Split(" ").Last()));
            TotalItemsChecked += Items.Count;
        }

        public Dictionary<int, List<Item>> GetReceiversAndItems()
        {
            Dictionary<int, List<Item>> result = new Dictionary<int, List<Item>>
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
        public int Value { get; set; }
        public Dictionary<int, int> DivisibleBy { get; } = new();

        public Item(int value)
        {
            Value = value;
            foreach (int i in new[] { 2, 3, 5, 7, 11, 13, 17, 19, 23 })
                DivisibleBy.Add(i, value % i);
        }

        public void UpdateDivisibleBy(Operator op, int value = default)
        {
            foreach (int key in DivisibleBy.Keys)
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
        Add, Multiply, Square
    }
}