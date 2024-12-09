namespace Day07;
public class Equation {
  public Equation() {
  }

  public Equation(long equationResult, List<long> numbers) {
    EquationResult = equationResult;
    Numbers = numbers;
  }

  public long EquationResult { get; set; }
  public List<long> Numbers { get; set; } = [];
}
