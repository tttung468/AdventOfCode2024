namespace Day11;
public static class Part01 {

  public static void Execute(List<long> stones) {
    var newStones = SplitTheStones(stones, Helper.MaxBlinkTime_Part1);
    Console.WriteLine($"Count stones: {newStones.Count}");
  }

  public static List<long> SplitTheStones(List<long> stones, int blinksTime) {
    List<long> newStones = [];
    List<List<long>> splitedNewStones = [];

    for (int i = 1; i <= blinksTime; i++) {
      Parallel.For(0, stones.Count, i => {
        lock (newStones) {
          SplitTheStone(stones[i], newStones);
        }
      });

      // Console.WriteLine($"blink :{i}");

      stones = [.. newStones];
      newStones = new(stones.Count * 2);


      // PrintStones(stones, i);
    }

    return stones;
  }

  public static void SplitTheStone(long stone, List<long> newStones) {
    var stoneStr = stone.ToString();

    if (stone == Helper.Stone0) {
      newStones.Add(Helper.Stone1);
    } else if (Helper.IsEvenNumberOfDigits(stoneStr)) {
      var (leftNumber, rightNumber) = Helper.SplitTheStoneWithEvenOfDigits(stoneStr);
      newStones.Add(leftNumber);
      newStones.Add(rightNumber);
    } else {
      newStones.Add(stone * Helper.Stone2024);
    }
  }

  public static void PrintStones(List<long> stones, int blinkTime) {
    Console.WriteLine($"\n\nAfter {blinkTime} blink");
    foreach (var item in stones) {
      Console.Write($"{item} ");
    }
  }
}
