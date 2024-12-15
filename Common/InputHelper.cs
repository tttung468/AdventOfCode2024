namespace Common;

public static class InputHelper {
  public const char DotChar = '.';

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
    string filePath = GetFilePath(fileName);
    string[] lines = File.ReadAllLines(filePath);
    return lines;
  }

  public static string GetFilePath(string fileName) {
    string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
    string projectDirectory = Directory.GetParent(baseDirectory).Parent.Parent.Parent.Parent.FullName
      + "\\Common\\Inputs";
    string filePath = Path.Combine(projectDirectory, fileName);
    return filePath;
  }

  public static char[,] ReadCharacterMatrix(string fileName) {
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

  public static int[,] ReadIntegerMatrix(string fileName) {
    var lines = ReadInputFile(fileName);
    int rows = lines.Length;
    int cols = lines[0].Length;
    var matrix = new int[rows, cols];

    for (int i = 0; i < rows; i++) {
      for (int j = 0; j < cols; j++) {
        if (lines[i][j] == DotChar) {
          matrix[i, j] = int.MaxValue;
        } else {
          matrix[i, j] = int.Parse(lines[i][j].ToString());
        }
      }
    }

    return matrix;
  }

  public static void PrintMatrix<T>(T[,] matrix) {
    for (int i = 0; i < matrix.GetLength(0); i++) {
      for (int j = 0; j < matrix.GetLength(1); j++) {
        Console.Write(matrix[i, j] + "");
      }
      Console.WriteLine("");
    }
    Console.WriteLine("");
  }

  public static T[,] CopyMatrix<T>(T[,] matrix) {
    T[,] copy = new T[matrix.GetLength(0), matrix.GetLength(1)];
    for (int i = 0; i < matrix.GetLength(0); i++) {
      for (int j = 0; j < matrix.GetLength(1); j++) {
        copy[i, j] = matrix[i, j];
      }
    }
    return copy;
  }

}
