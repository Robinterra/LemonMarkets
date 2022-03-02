using lemon.LemonMarkets.Interfaces;
using LemonMarkets.Interfaces;
using LemonMarkets.Models;
using LemonMarkets.Models.Enums;
using LemonMarkets.Models.Requests.Trading;
using LemonMarkets.Models.Responses;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApiService;

namespace LemonMarkets.Repos.V1
{

    public class OrdersRepo : IOrdersRepo
    {

        #region vars

        private readonly IApiClient tradingApi;

        #endregion vars

        #region ctor

        public OrdersRepo(IApiClient tradingApi)
        {
            this.tradingApi = tradingApi;
        }

        #endregion ctor

        #region methods

        public Task<LemonResult?> ActivateAsync(RequestActivateOrder request)
        {
            string route = $"orders/{request.OrderId}/activate";
            if ( request.Pin == null ) return this.tradingApi.PostAsync<LemonResult> ( route );

            return this.tradingApi.PostAsync<LemonResult, RequestActivateOrder>(request, route);
        }

        public Task<LemonResults<Order>?> GetAsync(OrderSearchFilter? request = null)
        {
            if (request == null) return this.tradingApi.GetAsync<LemonResults<Order>>("orders");

            List<string> param = new List<string>();

            if (request.From != null) param.Add($"from={request.From}");
            if (request.To != null) param.Add($"to={request.To}");
            if (request.Isins.Count != 0) param.Add($"isin={string.Join(',', request.Isins)}");
            if (request.SpaceUuid != null) param.Add($"space_id={request.SpaceUuid}");
            if (request.Side != OrderSide.All) param.Add($"side={request.Side.ToString().ToLower()}");
            if (request.Type != OrderType.All) param.Add($"type={request.Type.ToString().ToLower()}");
            if (request.Status != OrderStatus.All) param.Add($"status={request.Status.ToString().ToLower()}");

            if (param.Count == 0) return this.tradingApi.GetAsync<LemonResults<Order>>("orders");

            StringBuilder buildParams = new StringBuilder();
            buildParams.Append("?");
            buildParams.AppendJoin("&", param);

            return this.tradingApi.GetAsync<LemonResults<Order>>("orders", buildParams);
        }

        public Task<LemonResult<Order>?> GetAsync(string id)
        {
            return this.tradingApi.GetAsync<LemonResult<Order>>("orders", id);
        }

        public Task<LemonResult<Order>?> CreateAsync(RequestCreateOrder request)
        {
            return this.tradingApi.PostAsync<LemonResult<Order>, RequestCreateOrder>(request, "orders");
        }

        public Task<LemonResult?> DeleteAsync(string id)
        {
            return this.tradingApi.DeleteAsync<LemonResult>("orders", id);
        }

        #endregion methods
    }

}
