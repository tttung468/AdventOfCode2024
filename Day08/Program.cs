using Day08;

//  ............      ......#....#      ##....#....#
//  ........0...      ...#....0...      .#.#....0...
//  .....0......      ....#0....#.      ..#.#0....#.
//  .......0....      ..#....0....      ..##...0....
//  ....0.......      ....0....#..      ....0....#..
//  ......A.....      .#....A.....      .#...#A....#
//  ............      ...#........      ...#..#.....
//  ............      #......#....      #....#.#....
//  ........A...      ........A...      ..#.....A...
//  .........A..      .........A..      ....#....A..
//  ............      ..........#.      .#........#.
//  ............      ..........#.      ...#......##

// TopRight:    Anten1.X - Anten2.X > 0 && Anten1.Y - Anten2.Y < 0
// TopLeft:     Anten1.X - Anten2.X > 0 && Anten1.Y - Anten2.Y > 0
// BottomRight: Anten1.X - Anten2.X < 0 && Anten1.Y - Anten2.Y < 0
// BottomLeft:  Anten1.X - Anten2.X < 0 && Anten1.Y - Anten2.Y > 0
// horizontal:  Anten1.X - Anten2.X == 0
// vertical:    Anten1.Y - Anten2.Y == 0

// Antennas   (1,1), (2,1)  vertical (BottomRight)
// diffLocationX = 1   diffLocationY = 0  -> Antinodes  (0,1), (3,1)
// Antennas   (0,1), (0,2)  vertical (BottomRight)
// diffLocationX = 0   diffLocationY = -1  -> Antinodes  (0,0), (0,3)

// Antennas   (2,5), (1,8)  TopRight  diffLocationX = 1   diffLocationY = -3 -> Antinodes  (3,2), (0,11)
// leftAntinodeX = antenna1.X + |diffLocationX| = 2 + |1| = 3
// leftAntinodeY = antenna1.Y - |diffLocationY| = 5 - |-3| = 2
// rightAntinodeX = antenna2.X - |diffLocationX| = 1 - |1| = 3
// rightAntinodeY = antenna2.Y + |diffLocationY| = 8 + |-3| = 11

// Antennas   (2,5), (3, 7)      BottomRight -> Antinodes  (1, 3), (4,9)
// diffLocationX = -1   diffLocationY = -2
// leftAntinodeX = antenna1.X - |diffLocationX| = 2 - |1| = 1
// leftAntinodeY = antenna1.Y - |diffLocationY| = 5 - |-2| = 3
// rightAntinodeX = antenna2.X + |diffLocationX| = 3 + |1| = 4
// rightAntinodeY = antenna2.Y + |diffLocationY| = 7 + |-2| = 9

// Part 01
// 357
var (map, antennasDictionary) = Helper.ReadAntennasFromFile();
var minLocationX = 0;
var maxLocationX = map.GetLength(0) - 1;
var minLocationY = 0;
var maxLocationY = map.GetLength(1) - 1;
// InputHelper.PrintMap(map);

var uniqueAntinodes = Part01_CountUniqueLocationsContainingAntinodes(antennasDictionary);
var part01Count = uniqueAntinodes.Count;
Console.WriteLine($"Part 01: {part01Count}");
//Helper.PrintMapWithAntinodes(map, uniqueAntinodes);

// Part 02
// 1380   too high
var (count, part02UniqueAntinodes) = Part02_CountAntinodesWithImpactedSignals(antennasDictionary, true);
var part02Count = part02UniqueAntinodes.Count + Helper.CountAntennas(map);
Console.WriteLine($"Part 02: {part02Count}");
// Helper.PrintMapWithAntinodes(map, part02UniqueAntinodes);



Dictionary<string, Location> Part01_CountUniqueLocationsContainingAntinodes(Dictionary<char, List<Location>> antennasDictionary, bool isForPart2 = false) {
  List<Location> antinodes = [];
  Dictionary<string, Location> uniqueAntinodes = [];

  foreach (var pair in antennasDictionary) {
    var posibleAntinodesOfAntennas = GetAntinodes(pair.Value, isForPart2);
    antinodes.AddRange(posibleAntinodesOfAntennas);
  }

  foreach (var antinode in antinodes) {
    var key = $"{antinode.X}:{antinode.Y}";
    if (!uniqueAntinodes.TryGetValue(key, out var _)) {
      uniqueAntinodes[key] = antinode;
    }
  }

  return uniqueAntinodes;
}


(int count, Dictionary<string, Location> uniqueAntinodes) Part02_CountAntinodesWithImpactedSignals(Dictionary<char, List<Location>> antennasDictionary, bool isForPart2 = false) {
  List<Location> antinodes = [];
  Dictionary<string, Location> uniqueAntinodes = [];

  foreach (var pair in antennasDictionary) {
    var posibleAntinodesOfAntennas = GetAntinodes(pair.Value, isForPart2);
    antinodes.AddRange(posibleAntinodesOfAntennas);
  }

  // get antennasKeys
  List<string> antennasKeys = [];
  for (int i = 0; i < map.GetLength(0); i++) {
    for (int j = 0; j < map.GetLength(1); j++) {
      if (map[i, j] != Helper.DotChar) {
        antennasKeys.Add($"{i}:{j}");
      }
    }
  }

  foreach (var antinode in antinodes) {
    var key = $"{antinode.X}:{antinode.Y}";
    if (!uniqueAntinodes.TryGetValue(key, out var _)
      && !antennasKeys.Contains(key)) {
      uniqueAntinodes[key] = antinode;
    }
  }

  return (antinodes.Count, uniqueAntinodes);
}


