namespace Solutions;

class Solution1
{

  public System.Collections.Generic.List<int> ReturnListOfElves(string[] input)
  {
    List<int> totals = new();
    int elf = 0;
    foreach (string row in input)
    {
      bool isNumber = int.TryParse(row, out var value);
      if (isNumber)
      {
        elf += value;
      }
      else if (elf > 0)
      {
        totals.Add(elf);
        elf = 0;
      }
    }
    return totals;
  }
}