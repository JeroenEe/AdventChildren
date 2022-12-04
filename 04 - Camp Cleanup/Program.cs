string[] input = File.ReadAllLines("input.txt");

var solver = new CleanerApp(input);

Console.WriteLine("Amount of pairs expected: " + input.Length);
Console.WriteLine("Total overlaps withing pairs: " + solver.GetOverlapsPerPair());
Console.WriteLine("Pairs that have any overlap: " + solver.GetPartialOverlaps());

class CleanerApp
{
    string[] Input;
    List<Pair> Pairs = new();
    public CleanerApp(string[] input)
    {
        Input = input;
        Setup();
        Console.WriteLine("Amount of pairs processed: " + Pairs.Count);
    }

    public int GetOverlapsPerPair()
    {
        int total = 0;
        foreach (var pair in Pairs)
        {
            if (pair.HasCompleteOverlap())
            {
                total++;
            }
        }
        return total;
    }

    public int GetPartialOverlaps()
    {
        int total = 0;
        foreach (var pair in Pairs)
        {
            if (pair.HasPartialOverlap())
            {
                total++;
            }
        }
        return total;
    }

    // ----

    void Setup()
    {
        ProcessInput();
    }

    void ProcessInput()
    {
        foreach (var pair in Input)
        {
            var elves = pair.Split(",");
            int[][] elvesFullValues = new int[elves.Length][];

            for (var e = 0; e < elves.Length; e++)
            {
                string[] values = elves[e].Split("-");
                List<int> fullValues = new();
                int start = int.Parse(values.First());
                int end = int.Parse(values.Last());

                int diff = end - start;
                for (var i = 0; i <= diff; i++)
                {
                    fullValues.Add(start + i);
                }

                elvesFullValues[e] = fullValues.ToArray();
            }

            Pairs.Add(new Pair(elvesFullValues[0], elvesFullValues[1])); // Not sure how to do dynamically without index
        }
    }

    public class Pair
    {
        public (IEnumerable<int>, IEnumerable<int>) Values { get; private set; }

        public Pair(IEnumerable<int> first, IEnumerable<int> second)
        {
            Values = (first, second);
        }

        public bool HasCompleteOverlap()
        {
            var (first, second) = Values;

            var firstMatches = true;
            foreach (var number in first)
            {
                if (!second.Contains(number))
                {
                    firstMatches = false;
                }
            }

            var secondMatches = true;
            foreach (var number in second)
            {
                if (!first.Contains(number))
                {
                    secondMatches = false;
                }
            }

            return firstMatches || secondMatches;
        }

        public bool HasPartialOverlap()
        {
            var (first, second) = Values;

            var matches = false;
            foreach (var number in first)
            {
                if (second.Contains(number))
                {
                    matches = true;
                }
            }

            return matches;
        }
    }
}
