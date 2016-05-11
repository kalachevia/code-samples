using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using NUnit.Framework;
using task2.Graph;

namespace task2.test {
  [TestFixture]
  public class Tests {

    [Test]
    [ExpectedException(typeof(InvalidOperationException))]
    public void graph_tests() {
      SimpleGraph graph = new SimpleGraph();
      //can't add route with node1 = node2
      graph.AddRoute(14, 1, 1);
      Assert.AreEqual(graph.RouteCount(), 0);
      Assert.AreEqual(false, graph.ContainsRoute(1, 1));
      //shortest path test
      graph.AddRoute(10, 1, 2);
      graph.AddRoute(12, 1, 3);
      graph.AddRoute(9, 2, 4);
      graph.AddRoute(20, 1, 4);
      Assert.AreEqual(graph.RouteCount(), 4);
      Assert.AreEqual(true, graph.ContainsRoute(1, 2));

      Assert.AreEqual(string.Join(",", graph.ShortestPath(1, 4)), "1,2,4");
      //Optimal path changed
      graph.AddRoute(6, 3, 4);
      Assert.AreEqual(string.Join(",", graph.ShortestPath(1, 4)), "1,3,4");
      //clear graph test
      graph.Clear();
      Assert.AreEqual(graph.RouteCount(), 0);
      //path doesn't exist: returns null
      graph.AddRoute(2, 1, 2);
      graph.AddRoute(2, 3, 4);
      Assert.AreEqual(null, graph.ShortestPath(1, 4));
      //unexisting nodes: throws exception
      graph.ShortestPath(5, 6);
    }

    [Test]
    public void graph_creation_tests() {
      var creator = new XmlGraphCreator();
      creator.CreateGraph(new MockXmlSource());

      Assert.AreEqual(true, creator.IsCreated);
      Assert.AreNotEqual(null, creator.Start);
      Assert.AreNotEqual(null, creator.Finish);
      Assert.AreEqual(1, creator.Start.Value);
      Assert.AreEqual(3, creator.Finish.Value);

      IGraph graph = creator.Graph;
      //Expected 4 routes because of 2 routes lead to crash node 
      Assert.AreEqual(4, graph.RouteCount());
      Assert.AreEqual(false, graph.ContainsRoute(3, 4));
      Assert.AreEqual(false, graph.ContainsRoute(4, 3));
     
      Assert.AreEqual("1,2,3",string.Join(",",graph.ShortestPath(1, 3)));
    }
  }
}
