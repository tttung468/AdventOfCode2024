using Common;

namespace Day06;

public static class Helper {
  public const char UpDirectionCh = '^';
  public const char DownDirectionCh = 'v';
  public const char LeftDirectionCh = '<';
  public const char RightDirectionCh = '>';
  public const char ObstructionCh = '#';
  public const char VisitedCh = 'X';

  public static (Position startingPosition, char[,] map) ReadMapFromFile() {
    var lines = InputHelper.ReadInputFile(InputFileName.Input06);
    int rows = lines.Length;
    int cols = lines[0].Length;
    var map = new char[rows, cols];
    var startingPos = new Position();

    for (int i = 0; i < rows; i++) {
      for (int j = 0; j < cols; j++) {
        map[i, j] = lines[i][j];

        if (map[i, j] == UpDirectionCh) {
          startingPos = new Position(i, j);
        }
      }
    }

    return (startingPos, map);
  }

  public static void PrintMap(char[,] map) {
    for (int i = 0; i < map.GetLength(0); i++) {
      for (int j = 0; j < map.GetLength(1); j++) {
        Console.Write(map[i, j] + " ");
      }
      Console.WriteLine("");
    }
    Console.WriteLine("");
  }

  public static char[,] CopyMap(char[,] map) {
    char[,] copy = new char[map.GetLength(0), map.GetLength(1)];
    for (int i = 0; i < map.GetLength(0); i++) {
      for (int j = 0; j < map.GetLength(1); j++) {
        copy[i, j] = map[i, j];
      }
    }
    return copy;
  }
}
