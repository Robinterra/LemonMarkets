# LemonMarkets
C# Library to accecss the lemon markets REST API


## Example
```csharp
using System;
using LemonMarkets;
using LemonMarkets.Interfaces;
using LemonMarkets.Models;
using LemonMarkets.Models.Responses;

string apiKey = "e...Q";
ILemonApi lemonApi = LemonApi.Build (apiKey, LemonApi.MoneyTradingMode.Paper);

LemonResults<Space>? result = await lemonApi.Spaces.GetAsync ();
if ( result == null ) return 1;

Console.WriteLine(result.Status);

LemonResults<Quote>? results = await lemonApi.Quotes.GetAsync (new ("DE0008404005"));
if ( results == null ) return 2;

Console.WriteLine ( results.Status );

return 0;
```