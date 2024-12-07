// See https://aka.ms/new-console-template for more information

var countSafeReports = Part01_CountSafeReports();
Console.WriteLine($"Safe reports: {countSafeReports}\n\n");

var countSafeReportsWithProblemDamener = Part02_CountSafeReportsWithDampener();
Console.WriteLine($"\nSafe reports with Problem Damener: {countSafeReportsWithProblemDamener}");

static int Part01_CountSafeReports() {
  var reportsMatrix = ReadInputFile();

  int safeCount = 0;
  foreach (var report in reportsMatrix) {
    if (Part01_IsSafeReport(report)) {
      safeCount++;
    }
  }
  return safeCount;
}

static bool Part01_IsSafeReport(List<int> report) {
  bool increasing = true;
  bool decreasing = true;

  for (int i = 0; i < report.Count - 1; i++) {
    int diff = report[i + 1] - report[i];
    if (diff < 1 || diff > 3) {
      increasing = false;
    }
    if (diff > -1 || diff < -3) {
      decreasing = false;
    }
  }

  return increasing || decreasing;
}

static int Part02_CountSafeReportsWithDampener() {
  var reportsMatrix = ReadInputFile();

  int safeCount = 0;
  foreach (var report in reportsMatrix) {
    Console.WriteLine();


    foreach (var item in report) {
      Console.Write($"{item} ");
    }

    if (Part01_IsSafeReport(report)) {
      safeCount++;
      Console.Write("\t\tSafe");
      continue;
    }

    for (int i = 0; i < report.Count; i++) {
      List<int> reportWithoutDampenerIndex = new(report);
      reportWithoutDampenerIndex.RemoveAt(i);

      if (Part01_IsSafeReport(reportWithoutDampenerIndex)) {
        safeCount++;
        Console.Write($"\t\t DampenerIndex: {i}\t\tSafe");
        break;
      }
    }

  }
  return safeCount;
}

static List<List<int>> ReadInputFile() {
  string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
  string projectDirectory = Directory.GetParent(baseDirectory).Parent.Parent.Parent.FullName;
  string filePath = Path.Combine(projectDirectory, "Day02Input.txt");
  string[] lines = File.ReadAllLines(filePath);
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