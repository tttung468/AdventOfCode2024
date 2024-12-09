using Common;

namespace Day07;
public static class Helper {
  public const char MultiplyOperator = '*';
  public const char PlusOperator = '+';
  public const char ConcatenationOperator = '|';

  public static List<Equation> ReadEquationsFromFile() {
    var lines = InputHelper.ReadInputFile(InputFileName.Input07);
    List<Equation> equations = [];

    foreach (var line in lines) {
      var sections = line.Split(':');
      var equationResult = long.Parse(sections[0]);
      var numbersStr = sections[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
      List<long> numbers = [];

      foreach (var number in numbersStr) {
        numbers.Add(long.Parse(number));
      }

      equations.Add(new Equation(equationResult, numbers));
    }

    return equations;
  }

  public static List<char[]> GenerateOperatorCombinations(char[] choices, long slotNumber) {
    long totalCombinations = (long)Math.Pow(choices.Length, slotNumber);
    List<char[]> combinations = [];

    for (long i = 0; i < totalCombinations; i++) {
      char[] combination = new char[slotNumber];
      long temp = i;
      for (long j = 0; j < slotNumber; j++) {
        combination[j] = choices[temp % choices.Length];
        temp /= choices.Length;
      }
      combinations.Add(combination);
    }

    return combinations;
  }
}
