using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LemonMarkets.Interfaces;
using LemonMarkets.Models;
using LemonMarkets.Models.Responses;
using ApiService;

namespace LemonMarkets.Repos.V1
{

    public class VenuesRepo : IVenuesRepo
    {

        #region vars

        private readonly IApiClient marketApi;

        #endregion vars

        #region ctor

        public VenuesRepo(IApiClient marketApi)
        {
            this.marketApi = marketApi;
        }

        #endregion ctor

        #region methods

        public Task<LemonResults<Venue>> GetAsync ( VenueSearchFilter? request = null )
        {
            if (request == null) return this.GetAsync("venues");

            List<string> param = new List<string>();

            if (request.Mic != null) param.Add($"mic={request.Mic}");

            if (param.Count == 0) return this.GetAsync("venues");

            StringBuilder buildParams = new ();
            buildParams.Append("?");
            buildParams.AppendJoin("&", param);

            return this.GetAsync("venues", buildParams);
        }

        private async Task<LemonResults<Venue>> GetAsync(params object[] header)
        {
            LemonResultsInternal<Venue> result = (await this.marketApi.GetAsync<LemonResultsInternal<Venue>> (header))!;

            return new LemonResults<Venue>(result, new PageLoader<Venue>(this.marketApi));
        }

        #endregion methods

    }

}