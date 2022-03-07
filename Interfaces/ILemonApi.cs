using lemon.LemonMarkets.Interfaces;
using LemonMarkets.Models;

namespace LemonMarkets.Interfaces
{
    public interface ILemonApi
    {

        ConnectionInfo ConnectionInfo
        {
            get;
        }

        string ApiKey
        {
            get;
        }

        IOrdersRepo Orders
        {
            get;
        }

        IQuotesRepo Quotes
        {
            get;
        }

        IAccountRepo Account
        {
            get;
        }

        IPortfolioRepo Portfolio
        {
            get;
        }

        ITransactionsRepo Transactions
        {
            get;
        }

        IVenuesRepo Venues
        {
            get;
        }

        IInstrumentsRepo Instruments
        {
            get;
        }

        IOpenHighLowCloseRepo OHLC
        {
            get;
        }

    }
}
