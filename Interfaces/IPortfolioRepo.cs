using System.Collections.Generic;
using System.Threading.Tasks;
using LemonMarkets.Models;
using LemonMarkets.Models.Requests.Trading;
using LemonMarkets.Models.Responses;

namespace lemon.LemonMarkets.Interfaces
{

    public interface IPortfolioRepo
    {

        Task<LemonResults<PortfolioEntry>?> GetAsync ( RequestGetPortfolio? request = null );

    }

}