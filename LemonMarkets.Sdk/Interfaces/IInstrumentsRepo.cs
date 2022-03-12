using System.Threading.Tasks;
using LemonMarkets.Models;
using LemonMarkets.Models.Responses;

namespace lemon.LemonMarkets.Interfaces
{

    public interface IInstrumentsRepo
    {

        Task<LemonResults<Instrument>> GetAsync ( InstrumentSearchFilter? request = null );

    }

}