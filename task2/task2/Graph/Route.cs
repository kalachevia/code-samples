
namespace task2.Graph {
  /// <summary>
  /// Represents a route between two nodes
  /// </summary>
  public class Route {
    #region Ctor
    /// <summary>
    /// Initialize a new route between two nodes within defined cost
    /// </summary>
    public Route(int cost, int node1, int node2) {
      Cost = cost;
      Node1 = node1;
      Node2 = node2;
    }
    #endregion
    #region Properties
    /// <summary>
    /// Represents the time is taken to move from one node to another
    /// </summary>
    public int Cost { get; private set; }
    public int Node1 { get; private set; }
    public int Node2 { get; private set; }
    #endregion
  }
}
