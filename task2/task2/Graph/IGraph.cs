using System;
using System.Collections.Generic;

namespace task2.Graph {
  /// <summary>
  /// Provides functionality for creating graph structures and calc shortest way
  /// </summary>
  interface IGraph {
    /// <summary>
    /// Adds route to graph
    /// </summary>
    void AddRoute(int cost, int node1Id, int node2Id);
    /// <summary>
    /// Clears all graph routes and nodes
    /// </summary>
    void Clear();
    /// <summary>
    /// Checks existance of specified route
    /// </summary>
    bool ContainsRoute(int from, int to);
    /// <summary>
    /// Returns count of routes
    /// </summary>
    int RouteCount();
    /// <summary>
    /// Calculates shortest way between to nodes
    /// </summary>
    List<int> ShortestPath(int startId, int finishId);
    /// <summary>
    /// Clears all routes, connected with node
    /// </summary>
    void BlackOutNode(int nodeId);
  }
}
