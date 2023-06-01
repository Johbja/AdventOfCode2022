namespace Advent_Of_Code_2022.Days;

public class Day17 : Solution
{
    private char[] jetPattern;
    private List<byte[]> rocks;

    public Day17(string path, Type instanceType, bool render) : base(path, instanceType, render)
    {
        //1 0011110

        //1 0001000
        //1 0011100
        //1 0001000

        //1 0011100
        //1 0000100
        //1 0000100

        //1 0010000
        //1 0010000
        //1 0010000
        //1 0010000

        //1 0011000
        //1 0011000

        List<Rock> rocks1 = new();
        rocks1.Add(new Rock(new List<Func<int, int>[]>()
        {
            new Func<int, int>[4]
            {
                x => x - 1,
                x => x,
                x => x + 1,
                x => x + 2
            }
        }));
        rocks1.Add(new Rock(new List<Func<int, int>[]>()
        {
            new Func<int, int>[1]
            {
                x => x
            },
            new Func<int, int>[3]
            {
                x => x - 1,
                x => x,
                x => x +1
            },
            new Func<int, int>[1]
            {
                x => x
            },
        }));
        rocks1.Add(new Rock(new List<Func<int, int>[]>()
        {
            new Func<int, int>[3]
            {
                x => x - 1,
                x => x,
                x => x +1
            },
            new Func<int, int>[1]
            {
                x => x +1
            },
            new Func<int, int>[1]
            {
                x => x + 1
            },
        }));

        rocks = new();
        rocks.Add(new byte[1] { 30 });
        rocks.Add(new byte[3]
        {
            8,
            28,
            8
        });
        rocks.Add(new byte[3]
        {
            28,
            4,
            4
        });
        rocks.Add(new byte[4]
        {
            16,
            16,
            16,
            16
        });
        rocks.Add(new byte[2]
        {
            24,
            24
        });

        jetPattern = input.SelectMany(s => s.Select(c => c).ToArray()).ToArray();

    }

    protected override void SolvePartOne()
    {
        RunProtectedAction(() =>
        {
            List<byte> Shaft = new List<byte>() { 255, 0, 0, 0 };

            for (int i = 0; i < 2023; i++)
            {
                var currentRock = rocks[i % rocks.Count];
                Shaft = PlaceNewRock(Shaft, currentRock);
                int currentYPos = Shaft.Count - currentRock.Length;
                int rockLength = currentRock.Length - 1;

                bool moveRock = true;
                int instructionCounter = 0;
                while (moveRock)
                {
                    var currentJet = jetPattern[instructionCounter % jetPattern.Length];

                    Func<byte, byte> bitShiftOperation = currentJet == '>' ? x => (byte)(x >> 1) : x => (byte)(x << 1);

                    if (CheckIfRowCanShift(Shaft, currentYPos, rockLength, right: currentJet == '>'))
                    {
                        for (int y = currentYPos; y <= currentYPos + rockLength; y++)
                        {
                            Shaft[y] = bitShiftOperation(Shaft[y]);
                        }
                    }

                    if ((Shaft[currentYPos - 1] & Shaft[currentYPos]) != 0)
                    {
                        moveRock = false;
                        continue;
                    }

                    for (int y = currentYPos; y <= currentYPos + rockLength; y++)
                    {
                        Shaft[y - 1] = (byte)(Shaft[y] | Shaft[y - 1]);
                        Shaft[y] = 0;
                    }

                    currentYPos--;
                    instructionCounter++;
                }

            }


            StoreAnswerPartOne($"");
        });
    }

    protected override void SolvePartTwo()
    {
        RunProtectedAction(() =>
        {


            StoreAnswerPartTwo($"");
        });
    }

    private bool IsBitSet(byte b, int bit)
    {
        return (b & (1 << bit)) != 0;
    }

    private bool CheckIfRowCanShift(List<byte> shaft, int start, int length, bool right)
    {
        for (int y = start; y <= start + length; y++)
        {
            if ((right && IsBitSet(shaft[y], 0))
            || (!right && IsBitSet(shaft[y], 7)))
                return false;
        }

        return true;
    }

    private List<byte> PlaceNewRock(List<byte> currentShaft, byte[] currentRock)
    {
        int collideIndex = currentShaft.Count - 1;
        for (int i = currentShaft.Count - 1; i >= 0; i--)
        {
            collideIndex = i;

            if ((currentShaft[i] & 0) != 0)
                break;
        }

        return currentShaft.Take(collideIndex + 1).Concat(new List<byte>() { 0, 0, 0 }).Concat(currentRock.ToList()).ToList();

    }

}

public class Rock
{
    public List<Func<int, int>[]> Positions { get; private set; }

    public Rock(List<Func<int, int>[]> positions)
    {
        Positions = positions;
    }
}
