using Common;

namespace Day11;

public static class Helper {
  public const int Stone0 = 0;
  public const int Stone1 = 1;
  public const int Stone2024 = 2024;
  public const int MaxBlinkTime_Part1 = 25;
  public const int MaxBlinkTime_Part2 = 75;

  public static List<long> ReadStonesFromFile() {
    var matrix = InputHelper.ReadInputFile_HavingIntColumns(InputFileName.Input11);
    List<long> stones = [];

    foreach (var item in matrix[0]) {
      stones.Add(item);
    }

    return stones;
  }

  public static bool IsEvenNumberOfDigits(string stoneStr) {
    return stoneStr.Length % 2 == 0;
  }

  public static (long leftNumber, long rightNumber) SplitTheStoneWithEvenOfDigits(string stoneStr) {
    var leftNumber = stoneStr[..(stoneStr.Length / 2)];
    var rightNumber = stoneStr[(stoneStr.Length / 2)..];
    return (int.Parse(leftNumber), int.Parse(rightNumber));
  }
}