using System.Threading.Tasks;
using LemonMarkets.Models;
using LemonMarkets.Models.Responses;

namespace lemon.LemonMarkets.Interfaces
{

    public interface IQuotesRepo
    {

        Task<LemonResults<Quote>?> Get (QuoteSearchFilter request);

    }

}