namespace Day10;

public static class Helper {
  public const int StartingValue = 0;
  public const int EndingValue = 9;

  public static List<(int startX, int startY)> FindAllLocations(int[,] matrix, int valueInLocation) {
    List<(int, int)> locations = [];

    for (int i = 0; i < matrix.GetLength(0); i++) {
      for (int j = 0; j < matrix.GetLength(1); j++) {
        if (matrix[i, j] == valueInLocation) {
          locations.Add(new(i, j));
        }
      }
    }

    return locations;
  }

  public static int SumOfMaxDistances() {
    var sum = 0;
    for (int i = 1; i <= 9; i++) {
      sum += i;
    }
    return sum;
  }
}