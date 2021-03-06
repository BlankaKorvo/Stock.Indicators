﻿# Awesome Oscillator (AO)

Created by Bill Williams, the Awesome Oscillator (aka Super AO) is a measure of the gap between a fast and slow period modified moving average.
[[Discuss] :speech_balloon:](https://github.com/DaveSkender/Stock.Indicators/discussions/282 "Community discussion about this indicator")

![image](chart.png)

```csharp
// usage
IEnumerable<AwesomeResult> results =
  history.GetAwesome(fastPeriod, slowPeriod);  
```

## Parameters

| name | type | notes
| -- |-- |--
| `fastPeriod` | int | Number of periods (`F`) for the faster moving average.  Must be greater than 0.  Default is 5.
| `slowPeriod` | int | Number of periods (`S`) for the slower moving average.  Must be greater than `fastPeriod`.  Default is 34.

### Historical quotes requirements

You must have at least `S` periods of `history`.

`history` is an `IEnumerable<TQuote>` collection of historical price quotes.  It should have a consistent frequency (day, hour, minute, etc).  See [the Guide](../../docs/GUIDE.md) for more information.

## Response

```csharp
IEnumerable<AwesomeResult>
```

The first period `S-1` periods will have `null` values since there's not enough data to calculate.  We always return the same number of elements as there are in the historical quotes.

### AwesomeResult

| name | type | notes
| -- |-- |--
| `Date` | DateTime | Date
| `Oscillator` | decimal | Awesome Oscillator
| `Normalized` | decimal | `100 × Oscillator ÷ (median price)`

## Example

```csharp
// fetch historical quotes from your feed (your method)
IEnumerable<Quote> history = GetHistoryFromFeed("MSFT");

// calculate
IEnumerable<AwesomeResult> results = history.GetAwesome(5,34);

// use results as needed
AwesomeResult r = results.LastOrDefault();
Console.WriteLine("AO on {0} was {1}", r.Date, r.Oscillator);
```

```bash
AO on 12/31/2018 was -17.77
```

## Utilities for results

| name | description
| -- |--
| `.Find()` | Find a specific result by date.  See [guide](../../docs/UTILITIES.md#find-indicator-result-by-date)
| `.PruneWarmupPeriods()` | Remove the recommended warmup periods.  See [guide](../../docs/UTILITIES.md#prune-warmup-periods)
| `.PruneWarmupPeriods(qty)` | Remove a specific quantity of warmup periods.  See [guide](../../docs/UTILITIES.md#prune-warmup-periods)
