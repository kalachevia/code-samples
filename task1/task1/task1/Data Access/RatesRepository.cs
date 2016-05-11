using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using task1.Models;

namespace task1.DataAccess {
  /// <summary>
  /// Provides rates access and create functions
  /// </summary>
  public class RatesRepository : IRatesRepository, IDisposable {
    private ExchangeRatesContext fContext;
    private bool fDisposed = false;

    public RatesRepository() : this(new ExchangeRatesContext()) {
    }

    public RatesRepository(ExchangeRatesContext context) {
      fContext = context;
    }

    /// <summary>
    /// Returns list of rates for specific period and currency from db
    /// </summary>
    public List<RateEntry> GetRates(string currency, DateTime begin, DateTime end) {
      return fContext.RateEntries
        .Where(entry => entry.Date >= begin && entry.Date <= end && entry.Currency.Equals(currency))
        .ToList();
    }

    /// <summary>
    /// Inserts new rate in repository
    /// </summary>
    public void AddRate(RateEntry rate) {
      fContext.RateEntries.Add(rate);
    }

    /// <summary>
    /// Saves all changes to database
    /// </summary>
    public void Save() {
      fContext.SaveChanges();
    }

    #region IDisposable
    protected virtual void Dispose(bool disposing) {
      if (!fDisposed) {
        if (disposing) {
          fContext.Dispose();
        }
        fDisposed = true;
      }
    }

    public void Dispose() {
      Dispose(true);
      GC.SuppressFinalize(this);
    }
    #endregion
  }
}