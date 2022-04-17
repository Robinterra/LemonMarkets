using System.Collections.Generic;
using System.Threading.Tasks;
using LemonMarkets.Models;
using LemonMarkets.Models.Filters;
using LemonMarkets.Models.Requests.Trading;
using LemonMarkets.Models.Responses;

namespace lemon.LemonMarkets.Interfaces
{

    public interface IPositionsRepo
    {

        Task<LemonResults<PositionEntry>> GetAsync ( PositionSearchFilter? filter = null );

    }

}