namespace Day11 {
  public static class Part02_TrackingStoneCount {

    public static Dictionary<long, long> SplitTheStones(Dictionary<long, long> stones, int blinks) {
      for (int i = 1; i <= blinks; i++) {
        Dictionary<long, long> newStones = [];

        foreach (var stone in stones) {
          var stoneCount = stone.Value;
          var stoneStr = stone.Key.ToString();

          if (stone.Key == Helper.Stone0) {
            AddStoneCount(newStones, stoneCount, Helper.Stone1);
          } else if (Helper.IsEvenNumberOfDigits(stoneStr)) {
            var (left, right) = Helper.SplitTheStoneWithEvenOfDigits(stoneStr);
            AddStoneCount(newStones, stoneCount, left);
            AddStoneCount(newStones, stoneCount, right);
          } else {
            var newStone = stone.Key * Helper.Stone2024;
            AddStoneCount(newStones, stoneCount, newStone);
          }
        }

        stones = newStones;
      }

      return stones;
    }

    public static long CountTotalStones(Dictionary<long, long> stones) {
      long totalStones = 0;

      foreach (var stone in stones) {
        totalStones += stone.Value;
      }

      return totalStones;
    }

    public static void AddStoneCount(Dictionary<long, long> stones, long count, long stone) {
      if (stones.ContainsKey(stone)) {
        stones[stone] += count;
      } else {
        stones[stone] = count;
      }
    }
  }
}
