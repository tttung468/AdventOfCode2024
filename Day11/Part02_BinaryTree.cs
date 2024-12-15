namespace Day11;
public static class Part02_BinaryTree {
  public static void Execute(List<long> stones, int blinkTime) {
    BinaryTree binaryTree = new();

    // initialization
    foreach (var item in stones) {
      binaryTree.Insert(item);
    }

    BinaryTree binaryTreeAfterSplit = new();
    var root = binaryTree.Root;


    for (int i = 1; i <= blinkTime; i++) {
      BinaryTree.TravelTreeAndSplitNodes(root, binaryTreeAfterSplit);
      root = binaryTreeAfterSplit.Root;
      binaryTreeAfterSplit = new();

      Console.WriteLine($"\n\nBlink time {i}");
      //BinaryTree.InOrderTraversal(root);
    }

    var count = BinaryTree.CountNodes(root);
    Console.WriteLine($"Part 2: {count}");
  }
}
