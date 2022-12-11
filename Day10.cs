using System.Diagnostics;

namespace AoC2022;

public class Day10
{
    private const string TestInput = @"addx 15
addx -11
addx 6
addx -3
addx 5
addx -1
addx -8
addx 13
addx 4
noop
addx -1
addx 5
addx -1
addx 5
addx -1
addx 5
addx -1
addx 5
addx -1
addx -35
addx 1
addx 24
addx -19
addx 1
addx 16
addx -11
noop
noop
addx 21
addx -15
noop
noop
addx -3
addx 9
addx 1
addx -3
addx 8
addx 1
addx 5
noop
noop
noop
noop
noop
addx -36
noop
addx 1
addx 7
noop
noop
noop
addx 2
addx 6
noop
noop
noop
noop
noop
addx 1
noop
noop
addx 7
addx 1
noop
addx -13
addx 13
addx 7
noop
addx 1
addx -33
noop
noop
noop
addx 2
noop
noop
noop
addx 8
noop
addx -1
addx 2
addx 1
noop
addx 17
addx -9
addx 1
addx 1
addx -3
addx 11
noop
noop
addx 1
noop
addx 1
noop
noop
addx -13
addx -19
addx 1
addx 3
addx 26
addx -30
addx 12
addx -1
addx 3
addx 1
noop
noop
noop
addx -9
addx 18
addx 1
addx 2
noop
noop
addx 9
noop
noop
noop
addx -1
addx 2
addx -37
addx 1
addx 3
noop
addx 15
addx -21
addx 22
addx -6
addx 1
noop
addx 2
addx 1
noop
addx -10
noop
noop
addx 20
addx 1
addx 2
addx 2
addx -6
addx -11
noop
noop
noop";

    [Theory]
    [InlineData(TestInput, 13140)]
    [InlineData(RealInput, 16880)]
    public void Test1(string input, int expectedResult)
    {
        int[] result = GetRegisterValues(input);

        var total = result[20] * 20;
        for (var x = 60; x < 241; x += 40)
        {
            total += result[x] * x;
        }

        Assert.Equal(expectedResult, total);
    }

    [Theory]
    [InlineData(TestInput,
    """
    ##..##..##..##..##..##..##..##..##..##..
    ###...###...###...###...###...###...###.
    ####....####....####....####....####....
    #####.....#####.....#####.....#####.....
    ######......######......######......####
    #######.......#######.......#######.....
    """)]
    [InlineData(RealInput, 
    """
    ###..#..#..##..####..##....##.###..###..
    #..#.#.#..#..#....#.#..#....#.#..#.#..#.
    #..#.##...#..#...#..#..#....#.###..#..#.
    ###..#.#..####..#...####....#.#..#.###..
    #.#..#.#..#..#.#....#..#.#..#.#..#.#.#..
    #..#.#..#.#..#.####.#..#..##..###..#..#.
    """)]
    public void Test2(string input, string expectedResult)
    {
        var register = GetRegisterValues(input);
        var screen = Enumerable.Range(0, 6).Select(_ => new bool?[40]).ToArray();

        for (var i = 0; i < register.Length - 1; i++)
        {
            var column = i % 40;
            int registerValue = register[i + 1];
            screen[(i - column) / 40][column] = registerValue - 1 <= column && column <= registerValue + 1;
        }

        var result = string.Join(Environment.NewLine, screen.Select(l =>
                string.Join("", l.Select(c => c is null ? '?' : c.Value ? '#' : '.'))));

        Assert.Equal(expectedResult, result);
    }

    private static int[] GetRegisterValues(string input) =>
        input.Split(Environment.NewLine)
            .Aggregate((register: new int[241], cycle: 0, registerValue: 1), (a, l) =>
            {
                a.cycle += 1;
                a.register[a.cycle] = a.registerValue;
                if (!l.StartsWith('n'))
                {
                    a.cycle += 1;
                    a.register[a.cycle] = a.registerValue;
                    a.registerValue += short.Parse(l.Split(' ')[1]);
                }

                return a;
            }).register;

    private const string RealInput = @"noop
noop
addx 5
addx 3
noop
addx 14
addx -12
noop
addx 5
addx 1
noop
addx 19
addx -15
noop
noop
noop
addx 7
addx -1
addx 4
noop
noop
addx 5
addx 1
addx -38
noop
addx 21
addx -18
addx 2
addx 2
noop
addx 3
addx 5
addx -6
addx 11
noop
addx 2
addx 19
addx -18
noop
addx 8
addx -3
addx 2
addx 5
addx 2
addx 3
addx -2
addx -38
noop
addx 3
addx 4
addx 5
noop
addx -2
addx 5
addx -8
addx 12
addx 3
addx -2
addx 5
addx 11
addx -31
addx 23
addx 4
noop
noop
addx 5
addx 3
addx -2
addx -37
addx 1
addx 5
addx 2
addx 12
addx -10
addx 3
addx 4
addx -2
noop
addx 6
addx 1
noop
noop
noop
addx -2
addx 7
addx 2
noop
addx 3
addx 3
addx 1
noop
addx -37
addx 2
addx 5
addx 2
addx 32
addx -31
addx 5
addx 2
addx 9
addx 9
addx -15
noop
addx 3
addx 2
addx 5
addx 2
addx 3
addx -2
addx 2
addx 2
addx -37
addx 5
addx -2
addx 2
addx 5
addx 2
addx 16
addx -15
addx 4
noop
addx 1
addx 2
noop
addx 3
addx 5
addx -1
addx 5
noop
noop
noop
noop
addx 3
addx 5
addx -16
noop";
}