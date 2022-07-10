# LemonMarkets
[![.NET](https://github.com/Robinterra/LemonMarkets/actions/workflows/dotnet-unittest.yml/badge.svg)](https://github.com/Robinterra/LemonMarkets/actions/workflows/dotnet-unittest.yml)
[![Deploy](https://github.com/Robinterra/LemonMarkets/actions/workflows/dotnet-deployment2nuget.yml/badge.svg)](https://github.com/Robinterra/LemonMarkets/actions/workflows/dotnet-deployment2nuget.yml)

## Description
C# Library to accecss the lemon markets REST API

# Nuget Package
 `https://www.nuget.org/packages/LemonMarkets/`

## Example
Weitere Examples sind im sample Ordner zu finden (darunter wie eine Order erstellt und aktiviert wird).

```csharp
using System;
using LemonMarkets;
using LemonMarkets.Interfaces;
using LemonMarkets.Models;
using LemonMarkets.Models.Responses;

string marketKey = "e...T";
string tradingKey = "e...Q";
ILemonApi lemonApi = LemonApi.Build (marketKey, tradingKey, LemonApi.MoneyTradingMode.Paper);

LemonResults<Order>? result = await lemonApi.Orders.GetAsync();
if ( result == null ) return 1;

Console.WriteLine(result.Status);

LemonResults<Quote>? results = await lemonApi.Quotes.GetLatestAsync (new ("DE0008404005"));
if ( results == null ) return 2;

Console.WriteLine ( results.Status );

return 0;
```
