using Day07;

// 3312271364788  -- too low
// 3312271365652
var equations = Helper.ReadEquationsFromFile();
char[] part1Choices = [Helper.MultiplyOperator, Helper.PlusOperator];
var part1Total = CalculateTotalCalibration(part1Choices, equations);
Console.WriteLine($"Part 01: {part1Total}");

// Part 02
// 509463489296712
char[] part2Choices = [Helper.MultiplyOperator, Helper.PlusOperator, Helper.ConcatenationOperator];
var part2Total = CalculateTotalCalibration(part2Choices, equations);
Console.WriteLine($"Part 02: {part2Total}");

static long CalculateTotalCalibration(char[] choices, List<Equation> equations) {
  long total = 0;

  foreach (var equation in equations) {
    total += CalculateEquation(choices, equation);
  }

  return total;
}

static long CalculateEquation(char[] choices, Equation equation) {
  var generatedCombinations = Helper.GenerateOperatorCombinations(choices, equation.Numbers.Count - 1);
  long result = 0;

  foreach (var combination in generatedCombinations) {
    long resultEquation = CalculateBaseOnOperator(equation.Numbers[0], equation.Numbers[1], combination[0]);

    for (int i = 2; i < equation.Numbers.Count; i++) {
      resultEquation = CalculateBaseOnOperator(resultEquation, equation.Numbers[i], combination[i - 1]);
    }

    if (resultEquation == equation.EquationResult) {
      return resultEquation;
    }
  }

  return result;
}

static long CalculateBaseOnOperator(long num1, long num2, char operatorCh) {
  if (operatorCh == Helper.MultiplyOperator) {
    return num1 * num2;
  } else if (operatorCh == Helper.PlusOperator) {
    return num1 + num2;
  } else if (operatorCh == Helper.ConcatenationOperator) {
    var concatedNum = num1 + "" + num2;
    return long.Parse(concatedNum);
  }
  return 0;
}