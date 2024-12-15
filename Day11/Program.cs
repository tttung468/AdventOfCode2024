using Day11;


// Part 1: 239714
// var stones = Helper.ReadStonesFromFile();
// Part01.Execute(stones);


// Part 2: 
// 284973560658514
// Using List as Part1: Problem with stack overlow if using List (the items expand too large)
// Binary tree: stack overlow when adding to child node
// File Processing: the file expand to big, > 10gb
// -> using dictionary to track the stones's count

// Part02_BinaryTree.Execute(stones, Helper.MaxBlinkTime_Part1);
// Part02_FileProcessing.Execute();

Dictionary<long, long> stones = new() {
            { 92, 1 },
            { 0, 1 },
            { 286041, 1 },
            { 8034, 1 },
            { 34394, 1 },
            { 795, 1 },
            { 8, 1 },
            { 2051489, 1 },
        };
var blinks = Helper.MaxBlinkTime_Part2;
stones = Part02_TrackingStoneCount.SplitTheStones(stones, blinks);
long totalStones = Part02_TrackingStoneCount.CountTotalStones(stones);

Console.WriteLine($"Number of stones after {blinks} blinks: {totalStones}");
