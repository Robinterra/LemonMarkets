using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using lemon.LemonMarkets.Interfaces;
using LemonMarkets.Models;
using LemonMarkets.Models.Enums;
using LemonMarkets.Models.Responses;
using WsApiCore;

namespace LemonMarkets.Repos.V1
{

    public class InstrumentsRepo : IInstrumentsRepo
    {

        #region vars

        private readonly WsAPICore marketApi;

        #endregion vars

        #region ctor

        public InstrumentsRepo(WsAPICore marketApi)
        {
            this.marketApi = marketApi;
        }

        #endregion ctor

        #region methods

        public Task<LemonResults<Instrument>?> GetAsync ( InstrumentSearchFilter? request = null )
        {
            if (request == null) return this.marketApi.GetAsync<LemonResults<Instrument>> ("instruments");

            List<string> param = new List<string>();

            if (request.Mic != null) param.Add($"mic={request.Mic}");
            if (request.Isins.Count != 0) param.Add($"isin={string.Join(',', request.Isins)}");
            if (request.Search != null) param.Add($"search={request.Search}");
            if (request.Currency != Currency.None) param.Add($"currency={request.Currency}");
            if (request.IsTradable != null) param.Add($"tradable={request.IsTradable}");

            if (param.Count == 0) return this.marketApi.GetAsync<LemonResults<Instrument>> ("instruments");

            StringBuilder buildParams = new ();
            buildParams.Append("?");
            buildParams.AppendJoin("&", param);

            return this.marketApi.GetAsync<LemonResults<Instrument>> ("instruments", buildParams);
        }
        
        #endregion methods

    }

}