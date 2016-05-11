using System;
using System.Collections.Generic;
using System.Linq;

namespace task1 {
  public static class DateExtenstions {
    /// <summary>
    /// Creates a list of dates, which are between two dates
    /// </summary>
    public static IEnumerable<DateTime> DateRange(this DateTime from, DateTime to) {
      if (to < from)
        throw new ArgumentException();

      return Enumerable.Range(0, 1 + to.Subtract(from).Days)
        .Select(offset => from.AddDays(offset));
    }

    /// <summary>
    /// Calculates the approximate count of months between two dates
    /// </summary>
    public static double MonthCount(this DateTime from, DateTime to) {
      if (to < from)
        throw new ArgumentException();

      return (to.Year - from.Year) * 12 +
        to.Month - from.Month + (to.Day - from.Day) / 30.4;
    }
  }
}