using System;
using LemonMarkets.Models;

namespace LemonMarkets.Interfaces
{
    public interface ILemonApi
    {

        ConnectionInfo ConnectionInfo
        {
            get;
        }

        string MarketKey
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

        ILivestreamService Livestream
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
