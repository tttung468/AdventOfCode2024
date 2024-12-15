namespace Day11;
public class BinaryTree {
  public Node? Root;

  public BinaryTree() {
    Root = null;
  }

  // Insert a new node into the binary tree
  public void Insert(long data) {
    Root = InsertRec(Root, data);
  }

  private Node InsertRec(Node? root, long data) {
    if (root == null) {
      root = new Node(data);
      return root;
    }

    if (data <= root.Data) {
      root.Left = InsertRec(root.Left, data);
    } else if (data > root.Data) {
      root.Right = InsertRec(root.Right, data);
    }

    return root;
  }

  public static void InOrderTraversal(Node? node) {
    if (node != null) {
      InOrderTraversal(node.Left);
      Console.Write(node.Data + " ");
      InOrderTraversal(node.Right);
    }
  }

  public static void TravelTreeAndSplitNodes(Node? node, BinaryTree binaryTree) {
    if (node != null) {
      TravelTreeAndSplitNodes(node.Left, binaryTree);
      // Console.Write(node.Data + " ");
      SplitTheNode(node.Data, binaryTree);
      TravelTreeAndSplitNodes(node.Right, binaryTree);
    }
  }

  private static void SplitTheNode(long nodeData, BinaryTree binaryTree) {
    var nodeDataStr = nodeData.ToString();

    if (nodeData == Helper.Stone0) {
      binaryTree.Insert(Helper.Stone1);
    } else if (Helper.IsEvenNumberOfDigits(nodeDataStr)) {
      var (leftNumber, rightNumber) = Helper.SplitTheStoneWithEvenOfDigits(nodeDataStr);
      binaryTree.Insert(leftNumber);
      binaryTree.Insert(rightNumber);
    } else {
      binaryTree.Insert(nodeData * Helper.Stone2024);
    }
  }

  public static int CountNodes(Node? node) {
    if (node == null) {
      return 0;
    }

    int leftCount = CountNodes(node.Left);
    int rightCount = CountNodes(node.Right);

    return 1 + leftCount + rightCount;
  }
}