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
}
