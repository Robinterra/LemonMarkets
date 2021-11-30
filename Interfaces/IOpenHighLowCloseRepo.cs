using System.Threading.Tasks;
using LemonMarkets.Models;
using LemonMarkets.Models.Responses;

namespace LemonMarkets.Interfaces
{

    /// <summary>
    /// OHLC Repo
    /// </summary>
    public interface IOpenHighLowCloseRepo
    {

        Task<LemonResults<OHLCEntry>?> GetAsync (OHLCSearchFilter request);

    }

}