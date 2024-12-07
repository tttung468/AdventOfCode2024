

//   0 1 2 3 4 5
//0  . . X . . .
//1  . S A M X .
//2  . A . . A .
//3  X M A S . S
//4  . X . . . .

using Common;

var xmasTemplate = "XMAS";
var samxTemplate = "SAMX";

var lines = InputHelper.ReadInputFile(InputFileName.Input04);
Console.WriteLine($"Part01: {Part01_CountTheWordOccurs(lines)}");


int Part01_CountTheWordOccurs(string[] matrix) {
  var count = 0;

  for (int idX = 0; idX < matrix.Length; idX++) {
    for (int idY = 0; idY < matrix[idX].Length; idY++) {
      if (matrix[idX][idY] == 'X') {
        int temp;

        temp = CountByHorizontal(idY, matrix[idX]);
        if (temp > 0) {
          //Console.WriteLine($"{idX} - {idY} - Horizontal");
        }
        count += temp;

        temp = CountByVertical(idX, idY, matrix);
        if (temp > 0) {
          //Console.WriteLine($"{idX} - {idY} - Vertical");
        }
        count += temp;
      }
    }
  }

  count += CountByDiagonal(matrix);


  return count;
}

int CountByDiagonal(string[] matrix) {
  var diagonals = GetAllDiagonals(matrix);
  var count = 0;

  foreach (var item in diagonals) {
    for (int i = 0; i < item.Length; i++) {
      count += CountByHorizontal(i, item);
    }
  }

  return count;
}

static List<string> GetAllDiagonals(string[] matrx) {
  List<string> allDiagonals = [];
  var rows = matrx.Length;
  var cols = matrx[0].Length;

  // Take the main diagonals from the first row
  Console.WriteLine("\nMain diagonals from the first row");
  for (int startCol = 0; startCol < cols; startCol++) {
    List<char> mainDiagonal = [];
    for (int i = 0, j = startCol; i < rows && j < cols; i++, j++) {
      mainDiagonal.Add(matrx[i][j]);
    }
    var str = new string(mainDiagonal.ToArray());
    allDiagonals.Add(str);
    Console.WriteLine(str);
  }

  // Take the main diagonals from the first column (ignore the first element as it was taken above)
  Console.WriteLine("\nMain diagonals from the first column");
  for (int startRow = 1; startRow < rows; startRow++) {
    List<char> mainDiagonal = new List<char>();
    for (int i = startRow, j = 0; i < rows && j < cols; i++, j++) {
      mainDiagonal.Add(matrx[i][j]);
    }
    var str = new string(mainDiagonal.ToArray());
    allDiagonals.Add(str);
    Console.WriteLine(str);
  }

  // Take the sub-diagonals from the first row
  Console.WriteLine("\nSub-diagonals from the first row");
  for (int startCol = 0; startCol < cols; startCol++) {
    List<char> antiDiagonal = new List<char>();
    for (int i = 0, j = startCol; i < rows && j >= 0; i++, j--) {
      antiDiagonal.Add(matrx[i][j]);
    }
    var str = new string(antiDiagonal.ToArray());
    allDiagonals.Add(str);
    Console.WriteLine(str);
  }

  // sub-diagonals from the first column
  Console.WriteLine("\nduong cheo phu tu cot cuoi cung");
  for (int startRow = 1; startRow < rows; startRow++) {
    List<char> antiDiagonal = new List<char>();
    for (int i = startRow, j = cols - 1; i < rows && j >= 0; i++, j--) {
      antiDiagonal.Add(matrx[i][j]);
    }
    var str = new string(antiDiagonal.ToArray());
    allDiagonals.Add(str);
    Console.WriteLine(str);
  }

  return allDiagonals;
}

int CountByVertical(int idX, int idY, string[] matrix) {
  var column = string.Empty;
  for (int index = 0; index < matrix.Length; index++) {
    column += matrix[index][idY];
  }
  return CountByHorizontal(idX, column);
}

int CountByHorizontal(int index, string line) {
  var count = 0;
  var limitIndex = xmasTemplate.Length - 1;

  // backwards
  if (index >= limitIndex) {
    var samxSubtr = line.Substring(index - limitIndex, xmasTemplate.Length);
    if (samxSubtr == samxTemplate) {
      count++;
    }
  }

  // forwards
  if (index <= line.Length - 1 - limitIndex) {
    var xmasSubtr = line.Substring(index, xmasTemplate.Length);
    if (xmasSubtr == xmasTemplate) {
      count++;
    }
  }

  return count;
}