List<Location> GetAntinodes(List<Location> antennas, bool isForPart2 = false) {
  List<Location> antinodes = [];

  for (int i = 0; i < antennas.Count; i++) {
    for (int j = i + 1; j < antennas.Count; j++) {
      var locations = GetAntinodesFrom2Antennas(antennas[i], antennas[j], isForPart2);
      antinodes.AddRange(locations);
    }
  }

  return antinodes;
}

List<Location> GetAntinodesFrom2Antennas(Location antenna1, Location antenna2, bool isForPart2 = false) {
  List<Location> antinodes = [];
  if (ShouldSwapAntennaPointerToLeft(antenna1, antenna2)) {
    (antenna1, antenna2) = (antenna2, antenna1);
  }
  var (antinode1, antinode2) = CalculateAntinodeLocations(antenna1, antenna2);

  if (!IsLocationOutOfMap(antinode1)) {
    antinodes.Add(antinode1);
  }
  if (!IsLocationOutOfMap(antinode2)) {
    antinodes.Add(antinode2);
  }

  if (isForPart2) {
    // Console.WriteLine($"debug part2: ({antenna1.X},{antenna1.Y}), ({antenna2.X},{antenna2.Y})");

    List<Location> impacted = CalculateAntinodeLocationsWithImpactOfSignals(antinode1, antenna1, true);
    antinodes.AddRange(impacted);

    impacted = CalculateAntinodeLocationsWithImpactOfSignals(antenna2, antinode2, false);
    antinodes.AddRange(impacted);
  }

  return antinodes;
}

List<Location> CalculateAntinodeLocationsWithImpactOfSignals(Location antinode, Location antenna, bool isImpactedOnTheLeft) {
  Location? antinode1 = antinode;
  Location? antinode2 = antinode;
  List<Location> antinodes = [];

  while (true) {

    if (isImpactedOnTheLeft) {
      if (ShouldSwapAntennaPointerToLeft(antinode, antenna)) {
        (antinode, antenna) = (antenna, antinode);
      }
      (antinode1, _) = CalculateAntinodeLocations(antinode, antenna);
      // Console.WriteLine($"ImpactedOnTheLeft: ({antinode1.X},{antinode1.Y}), ({antinode.X},{antinode.Y}), ({antenna.X},{antenna.Y})");

      if (IsLocationOutOfMap(antinode1)) {
        break;
      } else {
        antinodes.Add(antinode1);
        antenna = antinode;
        antinode = antinode1;
      }
    } else {
      if (ShouldSwapAntennaPointerToLeft(antenna, antinode)) {
        (antenna, antinode) = (antinode, antenna);
      }
      (_, antinode2) = CalculateAntinodeLocations(antenna, antinode);
      //Console.WriteLine($"ImpactedOnTheright: ({antenna.X},{antenna.Y}), ({antinode.X},{antinode.Y}), ({antinode2.X},{antinode2.Y})");

      if (IsLocationOutOfMap(antinode2)) {
        break;
      } else {
        antinodes.Add(antinode2);
        antenna = antinode;
        antinode = antinode2;
      }
    }
  }

  return antinodes;
}


// choose the antenna pointer always in the left
bool ShouldSwapAntennaPointerToLeft(Location antenna1, Location antenna2) { return antenna1.Y > antenna2.Y || (antenna1.Y == antenna2.Y && antenna1.X > antenna2.X); }

(Location antinode1, Location antinode2) CalculateAntinodeLocations(Location antenna1, Location antenna2) {

  var antinode1 = new Location();
  var antinode2 = new Location();
  var diffLocationX = antenna1.X - antenna2.X;
  var diffLocationY = antenna1.Y - antenna2.Y;

  // topRight
  if (diffLocationX >= 0 && diffLocationY < 0) {
    CalculateLocationWhenTopRightAntenna2(antenna1, antenna2, antinode1, antinode2, diffLocationX, diffLocationY);
  }
  // bottomRight, horizontal, vertical
  else if (diffLocationX <= 0 && diffLocationY <= 0) {
    CalculateLocationWhenBottomRightAntenna2(antenna1, antenna2, antinode1, antinode2, diffLocationX, diffLocationY);
  }

  return (antinode1, antinode2);
}

void CalculateLocationWhenTopRightAntenna2(Location antenna1, Location antenna2, Location antinode1, Location antinode2, int diffLocationX, int diffLocationY) {
  antinode1.X = antenna1.X + Math.Abs(diffLocationX);
  antinode1.Y = antenna1.Y - Math.Abs(diffLocationY);
  antinode2.X = antenna2.X - Math.Abs(diffLocationX);
  antinode2.Y = antenna2.Y + Math.Abs(diffLocationY);
}

void CalculateLocationWhenBottomRightAntenna2(Location antenna1, Location antenna2, Location antinode1, Location antinode2, int diffLocationX, int diffLocationY) {
  antinode1.X = antenna1.X - Math.Abs(diffLocationX);
  antinode1.Y = antenna1.Y - Math.Abs(diffLocationY);
  antinode2.X = antenna2.X + Math.Abs(diffLocationX);
  antinode2.Y = antenna2.Y + Math.Abs(diffLocationY);
}

bool IsLocationOutOfMap(Location location) {
  if (location.X < minLocationX || location.X > maxLocationX
    || location.Y < minLocationY || location.Y > maxLocationY) {
    return true;
  }
  return false;
}