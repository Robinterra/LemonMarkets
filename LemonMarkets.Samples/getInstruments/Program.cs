using LemonMarkets;
using LemonMarkets.Interfaces;

using LemonMarkets.Models;
using LemonMarkets.Models.Enums;
using LemonMarkets.Models.Requests.Trading;
using LemonMarkets.Models.Responses;

// Du kannst dein api key auch direkt hier einfügen, dafür einfach zeile 10 auskommentieren und dann zeile 11 verwenden.
string? tradingKey = Environment.GetEnvironmentVariable("tradingKey");
//string apiKey = "hier api key einfügen";
string? marketKey = Environment.GetEnvironmentVariable("marketKey");
//string marketKey = "hier api key einfügen";

// Ändere zu Money, wenn du mit echten Geld handelst. Für diesen Beispiel nutze ich den mode auf Paper.
LemonApi.MoneyTradingMode tradingMode = LemonApi.MoneyTradingMode.Paper;

if (string.IsNullOrEmpty(tradingKey))
{
    await Console.Error.WriteLineAsync("es wurde kein api key über die Enviroment Variabeln angegeben");

    return 1;
}

if (string.IsNullOrEmpty(marketKey))
{
    await Console.Error.WriteLineAsync("es wurde kein api key über die Enviroment Variabeln angegeben");

    return 1;
}

// Ich empfehle aus kompatibilitätsgründen die Build methode zu verwenden,
// da ich vorhabe den Ctor noch regelmäßig zu verändern.
// Bei der Build methode muss der Api Key und den Trade Mode übergeben werden.
// Beim abfragen von daten ist es nicht relevant ob Paper oder Money eingestellt ist, da es nur eine API für die datenabfrage existiert.
ILemonApi lemonApi = LemonApi.Build(marketKey, tradingKey, tradingMode);

#region getInstruments

InstrumentSearchFilter searchFilter = new(mic: "XMUN");

LemonResults<Instrument> resultInstruments = await lemonApi.Instruments.GetAsync(searchFilter);

// Sollte bei vom HttpClient oder beim Deserialiseren eine exception hochkommen gebe ich die Exception über das Result Objekt zurück
if (resultInstruments.Exception is not null)
{
    await Console.Error.WriteLineAsync($"Leider ist eine Exception aufgetreten: {resultInstruments.Exception}");

    return 2;
}

// Sollte IsSuccess nicht true sein, dann konnte keine Instruments abgerufen werden.
if (!resultInstruments.IsSuccess || resultInstruments.Results is null)
{
    await Console.Error.WriteLineAsync($"Instruments konnten nicht abgerufen werden. HttpCode: '{resultInstruments.HttpCode}', Status: '{resultInstruments.Status}', ErrorCode: '{resultInstruments.Error_code}', ErrorMessage: '{resultInstruments.Error_message}'.");

    return 3;
}

Console.WriteLine($"Die Instruments konnten abgerufen werden");

int count = 0;
await foreach (Instrument instrument in resultInstruments)
{
    count++;
    Console.WriteLine($"Isin: {instrument.ISIN}, Name: {instrument.Name}");
}

Console.WriteLine($"Es wurden {count} Instruments geladen");

#endregion getInstruments

return 0;