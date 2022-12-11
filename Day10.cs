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
    [InlineData(RealInput, 234)]
    public void Test1(string input, int expectedResult)
    {
        var commands = input.Split(Environment.NewLine);
        var cycle = 1;
        var register = 1;
        var results = new List<int>();
        for (int i = 0; i < commands.Length; i++)
        {
            string command = commands[i];
            // if (i > 1 && commands[i - 1].StartsWith('n') && (cycle - 20) % 40 == 0)
            if ((cycle - 20) % 40 == 0 && i > 1)
            {
                results.Add(register * cycle);
            }
            if (command.StartsWith('n'))
            {
                cycle++;
                continue;
            }

            if ((cycle - 19) % 40 == 0 && i > 1)
            {
                results.Add(register * (cycle + 1));
            }

            register += int.Parse(command.Split(' ')[1]);
            cycle += 2;
        }

        Console.WriteLine(string.Join(Environment.NewLine, results));

        Assert.Equal(expectedResult, results.Sum());
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
    // [InlineData(RealInput, "DEF")]
    public void Test2(string input, string expectedResult)
    {
        var commands = input.Split(Environment.NewLine)
            .Aggregate((result: new int[241], cycle: 1, register: 1), (r, l) =>
            {
                if (l.StartsWith('n'))
                {
                    r.cycle++;
                    return r;
                }
                else
                {
                    r.result[r.cycle + 1] = r.register;
                    r.register += int.Parse(l.Split(' ')[1]);
                    r.cycle += 2;
                }

                r.result[r.cycle - 1] = r.register;

                return r;
            });
        var screen = Enumerable.Range(0, 6).Select(_ => new bool[40]).ToArray();

        var result = string.Join(Environment.NewLine, screen.Select(l =>
                string.Join("", l.Select(c => c ? '#' : '.'))));

        Assert.Equal(expectedResult, result);
    }

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