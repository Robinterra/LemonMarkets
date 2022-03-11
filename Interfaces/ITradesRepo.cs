using System.Threading.Tasks;
using LemonMarkets.Models;
using LemonMarkets.Models.Responses;

namespace lemon.LemonMarkets.Interfaces
{

    public interface ITradesRepo
    {

        Task<LemonResults<Trade>?> GetAsync ( TradesSearchFilter request );

    }

}