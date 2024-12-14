namespace Day10;

public static class Dijkstra {
  public static int[,] Directions = new int[,] { { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 } };

  public static int FindShortestPath(int[,] matrix, int startX, int startY, int endX, int endY) {
    int rows = matrix.GetLength(0);
    int cols = matrix.GetLength(1);
    var graph = ConvertMatrxToGraph(matrix, rows, cols);

    return DijkstraAlgorithm(graph, startX * cols + startY, endX * cols + endY, rows, cols);
  }

  private static Graph ConvertMatrxToGraph(int[,] matrix, int rows, int cols) {
    int vertices = rows * cols;
    Graph graph = new(vertices);

    for (int i = 0; i < rows; i++) {
      for (int j = 0; j < cols; j++) {
        int u = i * cols + j;

        for (int d = 0; d < 4; d++) {
          int newX = i + Directions[d, 0];
          int newY = j + Directions[d, 1];

          if (newX >= 0 && newX < rows && newY >= 0 && newY < cols) {
            int v = newX * cols + newY;

            if (matrix[newX, newY] - matrix[i, j] == 1) {
              graph.AddEdge(u, v, matrix[newX, newY]);
            }
          }
        }
      }
    }

    return graph;
  }

  private static int DijkstraAlgorithm(Graph graph, int source, int target, int rows, int cols) {
    int vertices = graph.Vertices;
    int[] distances = new int[vertices];
    bool[] shortestPathTreeSet = new bool[vertices];

    for (int i = 0; i < vertices; i++) {
      distances[i] = int.MaxValue;
      shortestPathTreeSet[i] = false;
    }

    distances[source] = 0;
    var priorityQueue = new SortedSet<(int Distance, int Vertex)> {
      (0, source)
    };

    while (priorityQueue.Count > 0) {
      var (currentDistance, u) = priorityQueue.Min;
      priorityQueue.Remove(priorityQueue.Min);

      if (u == target) {
        return currentDistance;
      }

      if (shortestPathTreeSet[u]) {
        continue;
      }

      shortestPathTreeSet[u] = true;

      var adjacencyList = graph.AdjacencyList[u];
      foreach (var edge in adjacencyList) {
        int v = edge.Item1;
        int weight = edge.Item2;

        if (!shortestPathTreeSet[v] && distances[u] != int.MaxValue
          && distances[u] + weight < distances[v]) {
          distances[v] = distances[u] + weight;
          priorityQueue.Add((distances[v], v));
        }
      }
    }

    return -1; // Path not found
  }
}
