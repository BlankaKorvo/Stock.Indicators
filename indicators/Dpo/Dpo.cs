using System;
using System.Collections.Generic;
using System.Linq;

namespace Skender.Stock.Indicators
{

    public static partial class Indicator
    {
        // SIMPLE MOVING AVERAGE
        /// 
        /// 
        public static IEnumerable<DpoResult> GetDpo<TQuote>(
            IEnumerable<TQuote> history,
            int lookbackPeriod
            )
            where TQuote : IQuote
        {
            // sort history
            List<TQuote> historyList = history.Sort();

            // check parameter arguments
            ValidateDpo(history, lookbackPeriod);

            // initialize
            List<SmaResult> sma = new List<SmaResult>(historyList.Count);
            List<DpoResult> dpo = new List<DpoResult>(historyList.Count);

            //barsback
            int barsback = Convert.ToInt32(Decimal.Round(lookbackPeriod / 2 + 1));

            for (int i = 0; i < historyList.Count; i++)
            {
                TQuote h = historyList[i];
                int index = i + 1;

                SmaResult smaResult = new SmaResult
                {
                    Date = h.Date
                };

                DpoResult dpoResult = new DpoResult
                {
                    Date = h.Date
                };

                if (index >= lookbackPeriod)
                {
                    decimal sumSma = 0m;
                    for (int p = index - lookbackPeriod; p < index; p++)
                    {
                        TQuote d = historyList[p];
                        sumSma += d.Close;
                    }

                    smaResult.Sma = sumSma / lookbackPeriod;
                }
                sma.Add(smaResult);
                if (index >= lookbackPeriod + barsback)
                {
                    dpoResult.Dpo = h.Close - sma[index - barsback - 1].Sma;
                }
                dpo.Add(dpoResult);
            }
            return dpo;
        }


        private static void ValidateDpo<TQuote>(
            IEnumerable<TQuote> history,
            int lookbackPeriod)
            where TQuote : IQuote
        {

            // check parameter arguments
            if (lookbackPeriod <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(lookbackPeriod), lookbackPeriod,
                    "Lookback period must be greater than 0 for SMA.");
            }

            // check history
            int qtyHistory = history.Count();
            int minHistory = lookbackPeriod + lookbackPeriod / 2 + 1;
            if (qtyHistory < minHistory)
            {
                string message = "Insufficient history provided for SMA.  " +
                    string.Format(
                        EnglishCulture,
                    "You provided {0} periods of history when at least {1} is required.",
                    qtyHistory, minHistory);

                throw new BadHistoryException(nameof(history), message);
            }
        }
    }
}
