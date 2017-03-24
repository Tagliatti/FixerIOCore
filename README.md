# FixerIoCore
A wrapper for the [fixer.io](http://fixer.io) rates and currency conversion API for .NET Core compatible with .NETStandard 1.6+

## Install
On Nuget:

	PM> Install-Package FixerIoCore

## Using
Default values
Currency: EUR
Https: false

```c#
var fixerIoClient = new FixerIoClient();
```

Default currency
```c#
var fixerIoClient = new FixerIoClient(Symbol.USD);
```

Default currency and what quotes should be returned
```c#
var fixerIoClient = new FixerIoClient(Symbol.USD, new[] { Symbol.GBP, Symbol.EUR });
```

Default currency, what quotes should be returned and requests using https
```c#
var fixerIoClient = new FixerIoClient(Symbol.USD, new[] { Symbol.GBP, Symbol.EUR }, true);
```

Get latest rates
```c#
var quote = fixerIoClient.GetLatest();
```

Get historical rates for any day since 1999
```c#
var quote = fixerIoClient.GetForDate(new DateTime(2017, 3, 1));
```

Converting
```c#
var quote = fixerIoClient.Convert(Symbol.USD, Symbol.EUR);
```
or
```c#
var quote = fixerIoClient.Convert(Symbol.USD, Symbol.EUR, new DateTime(2017, 3, 1));
```

**All functions have the option of asynchronous call**
```c#
var quote = await fixerIoClient.ConvertAsync(Symbol.USD, Symbol.EUR);
```

### List of all available currencies

- AUD
- BGN
- BRL
- CAD
- CHF
- CNY
- CZK
- DKK
- EUR
- GBP
- HKD
- HRK
- HUF
- IDR
- ILS
- INR
- JPY
- KRW
- MXN
- MYR
- NOK
- NZD
- PHP
- PLN
- RON
- RUB
- SEK
- SGD
- THB
- TRY
- USD
- ZAR