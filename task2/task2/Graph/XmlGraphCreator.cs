using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.IO;

namespace task2.Graph {
  /// <summary>
  /// Creates graph from xml data
  /// </summary>
  class XmlGraphCreator {
    #region Fields
    private IGraph fGraph = null;
    private int? fStart   = null;
    private int? fFinish  = null;
    #endregion

    #region Properties
    /// <summary>
    /// Created graph
    /// </summary>
    public IGraph Graph {
      get { return fGraph; }
    }

    /// <summary>
    /// Graph's start node id
    /// </summary>
    public int? Start {
      get { return fStart; }
    }

    /// <summary>
    /// Graph's finish node id
    /// </summary>
    public int? Finish {
      get { return fFinish; }
    }

    /// <summary>
    /// Determines if graph was already created
    /// </summary>
    public bool IsCreated {
      get { return fGraph != null; }
    }
    #endregion

    #region Methods
    /// <summary>
    /// Creates SimpleGraph from xml data
    /// </summary>
    public void CreateGraph(IGraphSource<XDocument> src) {
      if (IsCreated)
        throw new InvalidOperationException("Graph was already created");

      var doc = src.GetData();

      int? start = null, finish = null;
      IGraph graph = new SimpleGraph();
      //Adding routes to graph 
      doc.XPathSelectElements("/graph/node").ToList().ForEach(node => {
        int id = int.Parse(node.Attribute("id").Value);

        node.XPathSelectElements("//node[@id='" + id.ToString() + "']/link").ToList().ForEach(link => {
          graph.AddRoute(
            int.Parse(link.Attribute("weight").Value),
            int.Parse(link.Attribute("ref").Value),
            id
          );
        });
        //Check node role for determining start/finish nodes
        var role = node.Attribute("role");
        if (role != null)
          if (role.Value.Equals("start"))
            start = id;
          else if (role.Value.Equals("finish"))
            finish = id;
      });
      //If data contains crash nodes, we just delete them and all related routes from graph
      doc.XPathSelectElements("/graph/node[@status = 'crash']").ToList().ForEach(node => {
        graph.BlackOutNode(int.Parse(node.Attribute("id").Value));
      });

      fGraph = graph;
      fStart = start;
      fFinish = finish;
    }

    #endregion
  }
}
