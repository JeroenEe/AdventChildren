namespace Solutions;

/**
* Dennis Becker
*/
class Solution2
{
  public void CheesyWriteLine(string[] input)
  {

    int max = 0, temp = 0;
    foreach (var l in input)
    {
      Console.WriteLine(max = (temp += (string.IsNullOrEmpty(l) ? -temp : int.Parse(l))) > max ? temp : max);
    }
  }

  public void LessCheesy()
  {
    int temp = 0;
    System.Collections.Generic.List<int> list = new(System.Linq.Enumerable.Select(System.IO.File.ReadAllLines("input.txt"), line => temp += string.IsNullOrEmpty(line) ? -temp : int.Parse(line)));
    System.Console.WriteLine(System.Linq.Enumerable.Sum(System.Linq.Enumerable.Take(System.Linq.Enumerable.OrderByDescending(list, _ => _), 3)));
  }

  public void LessCheesyButSeparateElves()
  {
    int temp = 0;
    System.Collections.Generic.List<int> list = new(System.IO.File.ReadAllLines("input.txt").Select(line => temp += string.IsNullOrEmpty(line) ? -temp : int.Parse(line)));
    list.OrderByDescending(_ => _).Take(3).ToList().ForEach(System.Console.WriteLine);
  }
}
