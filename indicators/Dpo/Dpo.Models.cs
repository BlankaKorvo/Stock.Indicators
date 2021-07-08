using System;

namespace Skender.Stock.Indicators
{
    //public enum Dpo
    //{
    //    tradingView,
    //    Wiki
    //}

    [Serializable]
    public class DpoResult : ResultBase
    {
        public decimal? Dpo { get; set; }
        //public Dpo DpoVersion { get; set; }
        // simple moving average
        //public decimal? Mad { get; set; }  // mean absolute deviation
        //public decimal? Mse { get; set; }  // mean square error
        //public decimal? Mape { get; set; } // mean absolute percentage error
    }
}
