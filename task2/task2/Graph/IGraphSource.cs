using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace task2.Graph {
  /// <summary>
  /// Provides data to create graph
  /// </summary>
  interface IGraphSource<T> where T : class {
    T GetData();
  }
}
