using System.Collections;

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
        //0000000000
        //0000000000
        //-------------------------        
        //1 0000000 0000100 0000100
        //1 0011100 0000100 0000.00
        //1 0000100 0000.00 0000.00
        //0000001000
        //0000000000
        //  -----------------------              

        //1 0010000
        //1 0010000
        //1 0010000
        //1 0010000
        //----------------

        //1 0011000
        //1 0011000

        jetPattern = input.SelectMany(s => s.Select(c => c).ToArray()).ToArray();

    }

    public enum RockShape
    {
        Line = 0,
        Cross,
        Shelf,
        Edge,
        Square
    }

    public class FallingRock
    {
        public RockShape Shape { get; private set; }
        public (int x, int y) CurrentPosition { get; set; }
        public (int x, int y)[] RockPositionFormation { get; private set; }

        public FallingRock((int x, int y) currentPosition, RockShape shape = RockShape.Line)
        {
            CurrentPosition = currentPosition;
            Shape = shape;

            switch (shape)
            {
                case RockShape.Line:
                    RockPositionFormation = new (int x, int y)[4] { (0, 0), (1, 0), (2, 0), (3, 0), };
                    break;
                case RockShape.Cross:
                    RockPositionFormation = new (int x, int y)[5] { (0, 0), (1, 0), (1, -1), (1, 1), (2, 0) };
                    CurrentPosition = (CurrentPosition.x, CurrentPosition.y + 1);
                    break;
                case RockShape.Shelf:
                    RockPositionFormation = new (int x, int y)[5] { (0, 0), (1, 0), (2, 0), (2, -1), (2, -2) };
                    CurrentPosition = (CurrentPosition.x, CurrentPosition.y + 2);
                    break;
                case RockShape.Edge:
                    RockPositionFormation = new (int x, int y)[4] { (0, 0), (0, -1), (0, -2), (0, -3) };
                    CurrentPosition = (CurrentPosition.x, CurrentPosition.y + 3);
                    break;
                case RockShape.Square:
                    RockPositionFormation = new (int x, int y)[4] { (0, 0), (0, 1), (-1, 0), (1, -1) };
                    CurrentPosition = (CurrentPosition.x, CurrentPosition.y + 1);
                    break;
            }
        }

        public int GetHighestPosition()
        {
            return GetPositions().Max(pos => pos.y);
        }

        public int GetLowestPosition()
        {
            return GetPositions().Min(pos => pos.y);
        }

        public int GetLowestXPosition()
        {
            return GetPositions().Min(pos => pos.x);
        }

        public int GetHighestXPosition()
        {
            return GetPositions().Max(pos => pos.x);
        }

        public IEnumerable<(int x, int y)> GetPositions()
        {
            return RockPositionFormation.Select(pos => (x: CurrentPosition.x + pos.x, y: CurrentPosition.y + pos.y));
        }

    }

    protected override void SolvePartOne()
    {
        RunProtectedAction(() =>
        {
            List<FallingRock> rocks = new();
            RockShape currentShape = RockShape.Line;
            (int x, int y) currentPos = (3,4);
            int instructionPointer = 0;

            for(int i = 0; i < 2023; i++)
            {
                var currentRock = new FallingRock(currentPos, currentShape);
                var collitionPositions = rocks.SelectMany(x => x.GetPositions());
                (int x, int y) positionBeforeMove = (0,0);
                bool fall = false;

                while (positionBeforeMove != currentRock.CurrentPosition)
                {
                    positionBeforeMove = currentRock.CurrentPosition;

                    var currentPositions = currentRock.GetPositions();
                    
                    var leftMovement = currentRock.GetLowestXPosition() - 1;
                    var allLeftMovement = currentPositions.Select(pos => (pos.x - 1, pos.y));
                    
                    var rigthMovement = currentRock.GetHighestXPosition() + 1;
                    var allRightMovement = currentPositions.Select(pos => (pos.x + 1, pos.y));
                    
                    var downMovement =  currentRock.GetLowestPosition() - 1;
                    var allDownMovement = currentPositions.Select(pos => (pos.x, pos.y - 1));

                    if (!fall)
                    {
                        if (jetPattern[instructionPointer] == '>' && rigthMovement <= 7 && collitionPositions.All(cp => !allRightMovement.Contains(cp)))
                            currentRock.CurrentPosition = (currentRock.CurrentPosition.x + 1, currentRock.CurrentPosition.y);
                        else if (jetPattern[instructionPointer] == '<' && leftMovement > 0 && collitionPositions.All(cp => !allLeftMovement.Contains(cp)))
                            currentRock.CurrentPosition = (currentRock.CurrentPosition.x - 1, currentRock.CurrentPosition.y);

                        instructionPointer = (instructionPointer + 1) % jetPattern.Length;
                        fall = true;
                    }
                    else
                    {
                        if (downMovement >= 0 && collitionPositions.All(cp => !allDownMovement.Contains(cp)))
                            currentRock.CurrentPosition = (currentRock.CurrentPosition.x, currentRock.CurrentPosition.y - 1);

                        fall = false;
                    }
                }

                rocks.Add(currentRock);
                currentShape = (RockShape)(((int)currentShape + 1) % 5);
                currentPos = (currentPos.x, currentRock.GetHighestPosition() + 5);
            }

            var allPositions = rocks.SelectMany(x => x.GetPositions());
            int lastY = rocks.Max(x => x.GetHighestPosition());
            int xTest = allPositions.Max(x => x.x);
            List<string> outputrows = Enumerable.Range(0, lastY+1).Select(x => "|.......|").ToList();
            var lent = outputrows.Max(x => x.Length);
            foreach (var position in allPositions)
            {
                if(outputrows.Count > position.y)
                {
                    string currentRow = outputrows[position.y];
                    var currentArrayRow = currentRow.ToArray();
                    currentArrayRow[position.x] = '#';
                    outputrows[position.y] = new string(currentArrayRow);
                }
            }
            outputrows.Reverse();
            StoreAnswerPartOne(answers:outputrows);
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
