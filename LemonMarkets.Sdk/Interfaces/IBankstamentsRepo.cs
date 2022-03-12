using System.Threading.Tasks;
using LemonMarkets.Models;
using LemonMarkets.Models.Requests.Trading;
using LemonMarkets.Models.Responses;

namespace lemon.LemonMarkets.Interfaces
{

    public interface IBankstatementsRepo
    {

        Task<LemonResults<BankStatement>?> GetAsync ( BankStatementsFilter? request = null );

    }

}