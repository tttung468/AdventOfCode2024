using Day09;


// 12345
// 0..111....22222
// 02.111....2222.
// 022111....222..
// 0221112...22...
// 02211122..2....
// 022111222......

// 2333133121414131402
// -> 00...111...2...333.44.5555.6666.777.888899
// -> 0099811188827773336446555566..............
// denseFormat -> decodedFormat -> compactedSpaceFormat
// checkSum 1928


// Part 01
// 5905846266 too low
// 6385338159127
//Part01();

// Part 02
// 6415163624282
Part02();

static void Part01() {
  var (denseDisk, decodedDisk) = Helper.ReadAndDecodeInputFromFile();
  // Console.WriteLine(denseDisk);

  //for (int i = 0; i < decodedInput.Count; i++) {
  //  Console.Write(decodedInput[i]);
  //}
  //Console.WriteLine();

  var compactedSpaceInput = Part01_CompactSpace(decodedDisk);
  var checksum = CalculateChecksum(compactedSpaceInput);

  //for (int i = 0; i < compactedSpaceInput.Count; i++) {
  //  Console.Write(compactedSpaceInput[i]);
  //}
  Console.WriteLine($"\nPart 01: {checksum}");
}

static void Part02() {

  var (denseDisk, decodedDisk) = Helper.ReadAndDecodeInputFromFile();
  //Console.WriteLine("decodedDisk");
  //for (int i = 0; i < decodedDisk.Count; i++) {
  //  Console.Write(decodedDisk[i]);
  //}
  Console.WriteLine();

  var compactedSpaceInput = Part02_CompactSpace(decodedDisk);
  var checksum = CalculateChecksum(compactedSpaceInput);
  Console.WriteLine($"\nPart 02: {checksum}");

  //Console.WriteLine("\ncompactedSpaceInput");
  //for (int i = 0; i < compactedSpaceInput.Count; i++) {
  //  Console.Write(compactedSpaceInput[i]);
  //}
}

static List<int> Part01_CompactSpace(List<int> disk) {
  var left = 0;
  var right = disk.Count;
  List<int> compactedSpaceDisk = new(disk);

  while (left < right) {
    if (compactedSpaceDisk[left] == Helper.SpaceInDisk) {
      while (left < right) {
        right--;

        if (compactedSpaceDisk[right] != Helper.SpaceInDisk) {
          compactedSpaceDisk[left] = compactedSpaceDisk[right];
          compactedSpaceDisk[right] = Helper.SpaceInDisk;
          break;
        }
      }
    }

    left++;
  }

  return compactedSpaceDisk;
}

static List<int> Part02_CompactSpace(List<int> disk) {
  List<int> compactedSpaceDisk = new(disk);
  var filesOccurences = CountOccurencesOfFilesInDisk(disk);
  int startingFilePointer = FindStartingPoint(compactedSpaceDisk);

  while (startingFilePointer >= 0) {
    if (compactedSpaceDisk[startingFilePointer] == Helper.SpaceInDisk) {
      startingFilePointer--;
      continue;
    }

    var fileID = compactedSpaceDisk[startingFilePointer];
    var span = filesOccurences[fileID];
    var endingFilePointer = startingFilePointer + 1 - span;
    var startingPointerOfFreeSpace = FindStartingPointOfFreeSpace(compactedSpaceDisk, span, endingFilePointer);

    if (startingPointerOfFreeSpace < 0) {
      startingFilePointer = endingFilePointer - 1;
      continue;
    }

    MoveFiles(compactedSpaceDisk, startingPointerOfFreeSpace, span, fileID);
    MoveFiles(compactedSpaceDisk, endingFilePointer, span, Helper.SpaceInDisk);

    //Console.WriteLine($"\nfileId: {fileID}");
    //for (int i = 0; i < compactedSpaceDisk.Count; i++) {
    //  Console.Write(compactedSpaceDisk[i]);
    //}

    startingFilePointer = endingFilePointer - 1;
  }

  return compactedSpaceDisk;
}

static void MoveFiles(List<int> disk, int startingPoint, int span, int fileID) {
  for (int i = startingPoint; i < startingPoint + span; i++) {
    disk[i] = fileID;
  }
}

static int FindStartingPointOfFreeSpace(List<int> disk, int span, int limitPointer) {
  for (int i = 0; i < limitPointer; i++) {
    var spaces = disk.GetRange(i, span);
    if (spaces.All(x => x == Helper.SpaceInDisk)) {
      return i;
    }
  }

  return -1;
}

static int FindStartingPoint(List<int> disk) {
  for (int i = disk.Count - 1; i >= 0; i--) {
    if (disk[i] != Helper.SpaceInDisk) {
      return i;
    }
  }

  return -1;
}

static Dictionary<int, int> CountOccurencesOfFilesInDisk(List<int> disk) {
  Dictionary<int, int> filesOccurences = [];

  foreach (var file in disk) {
    if (file == Helper.SpaceInDisk) {
      continue;
    }

    if (!filesOccurences.TryGetValue(file, out _)) {
      filesOccurences[file] = 0;
    }
    filesOccurences[file]++;
  }

  return filesOccurences;
}

static long CalculateChecksum(List<int> disk) {
  long count = 0;

  for (int i = 0; i < disk.Count; i++) {
    if (disk[i] == Helper.SpaceInDisk) {
      count += 0;
    } else {
      count += i * disk[i];
    }
  }

  return count;
}

