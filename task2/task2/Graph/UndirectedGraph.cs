using System;
using System.Collections.Generic;
using System.Linq;

namespace task2.Graph {
  /// <summary>
  /// Represents undirected graph as list of routes between nodes and
  /// provides shortest way calc functionality between two nodes 
  /// </summary>
  public class SimpleGraph : IGraph {
    #region Ctor
    /// <summary>
    /// Initialize empty graph
    /// </summary>
    public SimpleGraph() {
      fRoutes = new List<Route>();
      fNodes = new List<int>();
    }
    #endregion
    #region Fields
    private List<Route> fRoutes;
    private List<int> fNodes;
    #endregion
    #region Methods
    /// <summary>
    /// Inserts an array of nodes into graph
    /// </summary>
    private void AddNode(params int[] nodes) {
      foreach (int node in nodes)
        if (!fNodes.Contains(node))
          fNodes.Add(node);
    }

    /// <summary>
    /// Checks if graph contain route between two nodes
    /// </summary>
    public bool ContainsRoute(int from, int to) {
      return fRoutes.Any(x => x.Node1 == from && x.Node2 == to);
    }

    /// <summary>
    /// Returns number of routes in graph
    /// </summary>
    public int RouteCount() {
      return fRoutes.Count;
    }

    /// <summary>
    /// Clears all graph content
    /// </summary>
    public void Clear() {
      fRoutes.Clear();
      fNodes.Clear();
    }

    /// <summary>
    /// Inserts a route between two nodes with a defined cost
    /// </summary>
    public void AddRoute(int cost, int node1Id, int node2Id) {
      AddNode(node1Id, node2Id);

      if (node1Id != node2Id && !ContainsRoute(node1Id, node2Id))
        fRoutes.Add(new Route(cost, node1Id, node2Id));
    }

    /// <summary>
    /// Calculates shortest between two nodes with Dijkstra algorithm
    /// </summary>
    /// <returns>Shortest path as list of node id's. 
    /// Null if shortest path doesn't exists</returns>
    public List<int> ShortestPath(int startId, int finishId) {
      if (!fNodes.Contains(startId) || !fNodes.Contains(finishId))
        throw new InvalidOperationException("Incorrect start/finish node params");
      //Current value marks of nodes
      var values = new Dictionary<int, double>();
      //Parts of shortest path: node -> previous node
      var paths = new Dictionary<int, int>();
      //List of checked nodes
      var check = new List<int>();
      //Queue of nodes for further processing
      Queue<int> queue = new Queue<int>();
      //Initialization
      foreach (int node in fNodes) {
        values.Add(node, double.PositiveInfinity);
        paths.Add(node, 0);
      }
      values[startId] = 0.0;

      for (int current = startId; ; ) {
        //Check links to neighbors starting with most low-costed 
        var links = fRoutes.Where(x => x.Node1 == current &&
          !check.Contains(x.Node2)).OrderBy(x => x.Cost);

        double way = values[current];
        //Update neighboring nodes value marks if need
        foreach (Route link in links) {
          queue.Enqueue(link.Node2);
          int linkedId = link.Node2;
          double nodeValue = link.Cost + way;
          if (values[linkedId] > nodeValue) {
            values[linkedId] = nodeValue;
            paths[linkedId] = current;
          }
        }
        //Add node to list of checked
        check.Add(current);
        if (queue.Count > 0)
          current = queue.Dequeue();
        else
          break;
      }
      //if value mark of finish node is infinity, then shortest way doesn't exist
      if (!values[finishId].Equals(double.PositiveInfinity)) {
        //Restore shortest path as list of nodes
        var nodes = new List<int>();
        for (int cur = finishId; paths.ContainsKey(cur); cur = paths[cur])
          nodes.Add(cur);
        nodes.Reverse();
        return nodes;
      }

      return null;
    }

    /// <summary>
    /// Deletes node from graph
    /// </summary>
    public void BlackOutNode(int nodeId) {
      fRoutes.RemoveAll(x => x.Node1 == nodeId || x.Node2 == nodeId);
      fNodes.Remove(nodeId);
    }
    #endregion
  }
}
