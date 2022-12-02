namespace Solutions;

enum RockPaperScissors
{
  Rock = 1,
  Paper = 2,
  Scissors = 3
}

enum LossDrawWin
{
  Loss = 0,
  Draw = 3,
  Win = 6
}

class Solution1
{
  Dictionary<string, RockPaperScissors> RockPaperScissorsDict = new() {
    { "A", RockPaperScissors.Rock },
    { "B", RockPaperScissors.Paper },
    { "C", RockPaperScissors.Scissors },
    { "X", RockPaperScissors.Rock },
    { "Y", RockPaperScissors.Paper },
    { "Z", RockPaperScissors.Scissors },
  };

  Dictionary<string, LossDrawWin> OutcomeDictionary = new() {
    { "X", LossDrawWin.Loss },
    { "Y", LossDrawWin.Draw },
    { "Z", LossDrawWin.Win },
  };

  Dictionary<RockPaperScissors, RockPaperScissors> WinConditions = new() {
    { RockPaperScissors.Rock, RockPaperScissors.Scissors },
    { RockPaperScissors.Paper, RockPaperScissors.Rock },
    { RockPaperScissors.Scissors, RockPaperScissors.Paper },
  };

  public int GetTotalWinScore(string[] input)
  {
    var scoreList = CalculateWinsAndLosses(input);
    return GetTotalPlayerScore(scoreList);
  }

  public int GetTotalWinByStrategy(string[] input)
  {
    var scoreList = CalculateWinsAndLosses(input, true);
    return GetTotalPlayerScore(scoreList);
  }

  List<(LossDrawWin, RockPaperScissors)> CalculateWinsAndLosses(string[] input, bool byWinCondition = false)
  {
    var list = new List<(LossDrawWin, RockPaperScissors)>();
    foreach (var value in input)
    {
      string[] players = value.Split();
      var player1 = byWinCondition ?
      DetermineNextMove(RockPaperScissorsDict[players[0]], OutcomeDictionary[players[1]]) : players[1];

      list.Add((DeterminePlayerWinOrLoss((new string[] { players[0], player1 })), RockPaperScissorsDict[player1]));
    }
    return list;
  }

  LossDrawWin DeterminePlayerWinOrLoss(string[] players)
  {
    var opponent = RockPaperScissorsDict[players[0]];
    var player = RockPaperScissorsDict[players[1]];

    var opponentWin = WinConditions[opponent] == player;
    var playerWin = WinConditions[player] == opponent;

    if (!opponentWin && !playerWin)
    {
      return LossDrawWin.Draw;
    }

    return playerWin ? LossDrawWin.Win : LossDrawWin.Loss;
  }

  string DetermineNextMove(RockPaperScissors opponentChoice, LossDrawWin winCondition)
  {
    switch (winCondition)
    {
      case LossDrawWin.Loss:
        var loseCon = WinConditions.First(x => x.Key == opponentChoice).Value;
        return RockPaperScissorsDict.First(x => x.Value == loseCon).Key;

      case LossDrawWin.Draw:
        return RockPaperScissorsDict.First(x => x.Value == opponentChoice).Key;

      case LossDrawWin.Win:
        var winCon = WinConditions.First(x => x.Value == opponentChoice).Key;
        return RockPaperScissorsDict.First(x => x.Value == winCon).Key;
    }

    throw new Exception("Invalid winCondition");
  }

  int GetTotalPlayerScore(List<(LossDrawWin, RockPaperScissors)> list)
  {
    int totalScore = 0;
    foreach (var item in list)
    {
      totalScore += (int)item.Item1 + (int)item.Item2;
    }
    return totalScore;
  }
}
