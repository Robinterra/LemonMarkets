using System;
using System.Threading.Tasks;
using LemonMarkets.Models;
using LemonMarkets.Models.Responses;

namespace LemonMarkets.Interfaces
{

    public interface IQuotesRepo
    {

        Task<LemonResults<Quote>> GetLatestAsync ( QuoteLatestSearchFilter request );

    }

}