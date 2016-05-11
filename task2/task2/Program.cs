using System;

using task2.Graph;

namespace task2 {
  class Program {

    static void Main(string[] args) {
      System.Threading.Thread.CurrentThread.CurrentUICulture =
        new System.Globalization.CultureInfo("en-US");
      //Program needs one input param - path to file with xml data
      if (args.Length < 1)
        Console.WriteLine("Usage: task2 <filepath>\n" +
          " <filepath> - path to xml file with road-system data");
      else {
        string file = args[0];

        //Loading and validating xml
        XmlGraphCreator creator = new XmlGraphCreator();
        //Try to create graph and calc shortest way
        try {
          creator.CreateGraph(new XmlSource(file, Resources.Schema));
         
          if (creator.Start == null)
            Console.WriteLine("No start node");
          else if (creator.Finish == null)
            Console.WriteLine("No finish node");
          else {
            //Calc shortest way
            var result = creator.Graph.ShortestPath(creator.Start.Value, creator.Finish.Value);
            if (result != null)
              Console.WriteLine("Shortest path: {0}", string.Join(" -> ", result));
          } //else start && finish != null
        } catch (Exception e) {
          Console.WriteLine(e.Message);
        }//catch
        Console.ReadKey();
      }//else (args count > 0)
    }//Main

  }
}
