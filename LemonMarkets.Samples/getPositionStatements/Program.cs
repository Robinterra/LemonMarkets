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
ILemonApi? lemonApi = LemonApi.Build(apiKey, tradingMode);

#region PositionStatements

LemonResults<Statement> result = await lemonApi!.PositionStatements.GetAsync();

// Sollte bei vom HttpClient oder beim Deserialiseren eine exception hochkommen gebe ich die Exception über das Result Objekt zurück
if (result.Exception is not null)
{
    await Console.Error.WriteLineAsync($"Leider ist eine Exception aufgetreten: {result.Exception}");

    return 2;
}

// Sollte IsSuccess nicht true sein, dann konnte keine Statements abgerufen werden.
if (!result.IsSuccess || result.Results is null)
{
    await Console.Error.WriteLineAsync($"Statements konnten nicht abgerufen werden. HttpCode: '{result.HttpCode}', Status: '{result.Status}', ErrorCode: '{result.Error_code}', ErrorMessage: '{result.Error_message}'.");

    return 3;
}

for (int i = 0; i < result.Results.Count; i++)
{
    Statement current = result.Results[i];

    await Console.Out.WriteLineAsync($"{i}: '{current.Isin_title}' {current.Quantity} Stück");
}

#endregion PositionStatements

return 0;