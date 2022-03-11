namespace LemonMarkets.Models.Enums
{

    /// <summary>
    /// Type of transaction, currently possible: pay_in, pay_out, order_buy, order_sell, dividend, tax
    /// </summary>
    public enum BankstatementType
    {

        None,
        Pay_in,
        Pay_out,
        Order_buy,
        Order_sell,
        Eod_Balance,
        Dividend,
        Tax

    }

}