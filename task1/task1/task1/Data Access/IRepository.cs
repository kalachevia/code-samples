using System;
using System.Collections.Generic;

using task1.Models;

namespace task1.DataAccess {
  /// <summary>
  /// Provides CRUD operations for rates
  /// </summary>
  interface IRatesRepository : IDisposable {
    void AddRate(RateEntry rate);
    void Save();

    List<RateEntry> GetRates(string currency, DateTime begin, DateTime end);
  }
}
