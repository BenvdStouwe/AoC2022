using System.Diagnostics;

namespace AoC2022;

public class Day11
{
    private const string TestInput =
        """
        Monkey 0:
          Starting items: 79, 98
          Operation: new = old * 19
          Test: divisible by 23
            If true: throw to monkey 2
            If false: throw to monkey 3

        Monkey 1:
          Starting items: 54, 65, 75, 74
          Operation: new = old + 6
          Test: divisible by 19
            If true: throw to monkey 2
            If false: throw to monkey 0

        Monkey 2:
          Starting items: 79, 60, 97
          Operation: new = old * old
          Test: divisible by 13
            If true: throw to monkey 1
            If false: throw to monkey 3

        Monkey 3:
          Starting items: 74
          Operation: new = old + 3
          Test: divisible by 17
            If true: throw to monkey 0
            If false: throw to monkey 1
        """;

    [Theory]
    [InlineData(TestInput, 10605)]
    [InlineData(RealInput, 58056)]
    public void Part1(string input, int expectedResult)
    {
        var monkeys = ParseMonkeys(input);

        var result = Play(monkeys, 20, true);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(TestInput, 2713310158)]
    [InlineData(RealInput, 15048718170)]
    public void Part2(string input, long expectedResult)
    {
        var monkeys = ParseMonkeys(input);

        var result = Play(monkeys, 10000, false);

        Assert.Equal(expectedResult, result);
    }

    private static long Play(Monkey[] monkeys, int rounds, bool useWorryDivider)
    {
        Func<long, long> worryOperator = useWorryDivider
            ? worry => worry / 3
            : worry => worry % monkeys.Aggregate(1, (d, m) => d * m.Divider);
        return Enumerable.Range(0, rounds).Aggregate(monkeys, (m, _) =>
            {
                foreach (var monkey in m)
                {
                    while (monkey.Items.TryDequeue(out var item))
                    {
                        var worryLevel = worryOperator(monkey.DetermineWorryLevel(item));
                        var targetMonkey = monkey.TargetSelector(worryLevel);
                        m[targetMonkey].Items.Enqueue(worryLevel);
                        monkey.InspectedItems++;
                    }
                }

                return m;
            })
            .OrderByDescending(m => m.InspectedItems)
            .Take(2).Aggregate(1L, (a, b) => a * b.InspectedItems);
    }

    private static Monkey[] ParseMonkeys(string input) =>
        input.Split(Environment.NewLine + Environment.NewLine)
            .Select(m => m.Split(Environment.NewLine))
            .Select(m => new Monkey
            {
                Divider = short.Parse(m[3].Split("by ")[1]),
                Items = new Queue<long>(ParseItems(m[1])),
                DetermineWorryLevel = ParseOperation(m[2].Split('=')[1]),
                TargetSelector = ParseAction(m[3..6])
            })
            .ToArray();

    private record Monkey
    {
        public int InspectedItems { get; set; }
        public required short Divider { get; init; }
        public required Queue<long> Items { get; init; }
        public required Func<long, long> DetermineWorryLevel { get; init; }
        public required Func<long, short> TargetSelector { get; init; }
    }

    private static Func<long, short> ParseAction(string[] action)
    {
        var monkeyIfTrue = short.Parse(action[1][^1].ToString());
        var monkeyIfFalse = short.Parse(action[2][^1].ToString());
        var divideBy = short.Parse(action[0].Split("by ")[1]);
        return val => val % divideBy == 0
            ? monkeyIfTrue
            : monkeyIfFalse;
    }

    private static IEnumerable<long> ParseItems(string items) => items.Split(':')[1].Split(',').Select(long.Parse);

    private static Func<long, long> ParseOperation(string s)
    {
        var strings = s.Split(' ');
        Func<long, long, long> operation = strings[2][0] switch
        {
            '+' => (old, other) => old + other,
            '*' => (old, other) => old * other,
            _ => throw new UnreachableException()
        };

        return strings[3] == "old"
            ? old => operation(old, old)
            : short.TryParse(strings[3], out var val)
                ? old => operation(old, val)
                : throw new UnreachableException();
    }

    private const string RealInput = """
    Monkey 0:
      Starting items: 72, 97
      Operation: new = old * 13
      Test: divisible by 19
        If true: throw to monkey 5
        If false: throw to monkey 6

    Monkey 1:
      Starting items: 55, 70, 90, 74, 95
      Operation: new = old * old
      Test: divisible by 7
        If true: throw to monkey 5
        If false: throw to monkey 0

    Monkey 2:
      Starting items: 74, 97, 66, 57
      Operation: new = old + 6
      Test: divisible by 17
        If true: throw to monkey 1
        If false: throw to monkey 0

    Monkey 3:
      Starting items: 86, 54, 53
      Operation: new = old + 2
      Test: divisible by 13
        If true: throw to monkey 1
        If false: throw to monkey 2

    Monkey 4:
      Starting items: 50, 65, 78, 50, 62, 99
      Operation: new = old + 3
      Test: divisible by 11
        If true: throw to monkey 3
        If false: throw to monkey 7

    Monkey 5:
      Starting items: 90
      Operation: new = old + 4
      Test: divisible by 2
        If true: throw to monkey 4
        If false: throw to monkey 6

    Monkey 6:
      Starting items: 88, 92, 63, 94, 96, 82, 53, 53
      Operation: new = old + 8
      Test: divisible by 5
        If true: throw to monkey 4
        If false: throw to monkey 7

    Monkey 7:
      Starting items: 70, 60, 71, 69, 77, 70, 98
      Operation: new = old * 7
      Test: divisible by 3
        If true: throw to monkey 2
        If false: throw to monkey 3
    """;
}