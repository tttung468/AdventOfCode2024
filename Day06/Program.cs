using Day06;

//  ....#.....      ....#.....          //  ....#.....      ....#.....
//  .........#      ....+---+#          //  ....+---+#      ....+---+#
//  ..........      ....|...|.          //  ....|...|.      ....|...|.
//  ..#.......      ..#.|...|.          //  ..#.|...|.      ..#.|...|.
//  .......#..      ..+-|-+#|.          //  ....|..#|.      ..+-+-+#|.
//  ..........      ..|.|.|.|.          //  ....|...|.      ..|.|.|.|.
//  .#..^.....      .#+-^-|-+.          //  .#.O^---+.      .#+-^-+-+.
//  ........#.      .+----|+#.          //  ........#.      ......O.#.
//  #.........      #+----||..          //  #.........      #.........
//  ......#...      ......#|..          //  ......#...      ......#...


// Part 01: 41 distinct positions 

(Position startingPosition, char[,] map) = Helper.ReadMapFromFile();
var minPositionX = 0;
var maxPositionX = map.GetLength(0) - 1;
var minPositionY = 0;
var maxPositionY = map.GetLength(1) - 1;

//Day06Helper.PrintMap(map);

// Part 01
var part01Map = Helper.CopyMap(map);
var part01Start = new Position(startingPosition.X, startingPosition.Y);
var res = Part01_PredictThePath(part01Start, part01Map);
var visitedPositions = GetDistinctVisitedPositions(part01Map);
Console.WriteLine($"Part 01: {visitedPositions.Count}");
//Helper.PrintMap(part01Map);

// Part 02
Console.WriteLine("Part 02");
var part02Map = Helper.CopyMap(map);
var part02Start = new Position(startingPosition.X, startingPosition.Y);
Part02_PredictCirclePaths(part02Start, part02Map, visitedPositions);
var part02Count = visitedPositions.Count(x => x.Value);
Console.WriteLine($"\nPart 02: {part02Count}");

Dictionary<Position, bool> GetDistinctVisitedPositions(char[,] map) {
  Dictionary<Position, bool> visitedPositions = [];

  for (int i = 0; i < map.GetLength(0); i++) {
    for (int j = 0; j < map.GetLength(1); j++) {
      if (map[i, j] == Helper.VisitedCh) {
        visitedPositions.Add(new Position(i, j), false);
      }
    }
  }
  return visitedPositions;
}

// return is circle path
bool Part01_PredictThePath(Position start, char[,] map, bool isPart2 = false) {
  Dictionary<string, int> visitedObstructionPositions = [];
  int startX = start.X;
  int startY = start.Y;

  while (true) {
    if (CheckStopPosition(start)) {
      break;
    }

    // check circle path
    if (visitedObstructionPositions.Any(pair => pair.Value > 4)) {
      return true;
    }

    PredictGoUp(start, map, visitedObstructionPositions);
    PredictGoRight(start, map, visitedObstructionPositions);
    PredictGoDown(start, map, visitedObstructionPositions);
    PredictGoLeft(start, map, visitedObstructionPositions);

  }
  return false;
}

void Part02_PredictCirclePaths(Position start, char[,] map, Dictionary<Position, bool> visitedPositions) {
  foreach (var visitedPosPair in visitedPositions.OrderBy(x => x.Key.X)) {

    var copiedStart = new Position(start.X, start.Y);
    var copiedMap = Helper.CopyMap(map);

    copiedMap[visitedPosPair.Key.X, visitedPosPair.Key.Y] = Helper.ObstructionCh;
    copiedMap[copiedStart.X, copiedStart.Y] = Helper.UpDirectionCh;

    var isCircle = Part01_PredictThePath(copiedStart, copiedMap, true);

    if (isCircle) {
      visitedPositions[visitedPosPair.Key] = true;
      //Console.Write($"{(visitedPosPair.Key.X, visitedPosPair.Key.Y)} - ");
      //Helper.PrintMap(copiedMap);
    }
  }
}

void PredictGoUp(Position start, char[,] map, Dictionary<string, int> visitedObstructionPositions) {
  if (map[start.X, start.Y] != Helper.UpDirectionCh) {
    return;
  }

  for (int i = start.X; i >= minPositionX; i--) {
    if (map[i, start.Y] == Helper.ObstructionCh) {
      var key = $"({i},{start.Y})";
      CountObstructionVisited(visitedObstructionPositions, key);

      start.X = i + 1;
      map[start.X, start.Y] = Helper.RightDirectionCh;
      return;
    }

    start.X = i;
    map[start.X, start.Y] = Helper.VisitedCh;
    //Console.WriteLine($"({start.X} - {start.Y})");
  }
}

void PredictGoDown(Position start, char[,] map, Dictionary<string, int> visitedObstructionPositions) {
  if (map[start.X, start.Y] != Helper.DownDirectionCh) {
    return;
  }

  for (int i = start.X; i <= maxPositionX; i++) {
    if (map[i, start.Y] == Helper.ObstructionCh) {
      var key = $"({i},{start.Y})";
      CountObstructionVisited(visitedObstructionPositions, key);

      start.X = i - 1;
      map[start.X, start.Y] = Helper.LeftDirectionCh;
      return;
    }

    start.X = i;
    map[start.X, start.Y] = Helper.VisitedCh;
    //Console.WriteLine($"({start.X} - {start.Y})");
  }
}

void PredictGoLeft(Position start, char[,] map, Dictionary<string, int> visitedObstructionPositions) {
  if (map[start.X, start.Y] != Helper.LeftDirectionCh) {
    return;
  }

  for (int i = start.Y; i >= minPositionY; i--) {
    if (map[start.X, i] == Helper.ObstructionCh) {
      var key = $"({start.X},{i})";
      CountObstructionVisited(visitedObstructionPositions, key);

      start.Y = i + 1;
      map[start.X, start.Y] = Helper.UpDirectionCh;
      return;
    }

    start.Y = i;
    map[start.X, start.Y] = Helper.VisitedCh;
    //Console.WriteLine($"({start.X} - {start.Y})");
  }
}

void PredictGoRight(Position start, char[,] map, Dictionary<string, int> visitedObstructionPositions) {
  if (map[start.X, start.Y] != Helper.RightDirectionCh) {
    return;
  }

  for (int i = start.Y; i <= maxPositionY; i++) {
    if (map[start.X, i] == Helper.ObstructionCh) {
      var key = $"({start.X},{i})";
      CountObstructionVisited(visitedObstructionPositions, key);

      start.Y = i - 1;
      map[start.X, start.Y] = Helper.DownDirectionCh;
      return;
    }

    start.Y = i;
    map[start.X, start.Y] = Helper.VisitedCh;
    //Console.WriteLine($"({start.X} - {start.Y})");
  }
}

static void CountObstructionVisited(Dictionary<string, int> visitedObstructionPositions, string key) {
  if (!visitedObstructionPositions.TryGetValue(key, out var _)) {
    visitedObstructionPositions.Add(key, 1);
  } else {
    visitedObstructionPositions[key]++;
  }
}

bool CheckStopPosition(Position start) {
  return start.X == minPositionX || start.X == maxPositionX
        || start.Y == minPositionY || start.Y == maxPositionY;
}