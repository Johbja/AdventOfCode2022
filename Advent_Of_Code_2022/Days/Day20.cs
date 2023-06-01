namespace Advent_Of_Code_2022.Days;

public class Day20 : Solution
{
    private List<(int number, int index)> numberSequence;

    public Day20(string path, Type instanceType, bool render) : base(path, instanceType, render)
    {
        numberSequence = input.Select((n, i) => (number: int.Parse(n), index: i)).ToList();
    }
    protected override void SolvePartOne()
    {
        RunProtectedAction(() =>
        {
            List<int> outputNumbers = new();

            for (int i = 0; i <= 3000; i++)
            {
                if (i == 999 || i == 1999 || i == 2999)
                {
                    var numberIndex = (numberSequence.FindIndex(x => x.number == 0) + 1) % numberSequence.Count;
                    outputNumbers.Add(numberSequence[numberIndex].number);
                }

                var currentIndex = i % numberSequence.Count;
                var currentItem = numberSequence.FindIndex(x => x.index == currentIndex);

                var targetIndex = (currentItem + numberSequence[currentItem].number);

                if (targetIndex == 0)
                    targetIndex--;

                if (targetIndex >= numberSequence.Count)
                    targetIndex = targetIndex % numberSequence.Count;

                if (targetIndex < 0)
                    targetIndex = numberSequence.Count + targetIndex;

                while (currentItem != targetIndex)
                {
                    if (currentItem < targetIndex)
                    {
                        var itemToMove = numberSequence[currentItem + 1];
                        var item = numberSequence[currentItem];

                        numberSequence[currentItem + 1] = item;
                        numberSequence[currentItem] = itemToMove;

                        currentItem++;
                    }
                    else
                    {
                        var itemToMove = numberSequence[currentItem - 1];
                        var item = numberSequence[currentItem];

                        numberSequence[currentItem - 1] = item;
                        numberSequence[currentItem] = itemToMove;

                        currentItem--;
                    }
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
}
