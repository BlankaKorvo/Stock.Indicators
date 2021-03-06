# STARC Bands

Created by Manning Stoller, [Stoller Average Range Channel (STARC) Bands](https://en.wikipedia.org/wiki/StarcBands_channel), are based on an SMA centerline and ATR band widths.  See also [Keltner Channels](../Keltner/README.md#content) for an EMA centerline equivalent.
[[Discuss] :speech_balloon:](https://github.com/DaveSkender/Stock.Indicators/discussions/292 "Community discussion about this indicator")

![image](chart.png)

```csharp
// usage
IEnumerable<StarcBandsResult> results = 
  history.GetStarcBands(smaPeriod, multiplier, atrPeriod);  
```

## Parameters

| name | type | notes
| -- |-- |--
| `smaPeriod` | int | Number of lookback periods (`S`) for the center line moving average.  Must be greater than 1 to calculate and is typically between 5 and 10.
| `multiplier` | decimal | ATR Multiplier. Must be greater than 0.  Default is 2.
| `atrPeriod` | int | Number of lookback periods (`A`) for the Average True Range.  Must be greater than 1 to calculate and is typically the same value as `smaPeriod`.  Default is 10.

### Historical quotes requirements

You must have at least `S` or `A+100` periods of `history`, whichever is more.  Since this uses a smoothing technique, we recommend you use at least `A+150` data points prior to the intended usage date for better precision.

`history` is an `IEnumerable<TQuote>` collection of historical price quotes.  It should have a consistent frequency (day, hour, minute, etc).  See [the Guide](../../docs/GUIDE.md) for more information.

## Response

```csharp
IEnumerable<StarcBandsResult>
```

The first `N-1` periods will have `null` values since there's not enough data to calculate, where `N` is the greater of `S` or `A`.  We always return the same number of elements as there are in the historical quotes.

:warning: **Warning**: The first `A+150` periods will have decreasing magnitude, convergence-related precision errors that can be as high as ~5% deviation in indicator values for earlier periods.

### StarcBandsResult

| name | type | notes
| -- |-- |--
| `Date` | DateTime | Date
| `UpperBand` | decimal | Upper STARC band
| `Centerline` | decimal | SMA of Close price
| `LowerBand` | decimal | Lower STARC band

## Example

```csharp
// fetch historical quotes from your feed (your method)
IEnumerable<Quote> history = GetHistoryFromFeed("SPY");

// calculate StarcBands(20)
IEnumerable<StarcBandsResult> results = history.GetStarcBands(20,2.0,10);

// use results as needed
StarcBandsResult result = results.LastOrDefault();
Console.WriteLine("Upper STARC Band on {0} was ${1}", result.Date, result.UpperBand);
```

```bash
Upper STARC Band on 12/31/2018 was $262.19
```

## Utilities for results

| name | description
| -- |--
| `.Find()` | Find a specific result by date.  See [guide](../../docs/UTILITIES.md#find-indicator-result-by-date)
| `.PruneWarmupPeriods()` | Remove the recommended warmup periods.  See [guide](../../docs/UTILITIES.md#prune-warmup-periods)
| `.PruneWarmupPeriods(qty)` | Remove a specific quantity of warmup periods.  See [guide](../../docs/UTILITIES.md#prune-warmup-periods)
