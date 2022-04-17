using System.Collections.Generic;
using System.Threading.Tasks;
using LemonMarkets.Models;
using LemonMarkets.Models.Filters;
using LemonMarkets.Models.Requests.Trading;
using LemonMarkets.Models.Responses;

namespace LemonMarkets.Interfaces
{

    public interface IPositionStatementsRepo
    {

        Task<LemonResults<Statement>> GetAsync(StatementSearchFilter? filter = null);

    }

}