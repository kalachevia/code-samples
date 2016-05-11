using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace task1.Models {
  /// <summary>
  /// Represents the exchange rate's data
  /// </summary>
  public class RateEntry {
    [Key,Column(Order=0)]
    public DateTime Date { get; set; }
    [Key,Column(Order=1)]
    public string Currency { get; set; }
    public double Value { get; set; }

    public static RateEntry Create(DateTime date, string currency, double value) {
      return new RateEntry() { Date = date, Currency = currency, Value = value };
    }
  }
}
