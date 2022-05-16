using LemonMarkets;
using LemonMarkets.Interfaces;

using LemonMarkets.Models;
using LemonMarkets.Models.Enums;
using LemonMarkets.Models.Requests.Trading;
using LemonMarkets.Models.Responses;

// Du kannst dein api key auch direkt hier einfügen, dafür einfach zeile 10 auskommentieren und dann zeile 11 verwenden.
string? apiKey = Environment.GetEnvironmentVariable("apikey");
//string apiKey = "hier api key einfügen";

// Ändere zu Money, wenn du mit echten Geld handelst. Für diesen Beispiel nutze ich den mode auf Real.
LemonApi.MoneyTradingMode tradingMode = LemonApi.MoneyTradingMode.Real;

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

ILivestreamService livestreamService = lemonApi.Livestream;

await livestreamService.AddSubscriptionOnIsin("DE0008404005");
await livestreamService.AddSubscriptionOnIsin("DE0006231004");

await livestreamService.ConnectAndSubscribeOnStream
(
    async quote =>
        await Console.Out.WriteLineAsync($"Isin: {quote.Isin} (Bid: {quote.Bid} / Ask: {quote.Ask})"),
    () => Task.CompletedTask
);

while (true)
{
    await Task.Delay(100);
}
