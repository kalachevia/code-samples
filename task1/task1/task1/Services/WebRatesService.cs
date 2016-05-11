using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;

using Newtonsoft.Json.Linq;

namespace task1.Services {
  /// <summary>
  /// Downloads rate values from web service
  /// </summary>
  public sealed class WebRatesService : IExternalRatesService {

    private WebClient fClient;
    private string fUrl;
    private bool fDisposed = false;

    public WebRatesService(string url) {
      if (url == null)
        throw new ArgumentNullException();

      fUrl = url;
      fClient = new System.Net.WebClient();
    }

    /// <summary>
    /// Returns rate's value, null if unseccessful
    /// </summary>
    public double? Get(DateTime date, string currency) {
      double? value = null;
      try {
        string json = fClient.DownloadString(string.Format(fUrl, date.ToString("yyyy-MM-dd")));
        JObject jData = JObject.Parse(json);
        var rate = (jData.Property("rates").Value as JObject)
          .Properties()
          .FirstOrDefault(x => x.Name.Equals(currency));
        if (rate != null)
          value = rate.Value.Value<double>();
      } catch {
      }
      return value;
    }

    #region IDisposable
    public void Dispose() {
      if (!fDisposed) {
        fClient.Dispose();
        fDisposed = true;
      }
      GC.SuppressFinalize(this);
    }
    #endregion
  }
}