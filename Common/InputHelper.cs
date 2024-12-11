namespace Common;

public static class InputHelper {

  public static List<List<int>> ReadInputFile_HavingIntColumns(string fileName) {
    string[] lines = ReadInputFile(fileName);
    List<List<int>> reportsMatrix = [];

    foreach (string line in lines) {
      string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
      List<int> levels = [];

      foreach (var item in parts) {
        levels.Add(int.Parse(item));
      }

      reportsMatrix.Add(levels);
    }

    return reportsMatrix;
  }

  public static string[] ReadInputFile(string fileName) {
    string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
    string projectDirectory = Directory.GetParent(baseDirectory).Parent.Parent.Parent.Parent.FullName
      + "\\Common\\Inputs";
    string filePath = Path.Combine(projectDirectory, fileName);
    string[] lines = File.ReadAllLines(filePath);
    return lines;
  }

  public static char[,] ReadMatrixFromFile(string fileName) {
    var lines = ReadInputFile(fileName);
    int rows = lines.Length;
    int cols = lines[0].Length;
    var matrix = new char[rows, cols];

    for (int i = 0; i < rows; i++) {
      for (int j = 0; j < cols; j++) {
        matrix[i, j] = lines[i][j];
      }
    }

    return matrix;
  }

  public static void PrintMap(char[,] map) {
    for (int i = 0; i < map.GetLength(0); i++) {
      for (int j = 0; j < map.GetLength(1); j++) {
        Console.Write(map[i, j] + "");
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
