using System.Data.Entity;

using task1.Models;

namespace task1.DataAccess {
  /// <summary>
  /// Db-access entry point
  /// </summary>
  public class ExchangeRatesContext : DbContext  {
    public DbSet<RateEntry> RateEntries { get; set; }
  }
}