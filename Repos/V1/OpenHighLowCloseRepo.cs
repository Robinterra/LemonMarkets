using System.Threading.Tasks;
using LemonMarkets.Interfaces;
using LemonMarkets.Models;
using LemonMarkets.Models.Responses;

namespace LemonMarkets.Repos.V1
{

    public class OpenHighLowCloseRepo : IOpenHighLowCloseRepo
    {


        public Task<LemonResults<OHLCEntry>?> GetAsync ( OHLCSearchFilter request )
        {
            throw new System.NotImplementedException ();
        }

    }

}