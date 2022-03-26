using LemonMarkets;
using LemonMarkets.Interfaces;

using LemonMarkets.Models;
using LemonMarkets.Models.Enums;
using LemonMarkets.Models.Requests.Trading;
using LemonMarkets.Models.Responses;

// Du kannst dein api key auch direkt hier einfügen, dafür einfach zeile 10 auskommentieren und dann zeile 11 verwenden.
string? apiKey = Environment.GetEnvironmentVariable("apikey");
//string apiKey = "hier api key einfügen";

// Ändere zu Money, wenn du mit echten Geld handelst. Für diesen Beispiel nutze ich den mode auf Paper.
LemonApi.MoneyTradingMode tradingMode = LemonApi.MoneyTradingMode.Paper;

if (string.IsNullOrEmpty(apiKey))
{
    await Console.Error.WriteLineAsync("es wurde kein api key über die Enviroment Variabeln angegeben");

    return 1;
}

// Ich empfehle aus kompatibilitätsgründen die Build methode zu verwenden,
// da ich vorhabe den Ctor noch regelmäßig zu verändern.
// Bei der Build methode muss der Api Key und den Trade Mode übergeben werden.
// Beim abfragen von daten ist es nicht relevant ob Paper oder Money eingestellt ist, da es nur eine API für die datenabfrage existiert.
ILemonApi lemonApi = LemonApi.Build(apiKey, tradingMode);

#region getQuotes

List<string> isins = new List<string> { "DE0006231004", "DE0008404005" };

QuoteLatestSearchFilter searchFilter = new(isins, mic: "XMUN");

LemonResults<Quote> resultQuotes = await lemonApi.Quotes.GetLatestAsync(searchFilter);

// Sollte bei vom HttpClient oder beim Deserialiseren eine exception hochkommen gebe ich die Exception über das Result Objekt zurück
if (resultQuotes.Exception is not null)
{
    await Console.Error.WriteLineAsync($"Leider ist eine Exception aufgetreten: {resultQuotes.Exception}");

    return 2;
}

// Sollte IsSuccess nicht true sein, dann konnte keine Quotes abgerufen werden.
if (!resultQuotes.IsSuccess || resultQuotes.Results is null)
{
    await Console.Error.WriteLineAsync($"Quotes konnten nicht abgerufen werden. HttpCode: '{resultQuotes.HttpCode}', Status: '{resultQuotes.Status}', ErrorCode: '{resultQuotes.Error_code}', ErrorMessage: '{resultQuotes.Error_message}'.");

    return 3;
}

Console.WriteLine($"Die Quotes konnten abgerufen werden");

foreach (Quote quote in resultQuotes.Results)
{
    Console.WriteLine($"Isin: {quote.Isin} (Bid: {quote.Bid} / Ask: {quote.Ask})");
}

#endregion getQuotes

return 0;