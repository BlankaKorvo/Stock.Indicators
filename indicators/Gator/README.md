# Gator Oscillator

Created by Bill Williams, the Gator Oscillator is an expanded view of [Williams Alligator](../Alligator/README.md#content).
[[Discuss] :speech_balloon:](https://github.com/DaveSkender/Stock.Indicators/discussions/385 "Community discussion about this indicator")

![image](chart.png)

```csharp
// usage
IEnumerable<GatorResult> results =
  history.GetGator();
```

## Historical quotes requirements

You must have at least 115 periods of `history`. Since this uses a smoothing technique, we recommend you use at least 265 data points prior to the intended usage date for better precision.

`history` is an `IEnumerable<TQuote>` collection of historical price quotes.  It should have a consistent frequency (day, hour, minute, etc).  See [the Guide](../../docs/GUIDE.md) for more information.

## Response

```csharp
IEnumerable<GatorResult>
```

The first 10-20 periods will have `null` values since there's not enough data to calculate.  We always return the same number of elements as there are in the historical quotes.

:warning: **Warning**: The first 150 periods will have decreasing magnitude, convergence-related precision errors that can be as high as ~5% deviation in indicator values for earlier periods.

### GatorResult

| name | type | notes
| -- |-- |--
| `Date` | DateTime | Date
| `Upper` | decimal | Absolute value of Alligator `Jaw-Teeth`
| `Lower` | decimal | Absolute value of Alligator `Lips-Teeth`
| `UpperIsExpanding` | boolean | Upper value is growing
| `LowerIsExpanding` | boolean | Lower value is growing

## Example

```csharp
// fetch historical quotes from your feed (your method)
IEnumerable<Quote> history = GetHistoryFromFeed("MSFT");

// calculate the Gator Oscillator
IEnumerable<GatorResult> results = history.GetGator();

// use results as needed
GatorResult result = results.LastOrDefault();
Console.WriteLine("Upper on {0} was {1}", result.Date, result.Upper);
Console.WriteLine("Lower on {0} was {1}", result.Date, result.Lower);
```

```bash
Upper on 12/31/2018 was 7.45
Lower on 12/31/2018 was -9.24
```

## Utilities for results

| name | description
| -- |--
| `.Find()` | Find a specific result by date.  See [guide](../../docs/UTILITIES.md#find-indicator-result-by-date)
| `.PruneWarmupPeriods()` | Remove the recommended warmup periods.  See [guide](../../docs/UTILITIES.md#prune-warmup-periods)
| `.PruneWarmupPeriods(qty)` | Remove a specific quantity of warmup periods.  See [guide](../../docs/UTILITIES.md#prune-warmup-periods)
