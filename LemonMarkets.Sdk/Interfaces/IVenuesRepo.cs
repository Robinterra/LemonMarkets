using System.Threading.Tasks;
using LemonMarkets.Models;
using LemonMarkets.Models.Responses;

namespace LemonMarkets.Interfaces
{

    public interface IVenuesRepo
    {

        Task<LemonResults<Venue>?> GetAsync ( VenueSearchFilter? request = null );

    }

}