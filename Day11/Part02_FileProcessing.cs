using Common;

namespace Day11;
public class Part02_FileProcessing {
  public static void Execute() {
    var readingFile = InputHelper.GetFilePath(InputFileName.Input11);
    RemoveAllFiles();

    try {
      for (int i = 1; i <= Helper.MaxBlinkTime_Part2; i++) {
        Console.WriteLine($"Blink of {i}");

        using StreamReader sr = new(readingFile);
        var writingFile = $"Day11_ResultOfBlink_{i}.txt";
        StreamWriter sw = new(writingFile, true);
        ProcessStone(sr, sw, readingFile);

        readingFile = writingFile;

        sr.Close();
        sw.Close();
      }
    } catch (Exception e) {
      Console.WriteLine("The file could not be read:");
      Console.WriteLine(e.Message);
    }

    var linesCount = CountLine(readingFile);
    Console.WriteLine($"Part 2: {linesCount}");
  }

  private static void ProcessStone(StreamReader sr, StreamWriter sw, string readingFile) {
    string? stone;
    var coutLine = CountLine(readingFile);

    Parallel.For(0, coutLine, i => {
      lock (sr) {
        stone = sr.ReadLine();
        if (stone != null) {
          var stoneNr = long.Parse(stone);
          if (stoneNr == Helper.Stone0) {
            sw.WriteLine(Helper.Stone1);
          } else if (Helper.IsEvenNumberOfDigits(stone)) {
            var (leftNumber, rightNumber) = Helper.SplitTheStoneWithEvenOfDigits(stone);
            sw.WriteLine(leftNumber);
            sw.WriteLine(rightNumber);
          } else {
            sw.WriteLine(stoneNr * Helper.Stone2024);
          }
        }
      }


    });

    //while ((stone = sr.ReadLine()) != null) {
    //  var stoneNr = long.Parse(stone);
    //  if (stoneNr == Helper.Stone0) {
    //    sw.WriteLine(Helper.Stone1);
    //  } else if (Helper.IsEvenNumberOfDigits(stone)) {
    //    var (leftNumber, rightNumber) = Helper.SplitTheStoneWithEvenOfDigits(stone);
    //    sw.WriteLine(leftNumber);
    //    sw.WriteLine(rightNumber);
    //  } else {
    //    sw.WriteLine(stoneNr * Helper.Stone2024);
    //  }
    //}
  }

  private static long CountLine(string filePath) {
    int lineCount = 0;

    using StreamReader reader = new(filePath);
    // Read the file line by line
    while (reader.ReadLine() != null) {
      lineCount++;
    }

    return lineCount;
  }

  private static void RemoveAllFiles() {
    string currentDirectory = Directory.GetCurrentDirectory();

    // Get all files in the directory
    string[] files = Directory.GetFiles(currentDirectory, "*.txt");

    // Delete each file
    foreach (string file in files) {
      File.Delete(file);
      // Console.WriteLine($"Deleted: {file}");
    }
  }
}
