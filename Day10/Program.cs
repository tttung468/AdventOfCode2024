using Common;
using Day10;

//      01234567
//   
//  0   89010123 
//  1   78121874 
//  2   87430965 
//  3   96549874 
//  4   45678903 
//  5   32019012 
//  6   01329801 
//  7   10456732


var matrix = InputHelper.ReadIntegerMatrix(InputFileName.Input10);
// InputHelper.PrintMatrix(matrix);

// Part 01: 550
//var scorePart1 = Part1_SumOfRatings(matrix);
//Console.WriteLine($"Part 01: {scorePart1}");

//static int Part1_SumOfRatings(int[,] matrix, bool isPart2 = false) {
//  var maxDistances = Helper.SumOfMaxDistances();
//  var startingLocations = Helper.FindAllLocations(matrix, Helper.StartingValue);
//  var endingLocations = Helper.FindAllLocations(matrix, Helper.EndingValue);
//  var score = 0;

//  foreach (var (startX, startY) in startingLocations) {
//    foreach (var (endX, endY) in endingLocations) {
//      int shortestPath = Dijkstra.FindShortestPath(matrix, startX, startY, endX, endY);

//      if (shortestPath == maxDistances) {
//        score++;
//      }
//    }
//  }

//  return score;
//}

// Part 01: 550
int[,] Directions = new int[,] { { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 } };

var scorePart2 = Part2_SumOfRatings(matrix, true);
Console.WriteLine($"Part 02: {scorePart2}");


int Part2_SumOfRatings(int[,] matrix, bool isPart2 = false) {
  var maxDistances = Helper.SumOfMaxDistances();
  var startingLocations = Helper.FindAllLocations(matrix, Helper.StartingValue);
  var endingLocations = Helper.FindAllLocations(matrix, Helper.EndingValue);
  var score = 0;
  List<int> path = [];
  List<List<int>> allPaths = [];

  foreach (var (startX, startY) in startingLocations) {
    foreach (var (endX, endY) in endingLocations) {
      allPaths = [];
      path = [];
      FindAllPaths(matrix, startX, startY, endX, endY, path, allPaths);
      score += allPaths.Count;
    }
  }

  return score;
}

void FindAllPaths(int[,] grid, int startX, int startY, int endX, int endY, List<int> path, List<List<int>> allPaths) {
  int rows = grid.GetLength(0);
  int cols = grid.GetLength(1);

  // Add the current cell to the path
  path.Add(grid[startX, startY]);

  // If the end cell is reached, add the current path to allPaths
  if (startX == endX && startY == endY) {
    allPaths.Add(new List<int>(path));
  } else {
    // Explore the next cells (right and down)
    for (int i = 0; i < 4; i++) {
      int newX = startX + Directions[i, 0];
      int newY = startY + Directions[i, 1];

      if (IsValid(newX, newY, rows, cols)) {
        if (matrix[newX, newY] - matrix[startX, startY] == 1) {
          FindAllPaths(grid, newX, newY, endX, endY, path, allPaths);
        }
      }
    }
  }

  // Backtrack: remove the current cell from the path
  path.RemoveAt(path.Count - 1);
}

static bool IsValid(int x, int y, int rows, int cols) {
  return x >= 0 && x < rows && y >= 0 && y < cols;
}

