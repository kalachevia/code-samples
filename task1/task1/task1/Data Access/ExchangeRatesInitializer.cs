using System.Data.Entity;

namespace task1.DataAccess {
  /// <summary>
  /// Recreates db if model class RateEntry changes
  /// </summary>
  public class ExchangeRatesInitializer : DropCreateDatabaseIfModelChanges<ExchangeRatesContext> {
    protected override void Seed(ExchangeRatesContext context) {
      base.Seed(context);
    }
  }
}