string[] input = File.ReadAllLines("input.txt");

var solution1_part1 = new Solutions.Solution1().ReturnListOfElves(input).Max();
Console.WriteLine(solution1_part1);

var solution1_part2 = new Solutions.Solution1().ReturnListOfElves(input).OrderByDescending(_ => _).Take(3).Sum();
Console.WriteLine(solution1_part2);

// new Solutions.Solution2().CheesyWriteLine(input);
// new Solutions.Solution2().LessCheesy();
new Solutions.Solution2().LessCheesyButSeparateElves();