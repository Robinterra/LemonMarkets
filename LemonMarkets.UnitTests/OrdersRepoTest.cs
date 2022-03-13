using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ApiService;
using LemonMarkets.Models;
using LemonMarkets.Models.Responses;
using Xunit;
using LemonMarkets.Repos.V1;
using LemonMarkets.Interfaces;
using LemonMarkets.Models.Requests.Trading;

namespace LemonMarkets.UnitTests
{

    public class OrdersRepoTest
    {

        #region vars



        #endregion vars

        #region ctor

        #endregion ctor

        #region methods

        #region Create_ShouldReturnAOrder_WhenRequestCreateOrderIsCorrect

        [Fact]
        public async Task Create_ShouldReturnAOrder_WhenRequestCreateOrderIsCorrect()
        {
            // Arrange
            IApiClient apiClient = new FakeApiClient(LemonApi.apiRealTradingBaseUrl, "/v1", post: ApiClient_Create_ShouldReturnAOrder_WhenRequestCreateOrderIsCorrect );
            IOrdersRepo ordersRepo = new OrdersRepo(apiClient);

            DateTime expires = DateTime.Parse("2022-03-13");
            RequestCreateOrder request = new("DE123456", expires, Models.Enums.OrderSide.Buy, quantity: 10, venue: "XMUN", limit: 130.25m, stop: 129.125m, notes: "hallo welt");

            // Act
            LemonResult<Order> result = await ordersRepo.CreateAsync(request);

            // Assert
            Assert.NotNull ( result );
            Assert.True(result.IsSuccess);
            Assert.Equal(200, result.HttpCode);
            Assert.Null(result.Exception);
            Assert.NotNull(result.Results);

            Order order = result.Results;
            Assert.Equal(expires, order.Expires_at);
            Assert.Equal(Models.Enums.OrderSide.Buy, order.Side);
            Assert.Equal(1302500, order.Limit_price);
            Assert.Equal(1291250, order.Stop_price);
            Assert.Equal(10, order.Quantity);
            Assert.Equal("hallo welt", order.Notes);
            Assert.Equal("DE123456", order.Isin);
        }

        private Task<FakeApiResponse> ApiClient_Create_ShouldReturnAOrder_WhenRequestCreateOrderIsCorrect(FakeApiRequest request)
        {
            Assert.Equal("https://trading.lemon.markets/v1/orders", request.FullUrl);

            RequestCreateOrder requestCreateOrder = request.FromBody as RequestCreateOrder;
            Assert.NotNull(requestCreateOrder);

            Order order = new Order()
            {
                Id = Guid.NewGuid().ToString(),
                Expires_at = requestCreateOrder.Expires_at,
                Side = requestCreateOrder.Side,
                Limit_price = (int)(requestCreateOrder.Limit_price * 10000),
                Stop_price = (int)(requestCreateOrder.Stop_price * 10000),
                Venue = requestCreateOrder.Venue,
                Notes = requestCreateOrder.Notes,
                Isin = requestCreateOrder.Isin,
                Quantity = requestCreateOrder.Quantity,
            };

            FakeApiResponse response = new FakeApiResponse (HttpStatusCode.OK, new LemonResult<Order>() { Results = order, Status = "ok"});

            return Task.FromResult ( response );
        }

        #endregion Create_ShouldReturnAOrder_WhenRequestCreateOrderIsCorrect

        #endregion methods

    }

}