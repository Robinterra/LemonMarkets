using System;
using LemonMarkets.Models.Enums;

namespace LemonMarkets.Models
{

    public class BankStatementsFilter : Requests.Trading.RequestGetTransactions
    {

        public BankStatementsFilter (string? spaceId = null, DateTime? to = null, DateTime? from = null, string? isin = null, TransactionType type = TransactionType.None)
        : base(to, from, isin, type)
        {

        }

    }

}