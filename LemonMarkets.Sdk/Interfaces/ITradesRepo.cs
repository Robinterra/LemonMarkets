using System;
using System.Threading.Tasks;
using LemonMarkets.Models;
using LemonMarkets.Models.Responses;

namespace LemonMarkets.Interfaces
{

    public interface ITradesRepo
    {

        Task<LemonResults<Trade>> GetLatestAsync ( TradesLatestSearchFilter request );

    }

}