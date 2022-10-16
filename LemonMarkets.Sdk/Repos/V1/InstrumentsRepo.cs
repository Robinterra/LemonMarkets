using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LemonMarkets.Models;
using LemonMarkets.Models.Enums;
using LemonMarkets.Models.Responses;
using ApiService;
using LemonMarkets.Interfaces;

namespace LemonMarkets.Repos.V1
{

    public class InstrumentsRepo : IInstrumentsRepo
    {

        #region vars

        private readonly IApiClient marketApi;

        #endregion vars

        #region ctor

        public InstrumentsRepo(IApiClient marketApi)
        {
            this.marketApi = marketApi;
        }

        #endregion ctor

        #region methods

        public Task<LemonResults<Instrument>> GetAsync ( InstrumentSearchFilter? request = null )
        {
            if (request == null) return this.GetAsync("instruments")!;

            List<string> param = new List<string>();

            if (request.Mic != null) param.Add($"mic={request.Mic}");
            if (request.Isins.Count != 0) param.Add($"isin={string.Join(',', request.Isins)}");
            if (request.Search != null) param.Add($"search={request.Search}");
            if (request.Currency != null) param.Add($"currency={request.Currency}");
            if (request.IsTradable != null) param.Add($"tradable={request.IsTradable}");

            if (param.Count == 0) return this.GetAsync("instruments")!;

            StringBuilder buildParams = new ();
            buildParams.Append("?");
            buildParams.AppendJoin("&", param);

            return this.GetAsync("instruments", buildParams)!;
        }

        private async Task<LemonResults<Instrument>> GetAsync(params object[] header)
        {
            LemonResultsInternal<Instrument> result = (await this.marketApi.GetAsync<LemonResultsInternal<Instrument>> (header))!;

            return new LemonResults<Instrument>(result, new PageLoader<Instrument>(this.marketApi));
        }

        #endregion methods

    }

}