using System;
using System.Threading.Tasks;
using LemonMarkets.Models;
using LemonMarkets.Models.Responses;

namespace LemonMarkets.Interfaces
{

    public interface ITradesRepo
    {

        [Obsolete("Please use GetLatestAsync, diese Methode wird noch bis zum 2022-04-01 funktionieren")]
        Task<LemonResults<Trade>> GetAsync ( TradesSearchFilter request );

        Task<LemonResults<Trade>> GetLatestAsync ( TradesLatestSearchFilter request );

    }

}