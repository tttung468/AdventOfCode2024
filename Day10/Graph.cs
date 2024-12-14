namespace Day10;

public class Graph {

  public int Vertices { get; set; }
  public List<Tuple<int, int>>[] AdjacencyList { get; set; }

  public Graph(int vertices) {
    Vertices = vertices;
    AdjacencyList = new List<Tuple<int, int>>[vertices];

    for (int i = 0; i < vertices; i++) {
      AdjacencyList[i] = [];
    }
  }

  public void AddEdge(int u, int v, int weight) {
    AdjacencyList[u].Add(new Tuple<int, int>(v, weight));
  }
}