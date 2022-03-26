using LemonMarkets;
using LemonMarkets.Interfaces;

using LemonMarkets.Models;
using LemonMarkets.Models.Enums;
using LemonMarkets.Models.Requests.Trading;
using LemonMarkets.Models.Responses;

// Du kannst dein api key auch direkt hier einfügen, dafür einfach zeile 10 auskommentieren und dann zeile 11 verwenden.
string? apiKey = Environment.GetEnvironmentVariable("apikey");
//string apiKey = "hier dein api key einfügen";

// Um eine Order mit echten Geld zu aktivieren musst du dein Pin angeben. Diesen Pin gibst du an wenn du dein Echtgeld Account anlegst.
string? activateOrderPin = Environment.GetEnvironmentVariable("activateOrderPin");

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
ILemonApi lemonApi = LemonApi.Build(apiKey, tradingMode);

#region CreateOrder

// Isin: Mit der Isin gibst du an welches Instrument du Kaufen/Verkaufen möchtest. (In diesen fall Infineon)
// Expires: Gibst du an wann deine Order ablaufen soll
// Side: Mit Side gibst du an ob du Verkaufen oder Kaufen möchtest
// Quantity: Mit Quantity gibst du die Anzahl an, also wie viel du kaufen/verkaufen möchtest
// venue: Mit der Venue gibst du an an welcher börse du handeln möchtest. (In diesen fall Munich Stock Exchange (XMUN))
RequestCreateOrder createOrder = new(isin: "DE0006231004", expires: DateTime.UtcNow.AddDays(1), side: OrderSide.Buy, quantity: 20, venue: "XMUN");

// Wenn du eine Limit oder eine stop order erstellen möchtest musst du dann einfach nur stop oder dein limit angeben
//RequestCreateOrder createOrder = new(isin: "DE0006231004", expires: DateTime.UtcNow.AddDays(1), side: OrderSide.Buy, quantity: 20, venue: "XMUN", limit: 31.25m);
//RequestCreateOrder createOrder = new(isin: "DE0006231004", expires: DateTime.UtcNow.AddDays(1), side: OrderSide.Buy, quantity: 20, venue: "XMUN", stop: 31.25m);

LemonResult<Order> createOrderResult = await lemonApi.Orders.CreateAsync(createOrder);

// Sollte bei vom HttpClient oder beim Deserialiseren eine exception hochkommen gebe ich die Exception über das Result Objekt zurück
if (createOrderResult.Exception is not null)
{
    await Console.Error.WriteLineAsync($"Leider ist eine Exception aufgetreten: {createOrderResult.Exception}");

    return 2;
}

// Sollte IsSuccess nicht true sein, dann konnte keine Order erstellt werden.
// Ich habe die null prüfungen hier mit reingenommen, da ich nullable aktiviert habe. So kann ich hier die null Warnungen umgehen.
if (!createOrderResult.IsSuccess || createOrderResult.Results is null || createOrderResult.Results.Id is null)
{
    await Console.Error.WriteLineAsync($"Order konnte nicht erstellt werden. HttpCode: '{createOrderResult.HttpCode}', Status: '{createOrderResult.Status}', ErrorCode: '{createOrderResult.Error_code}', ErrorMessage: '{createOrderResult.Error_message}'.");

    return 3;
}

Order order = createOrderResult.Results;

Console.WriteLine($"Order konnte erfolgreich unter der Id '{order.Id}' am '{order.Created_at}' angelegt");

#endregion CreateOrder

#region ActivateOrder

// Um eine Order zu aktivieren wird die Id benötigt und bei der RealMoney Umgebung zusätzlich ein Pin.
RequestActivateOrder requestActivateOrder = new(order.Id, activateOrderPin);

LemonResult activateResult = await lemonApi.Orders.ActivateAsync(requestActivateOrder);

// Sollte bei vom HttpClient oder beim Deserialiseren eine exception hochkommen gebe ich die Exception über das Result Objekt zurück
if (activateResult.Exception is not null)
{
    await Console.Error.WriteLineAsync($"Leider ist eine Exception aufgetreten: {activateResult.Exception}");

    return 4;
}

// Sollte IsSuccess nicht true sein, dann konnte keine Order aktiviert werden.
if (!activateResult.IsSuccess)
{
    await Console.Error.WriteLineAsync($"Order konnte nicht aktiviert werden. HttpCode: '{activateResult.HttpCode}', Status: '{activateResult.Status}', ErrorCode: '{activateResult.Error_code}', ErrorMessage: '{activateResult.Error_message}'.");

    return 5;
}

Console.WriteLine($"Die Order '{order.Id}' wurde aktiviert.");

#endregion ActivateOrder

return 0;
