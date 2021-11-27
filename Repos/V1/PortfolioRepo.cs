using System.Threading.Tasks;
using lemon.LemonMarkets.Interfaces;
using LemonMarkets.Models;
using LemonMarkets.Models.Requests.Trading;
using LemonMarkets.Models.Responses;

namespace LemonMarkets.Repos.V1
{

    public class PortfolioRepo : IPortfolioRepo
    {


        public Task<LemonResults<string, PortfolioEntry>> GetAsync ( RequestGetPortfolio? request = null )
        {
            throw new System.NotImplementedException ();
        }

    }

}