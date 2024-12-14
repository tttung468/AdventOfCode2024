using Common;

namespace Day08;
public static class Helper {
  public const char DotChar = '.';
  public const char AntinodeChar = '#';

  public static (char[,] map, Dictionary<char, List<Location>> antennasDictionary) ReadAntennasFromFile() {
    Dictionary<char, List<Location>> antennasDictionary = [];
    var map = InputHelper.ReadCharacterMatrix(InputFileName.Input08);

    for (int i = 0; i < map.GetLength(0); i++) {
      for (int j = 0; j < map.GetLength(1); j++) {
        if (map[i, j] != DotChar) {
          if (!antennasDictionary.TryGetValue(map[i, j], out _)) {
            antennasDictionary[map[i, j]] = [];
          }
          antennasDictionary[map[i, j]].Add(new Location(i, j));
        }
      }
    }

    return (map, antennasDictionary);
  }

  public static void PrintMapWithAntinodes(char[,] map, Dictionary<string, Location> uniqueAntinodes) {
    var copiedMap = InputHelper.CopyMatrix(map);

    foreach (var pair in uniqueAntinodes) {
      if (copiedMap[pair.Value.X, pair.Value.Y] == DotChar) {
        copiedMap[pair.Value.X, pair.Value.Y] = AntinodeChar;
      }
    }

    InputHelper.PrintMatrix(copiedMap);
  }

  public static int CountAntennas(char[,] map) {
    var count = 0;
    for (int i = 0; i < map.GetLength(0); i++) {
      for (int j = 0; j < map.GetLength(1); j++) {
        if (map[i, j] != DotChar) {
          count++;
        }
      }
    }

    return count;
  }
}