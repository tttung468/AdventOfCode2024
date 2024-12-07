

int totalDistance = Part01CalculateTotalDistance();
Console.WriteLine($"Part 01: total distance = {totalDistance}");

int similarityScore = Part02CalculateSimilarityScore();
Console.WriteLine($"Part 02: similarity score = {similarityScore}");


static void Part01ReadInputFile(List<int> leftColumnDict, List<int> rightColumnDict) {
  string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
  string projectDirectory = Directory.GetParent(baseDirectory).Parent.Parent.Parent.FullName;
  string filePath = Path.Combine(projectDirectory, "Day01Input.txt");
  string[] lines = File.ReadAllLines(filePath);

  foreach (string line in lines) {
    // Split the line into two parts
    string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

    // Add to dictionaries
    leftColumnDict.Add(int.Parse(parts[0]));
    rightColumnDict.Add(int.Parse(parts[1]));
  }
}

static int Part01CalculateTotalDistance() {
  List<int> leftColumnDict = [];
  List<int> rightColumnDict = [];

  Part01ReadInputFile(leftColumnDict, rightColumnDict);

  leftColumnDict.Sort();
  rightColumnDict.Sort();

  int sum = 0;
  for (int i = 0; i < leftColumnDict.Count; i++) {
    sum += Math.Abs(leftColumnDict[i] - rightColumnDict[i]);
  }

  return sum;
}

static void Part02ReadInputFile(Dictionary<int, int> leftColumnDict, Dictionary<int, int> rightColumnDict) {
  string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
  string projectDirectory = Directory.GetParent(baseDirectory).Parent.Parent.Parent.FullName;
  string filePath = Path.Combine(projectDirectory, "Day01Input.txt");
  string[] lines = File.ReadAllLines(filePath);

  foreach (string line in lines) {
    // Split the line into two parts
    string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    var left = int.Parse(parts[0]);
    var right = int.Parse(parts[1]);

    // Add to dictionaries
    if (!leftColumnDict.ContainsKey(left)) {
      leftColumnDict[left] = 0;
    }
    leftColumnDict[left]++;


    if (!rightColumnDict.ContainsKey(right)) {
      rightColumnDict[right] = 0;
    }
    rightColumnDict[right]++;
  }
}

static int Part02CalculateSimilarityScore() {
  Dictionary<int, int> leftColumnDict = new();
  Dictionary<int, int> rightColumnDict = new();
  int similarityScore = 0;

  Part02ReadInputFile(leftColumnDict, rightColumnDict);

  foreach (var item in leftColumnDict) {
    int rightSimilarityScore = 0;
    if (rightColumnDict.ContainsKey(item.Key)) {
      rightSimilarityScore = rightColumnDict[item.Key];
    }

    similarityScore += item.Key * item.Value * rightSimilarityScore;
  }

  return similarityScore;
}