using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Configuration;

using DotNet.Highcharts;
using DotNet.Highcharts.Options;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Enums;

using task1.DataAccess;
using task1.Models;

using task1.Services;

namespace task1.Controllers {
  public class HomeController : Controller {

    private readonly string baseCurrency;
    private readonly string altCurrency;

    private IRatesRepository  fRatesRepository;
    private IExternalRatesService fRatesService;

    public HomeController() {
      baseCurrency = WebConfigurationManager.AppSettings["BaseCurrency"];
      altCurrency = WebConfigurationManager.AppSettings["AltCurrency"];
      if (baseCurrency == null || altCurrency == null)
        throw new ArgumentNullException();

      string url = WebConfigurationManager.AppSettings["ExchangeRatesUrl"];
      fRatesService = new WebRatesService(url);

      fRatesRepository = new RatesRepository();
    }

    public ActionResult Index() {
      return View(new IndexDataViewModel());
    }

    [HttpPost]
    public ActionResult Index(IndexDataViewModel data) {
      Validate(data);
      if (ModelState.IsValid) {

        IList<RateEntry> rates = null;
        string[] xAxis;
        object[] yAxis;

        string currency = data.Currency;
        //If user pick USD currency then we take rates in EUR and revert them 
        if (currency.Equals(baseCurrency)) {
          data.BaseCurrency = currency = altCurrency;

          rates = GetRates(altCurrency, data.BeginDate.Value, data.EndDate.Value);
          yAxis = rates.Select(x => (object)(1 / x.Value).ToString("#.000")).ToArray();
        }
        else {
          //If user pick one of the other currencies (not USD), we can take rates in USD base
          data.BaseCurrency = baseCurrency;
          rates = GetRates(currency, data.BeginDate.Value, data.EndDate.Value);

          yAxis = rates.Select(x => (object)x.Value.ToString("#.000")).ToArray();
        }

        xAxis = rates.Select(x => x.Date.ToString("MM/dd/yy")).ToArray();

        if (rates.Count > 0)
         ViewBag.Chart = CreateChart(xAxis, yAxis, data);
      }
      return View(data);
    }

    /// <summary>
    /// Gets rates from db cache or downloads them from web service. 
    /// Returns list or rates for specific period.
    /// </summary>
    private IList<RateEntry> GetRates(string currency, DateTime begin, DateTime end) {
      //Getting list of dates in specific range
      var dates = begin.DateRange(end);
      //Getting rates which are stored in db
      var rates = fRatesRepository.GetRates(currency, begin, end);
      
      //Detect rates, that aren't in db
      var newDates = dates.Except(rates.Select(x => x.Date)).ToList();
        if (newDates.Count > 0) {
          foreach (DateTime date in newDates) {
            //Download rates from web service
            double? rateValue = fRatesService.Get(date, currency);
            if (!rateValue.HasValue)
              break;
            //Save rate in database
            var rate = RateEntry.Create(date, currency, rateValue.Value);
            fRatesRepository.AddRate(rate);
            rates.Add(rate);
          }

          fRatesRepository.Save();
        }

        return rates.OrderBy(x => x.Date).ToList();
    }

    /// <summary>
    /// Validates input data
    /// </summary>
    private void Validate(IndexDataViewModel data) {
      if (!data.BeginDate.HasValue)
        ModelState.AddModelError("", "Begin date is required");

      if (!data.EndDate.HasValue)
        ModelState.AddModelError("", "End date is required");

      if (data.EndDate > DateTime.Today)
        ModelState.AddModelError("", "End date shouldn't belong to the future");

      if (data.BeginDate > DateTime.Today)
        ModelState.AddModelError("", "Begin date shouldn't belong to the future");

      if (data.BeginDate > data.EndDate)
        ModelState.AddModelError("", "Begin date shouldn't exceed the end date");

      if (ModelState.IsValid && data.BeginDate.Value.MonthCount(data.EndDate.Value) > 2)
        ModelState.AddModelError("", "Selected period should not exceed 2 months");
    }

    /// <summary>
    /// Creates and formats exchange rates chart
    /// </summary>
    private Highcharts CreateChart(string[] xAxis, object[] yAxis, IndexDataViewModel data) {
      Highcharts chart = new DotNet.Highcharts.Highcharts("chart")
        .SetTooltip(new Tooltip { PointFormat = string.Format("1 {0} = {{point.y}} {1}", data.BaseCurrency, data.Currency) })
        .SetXAxis(new XAxis {
          TickInterval = xAxis.Length / 20 + 1,
          TickmarkPlacement = Placement.On,
          Categories = xAxis,
          Title = new XAxisTitle { Text = "Date" },
          Labels = new XAxisLabels {
            Align = HorizontalAligns.Center,
            Style = "color: 'black', fontSize: '10px', fontFamily: 'Arial'"
          }
        })
        .SetYAxis(new YAxis {
          Title = new YAxisTitle { Text = "Value" },
          Labels = new YAxisLabels {
            Format = "{value:.2f}"
              }
        })
        .SetSeries(new Series {
          Data = new Data(yAxis),
          Name = data.Currency
        })
        .SetTitle(new Title() {
          Text = string.Format("{0} rates, {1:MM/dd/yy}-{2:MM/dd/yy}, base: {3}",
           data.Currency, data.BeginDate.Value, data.EndDate.Value, data.BaseCurrency)
        });

      return chart;
    }

    #region IDisposable
    protected override void Dispose(bool disposing) {
      fRatesRepository.Dispose();
      fRatesService.Dispose();
      base.Dispose(disposing);
    }
    #endregion
  }
}
