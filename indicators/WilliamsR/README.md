﻿# Williams %R

Created by Larry Williams, the [Williams %R](https://en.wikipedia.org/wiki/Williams_%25R) momentum indicator is a stochastic oscillator with scale of -100 to 0.  It is exactly the same as the Fast variant of [Stochastic Oscillator](../Stochastic/README.md), but with a different scaling.
[[Discuss] :speech_balloon:](https://github.com/DaveSkender/Stock.Indicators/discussions/229 "Community discussion about this indicator")

![image](chart.png)

```csharp
// usage
IEnumerable<WilliamsResult> results =
  history.GetWilliamsR(lookbackPeriod);  
```

## Parameters

| name | type | notes
| -- |-- |--
| `lookbackPeriod` | int | Number of periods (`N`) in the lookback period.  Must be greater than 0.  Default is 14.

### Historical quotes requirements

You must have at least `N` periods of `history`.

`history` is an `IEnumerable<TQuote>` collection of historical price quotes.  It should have a consistent frequency (day, hour, minute, etc).  See [the Guide](../../docs/GUIDE.md) for more information.

## Response

```csharp
IEnumerable<WilliamsResult>
```

The first `N-1` periods will have `null` Oscillator values since there's not enough data to calculate.  We always return the same number of elements as there are in the historical quotes.

### WilliamsResult

| name | type | notes
| -- |-- |--
| `Date` | DateTime | Date
| `WilliamsR` | decimal | Oscillator over prior `N` lookback periods

## Example

```csharp
// fetch historical quotes from your feed (your method)
IEnumerable<Quote> history = GetHistoryFromFeed("SPY");

// calculate WilliamsR(14)
IEnumerable<WilliamsResult> results = history.GetWilliamsR(14);

// use results as needed
WilliamsResult result = results.LastOrDefault();
Console.WriteLine("Williams %R on {0} was {1}", result.Date, result.WilliamsR);
```

```bash
Williams %R on 12/31/2018 was -52.0
```

## Utilities for results

| name | description
| -- |--
| `.Find()` | Find a specific result by date.  See [guide](../../docs/UTILITIES.md#find-indicator-result-by-date)
| `.PruneWarmupPeriods()` | Remove the recommended warmup periods.  See [guide](../../docs/UTILITIES.md#prune-warmup-periods)
| `.PruneWarmupPeriods(qty)` | Remove a specific quantity of warmup periods.  See [guide](../../docs/UTILITIES.md#prune-warmup-periods)
