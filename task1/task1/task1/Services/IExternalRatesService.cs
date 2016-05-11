using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace task1.Services {
  /// <summary>
  /// Provides functionality of read rate's value
  /// </summary>
  interface IExternalRatesService : IDisposable {
    double? Get(DateTime date, string currency);
  }
}
