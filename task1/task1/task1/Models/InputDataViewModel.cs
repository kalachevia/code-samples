using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace task1.Models {
  /// <summary>
  /// Represents the data, which is displayed on main page 
  /// </summary>
  public class IndexDataViewModel {

    public IndexDataViewModel() {
      Currencies = new SelectList(new [] { "EUR", "USD", "RUB", "GBP", "JPY" });
      Currency = "EUR";
      BeginDate = DateTime.Now;
      EndDate = DateTime.Now;
    }

    [
      DataType(DataType.Date),
      Required(ErrorMessage="*"),
      DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true),
      Display(Name="Begin date")
    ]
    public DateTime? BeginDate { get; set; }

    [
      DataType(DataType.Date),
      Required(ErrorMessage = "*"),
      DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true),
      Display(Name="End date")
    ]
    public DateTime? EndDate { get; set; }
    public string Currency { get; set; }
    public string BaseCurrency { get; set; }

    public SelectList Currencies { get; set; }
  }
}