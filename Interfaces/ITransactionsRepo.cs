using System.Threading.Tasks;
using LemonMarkets.Models;
using LemonMarkets.Models.Requests.Trading;
using LemonMarkets.Models.Responses;

namespace lemon.LemonMarkets.Interfaces
{

    public interface ITransactionsRepo
    {

        Task<LemonResults<string, PortfolioEntry>?> GetAsync ( RequestGetPortfolio? request = null );

    }

}