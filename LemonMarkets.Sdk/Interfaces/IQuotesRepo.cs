using System;
using System.Threading.Tasks;
using LemonMarkets.Models;
using LemonMarkets.Models.Responses;

namespace LemonMarkets.Interfaces
{

    public interface IQuotesRepo
    {

        [Obsolete("Please use GetLatestAsync, diese Methode wird noch bis zum 2022-04-01 funktionieren")]
        Task<LemonResults<Quote>> GetAsync (QuoteSearchFilter request);

        Task<LemonResults<Quote>> GetLatestAsync ( QuoteLatestSearchFilter request );

    }

}