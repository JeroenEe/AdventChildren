string[] input = File.ReadAllLines("input.txt");

Console.WriteLine(new Solutions.Solution1().GetTotalWinScore(input));
Console.WriteLine(new Solutions.Solution1().GetTotalWinByStrategy(input));