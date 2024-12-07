

// xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))
// mul(
//  161 = (2*4 + 5*5 + 11*8 + 8*5).

// Part 01
// 174 095 412  - higher
// 26 724 937   - lower
// 173 785 482

// Part 02
// do(), don't()
// 2 719 815    -- too low
// 10 725 890
// 173 785 482  -- too high
// 87 863 550   -- too high
// 41 197 404   -- wrong
// 83 158 140
using System.Text.RegularExpressions;

var mulTemplate = "mul(";
var doInstructionTemplate = "do()";
var dontInstructionTemplate = "don't()";

var resultPart01 = Part01_MultiplyNumbersInMessyLines(ReadInputFile());
Console.WriteLine($"Part 01: {resultPart01}\n\n");

var resultPart02 = Part02_MultiplyNumbersWithInstruction(ReadInputFile());
Console.WriteLine($"Part 02: {resultPart02}");

long Part01_MultiplyNumbersInMessyLines(string[] lines) {
  long result = 0;

  foreach (var line in lines) {
    var splittedSubStrings = line.Split(mulTemplate);

    foreach (var split in splittedSubStrings) {
      var indexOfEnding = split.IndexOf(')');
      if (indexOfEnding < 0) {
        continue;
      }

      var strHavingFactors = split[..indexOfEnding];
      var factors = strHavingFactors.Split(',');

      if (factors.Length != 2
        || !int.TryParse(factors[0], out int parsedFactorX)
        || !int.TryParse(factors[1], out int parsedFactorY)) {
        continue;
      }

      result += parsedFactorX * parsedFactorY;
      // Console.WriteLine($"{parsedFactorX} - \t{parsedFactorY} - \t{split}");

    }
  }

  return result;
}

long Part02_MultiplyNumbersWithInstruction(string[] lines) {
  long result = 0;

  foreach (var line in lines) {
    var doInstructionSplittedSubStrings = line.Split(doInstructionTemplate);

    foreach (var doInstructionSplit in doInstructionSplittedSubStrings) {

      var indexOfDontInstruction = doInstructionSplit.IndexOf(dontInstructionTemplate);
      var onlyDoInstructionSplit = doInstructionSplit;
      if (indexOfDontInstruction >= 0) {
        onlyDoInstructionSplit = onlyDoInstructionSplit[..indexOfDontInstruction];
      }
      Console.WriteLine($"doInstructionSplit\t{doInstructionSplit}");
      Console.WriteLine($"onlyDoInstructionSplit\t{onlyDoInstructionSplit}");

      var mulTemplateSplits = onlyDoInstructionSplit.Split(mulTemplate);
      foreach (var split in mulTemplateSplits) {
        result += MultiplyFactors(split);
      }

      Console.WriteLine();
    }
  }

  return result;
}

long Part02_MultiplyNumbersWithInstruction_Ai(string[] lines) {
  long result = 0;

  foreach (var line in lines) {
    result += SumEnabledMultiplications(line);
  }

  return result;
}

static long SumEnabledMultiplications(string memory) {
  // Regular expression to find mul instructions
  Regex mulPattern = new Regex(@"mul\((\d+),(\d+)\)");

  // Regular expression to find do and don't instructions
  Regex doPattern = new Regex(@"do\(\)");
  Regex dontPattern = new Regex(@"don't\(\)");

  // Initialize variables
  bool enabled = true;
  long totalSum = 0;

  // Split the memory into tokens
  string[] tokens = Regex.Split(memory, @"(\bdo\(\)|\bdon't\(\)|mul\(\d+,\d+\))");

  foreach (var token in tokens) {
    if (doPattern.IsMatch(token)) {
      enabled = true;
    } else if (dontPattern.IsMatch(token)) {
      enabled = false;
    } else if (mulPattern.IsMatch(token) && enabled) {
      var match = mulPattern.Match(token);
      int x = int.Parse(match.Groups[1].Value);
      int y = int.Parse(match.Groups[2].Value);
      totalSum += x * y;
    }
  }

  return totalSum;
}

long MultiplyFactors(string split) {
  var indexOfEnding = split.IndexOf(')');
  if (indexOfEnding < 0) {
    return 0;
  }

  var strHavingFactors = split[..indexOfEnding];

  if (strHavingFactors.Contains(' ')) {
    return 0;
  }

  var factors = strHavingFactors.Split(',');

  if (factors.Length != 2
    || !int.TryParse(factors[0], out int parsedFactorX)
    || !int.TryParse(factors[1], out int parsedFactorY)) {
    return 0;
  }

  Console.WriteLine($"{parsedFactorX} - \t{parsedFactorY} - \t{split}");
  return parsedFactorX * parsedFactorY;
}

static string[] ReadInputFile() {
  string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
  string projectDirectory = Directory.GetParent(baseDirectory).Parent.Parent.Parent.FullName;
  string filePath = Path.Combine(projectDirectory, "Day03Input.txt");
  string[] lines = File.ReadAllLines(filePath);

  return lines;
}