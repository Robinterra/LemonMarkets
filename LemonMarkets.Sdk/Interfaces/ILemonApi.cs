using System;
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

        IPositionsRepo Positions
        {
            get;
        }

        IPositionStatementsRepo PositionStatements
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

        IBankstatementsRepo Bankstatements
        {
            get;
        }

        ITradesRepo Trades
        {
            get;
        }

    }
}
