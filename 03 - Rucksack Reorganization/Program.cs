string[] input = File.ReadAllLines("input.txt");

var solver = new RucksackSolver(input);
Console.WriteLine("Total value duplicates: " + solver.GetTotalScoreOfIndividualRucksackItems());

Console.WriteLine("Total value badges: " + solver.GetTotalScoreOfTeamBadges()); // Should be lower than 3200

class RucksackSolver
{
    readonly string[] Input;
    readonly List<char> LetterValues = new();

    public RucksackSolver(string[] input)
    {
        Input = input;
        SetLetterValues();
    }

    public int GetTotalScoreOfIndividualRucksackItems()
    {
        char[] duplicates = GetAllDuplicates();
        Console.WriteLine("Duplicates found: " + duplicates.Length);

        int totalPriorityValues = 0;
        foreach (char d in duplicates)
        {
            totalPriorityValues += GetPriorityByLetter(d);
        }
        return totalPriorityValues;
    }

    public int GetTotalScoreOfTeamBadges()
    {
        List<char> badges = GetAllBadges();
        Console.WriteLine("Total badges found: " + badges.Count);

        int totalPriorityValue = 0;
        foreach (char badge in badges)
        {
            totalPriorityValue += GetPriorityByLetter(badge);
        }
        return totalPriorityValue;
    }

    List<char> GetAllBadges()
    {
        List<string[]> groups = SplitIntoGroups(3);
        return IdentifyBadgesFromGroups(groups);
    }

    List<char> IdentifyBadgesFromGroups(List<string[]> groups)
    {
        List<char> badges = new();

        foreach (var group in groups)
        {
            badges.Add(IdentifyBadgeFromGroup(group));
        }

        return badges;
    }

    char IdentifyBadgeFromGroup(string[] group)
    {
        int groupSize = group.Length;

        List<List<char>> membersWithDistinctItems = new();
        foreach (string member in group)
        {
            List<char> distinct = member.ToCharArray().Distinct().ToList();
            membersWithDistinctItems.Add(distinct);
        }

        List<char> stackedDistinctMemberItems = new();
        foreach (List<char> member in membersWithDistinctItems)
        {
            foreach (char item in member)
            {
                stackedDistinctMemberItems.Add(item);
            }
        }

        foreach (char stackItem in stackedDistinctMemberItems.Distinct())
        {
            var items = stackedDistinctMemberItems.FindAll(item => item == stackItem);
            if (items.Count() == groupSize)
            {
                return stackItem;
            }
        }

        throw new Exception("No badge has been found");
    }

    List<string[]> SplitIntoGroups(int groupSize)
    {
        List<string[]> groups = new List<string[]>();
        for (int i = 0; i < Input.Length; i += groupSize)
        {
            List<string> groupMembers = new();
            for (int j = i; j < i + groupSize; j++)
            {
                groupMembers.Add(Input[j]);
            }
            groups.Add(groupMembers.ToArray());
        }

        return groups;
    }

    char[] GetAllDuplicates()
    {
        List<char> duplicates = new();
        foreach (var rucksack in Input)
        {
            var (compartment1, compartment2) = SplitAndConvertRucksack(rucksack);
            var rucksackDuplicates = IdentifyDuplicates(compartment1, compartment2);
            duplicates.AddRange(rucksackDuplicates);
        }

        return duplicates.ToArray();
    }

    (List<char> compartment1, List<char> compartment2) SplitAndConvertRucksack(string rucksack)
    {
        List<char> compartment1 = rucksack.ToCharArray().Take((rucksack.Length / 2)).ToList();
        List<char> compartment2 = rucksack.ToCharArray().TakeLast((rucksack.Length / 2)).ToList();

        return (compartment1, compartment2);
    }

    List<char> IdentifyDuplicates(List<char> compartment1, List<char> compartment2)
    {
        List<char> duplicates = new();
        foreach (char c in compartment1.Distinct())
        {
            if (compartment2.Contains(c))
            {
                duplicates.Add(c);
            }
        }

        return duplicates;
    }

    int GetPriorityByLetter(char letter)
    {
        int value = LetterValues.FindIndex(_ => _ == letter) + 1;
        return value;
    }

    // Setup related functions
    void SetLetterValues()
    {
        char[] letters = ("abcdefghijklmnopqrstuvwxyz").ToCharArray();
        foreach (char letter in letters)
        {
            LetterValues.Add(letter);
        }
        foreach (char letter in letters)
        {
            LetterValues.Add(letter.ToString().ToUpper().ToCharArray().First());
        }
    }
}