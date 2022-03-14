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
using LemonMarkets.Models.Enums;

namespace LemonMarkets.UnitTests
{

    public class OrdersRepoTest
    {

        #region vars

        private List<Order> orders;

        #endregion vars

        #region ctor

        public OrdersRepoTest()
        {
            this.orders = new();
            this.orders.Add(new Order() { Id = "09ceeff6-c862-45c1-8b5f-49dabe2394b0", Isin = "DE123456", Notes = "feste guid", Side = Models.Enums.OrderSide.Buy, Quantity = 100, Venue = "XMUN", Limit_price = 120000, Status = OrderStatus.Executed, Created_at = DateTime.Parse("2022-03-11") });
            this.orders.Add(new Order() { Id = Guid.NewGuid().ToString(), Isin = "DE123456", Notes = "random guid 1", Side = Models.Enums.OrderSide.Buy, Quantity = 100, Venue = "XMUN", Limit_price = 122000, Status = OrderStatus.Open, Created_at = DateTime.Parse("2022-03-13") });
            this.orders.Add(new Order() { Id = Guid.NewGuid().ToString(), Isin = "DE123456", Notes = "random guid 3", Side = Models.Enums.OrderSide.Sell, Quantity = 100, Venue = "MUNCH", Limit_price = 122000, Status = OrderStatus.Executed, Created_at = DateTime.Parse("2022-02-10") });
            this.orders.Add(new Order() { Id = Guid.NewGuid().ToString(), Isin = "DE123457", Notes = "random guid 2", Side = Models.Enums.OrderSide.Sell, Quantity = 100, Venue = "MUNCH", Limit_price = 1000, Status = OrderStatus.Activated, Created_at = DateTime.Parse("2022-02-09") });

        }

        #endregion ctor

        #region methods

        #region Get_ShouldReturn1Order_WhenAskAllOpenOrders

        [Fact]
        public async Task Get_ShouldReturn1Order_WhenAskAllOpenOrders()
        {
            // Arrange
            IApiClient apiClient = new FakeApiClient(LemonApi.apiRealTradingBaseUrl, "/v1", get: ApiClient_GetWithFilter );
            IOrdersRepo ordersRepo = new OrdersRepo(apiClient);

            OrderSearchFilter searchFilter = new(orderStatus: OrderStatus.Open);

            // Act
            LemonResults<Order> result = await ordersRepo.GetAsync(searchFilter);

            // Assert
            Assert.NotNull ( result );
            Assert.True(result.IsSuccess);
            Assert.Equal(200, result.HttpCode);
            Assert.Null(result.Exception);
            Assert.NotNull(result.Results);

            Assert.Single(result.Results);

            Order order = result.Results.Find(t=>t.Isin == "DE123456");
            Assert.NotNull(order);
            Assert.Equal("random guid 1", order.Notes);
        }

        #endregion Get_ShouldReturn1Order_WhenAskAllOpenOrders

        #region Get_ShouldReturn1Order_WhenAskById

        [Fact]
        public async Task Get_ShouldReturn1Order_WhenAskById()
        {
            // Arrange
            IApiClient apiClient = new FakeApiClient(LemonApi.apiRealTradingBaseUrl, "/v1", get: ApiClient_GetWithFilter );
            IOrdersRepo ordersRepo = new OrdersRepo(apiClient);

            // Act
            LemonResult<Order> result = await ordersRepo.GetAsync("09ceeff6-c862-45c1-8b5f-49dabe2394b0");

            // Assert
            Assert.NotNull ( result );
            Assert.True(result.IsSuccess);
            Assert.Equal(200, result.HttpCode);
            Assert.Null(result.Exception);
            Assert.NotNull(result.Results);

            Assert.Equal("09ceeff6-c862-45c1-8b5f-49dabe2394b0", result.Results.Id);
        }

        #endregion Get_ShouldReturn1Order_WhenAskById

        #region Get_ShouldReturn2Orders_WhenAskFor2IsinAndOneSide

        [Fact]
        public async Task Get_ShouldReturn2Orders_WhenAskFor2IsinAndOneSide()
        {
            // Arrange
            IApiClient apiClient = new FakeApiClient(LemonApi.apiRealTradingBaseUrl, "/v1", get: ApiClient_GetWithFilter );
            IOrdersRepo ordersRepo = new OrdersRepo(apiClient);

            List<string> isins = new List<string> { "DE123456", "DE123457" };
            OrderSearchFilter searchFilter = new(isins, orderSide: OrderSide.Sell);

            // Act
            LemonResults<Order> result = await ordersRepo.GetAsync(searchFilter);

            // Assert
            Assert.NotNull ( result );
            Assert.True(result.IsSuccess);
            Assert.Equal(200, result.HttpCode);
            Assert.Null(result.Exception);
            Assert.NotNull(result.Results);

            Assert.Equal(2, result.Results.Count);

            Order order = result.Results.Find(t=>t.Isin == "DE123456");
            Assert.NotNull(order);
            Assert.Equal("random guid 3", order.Notes);

            order = result.Results.Find(t=>t.Isin == "DE123457");
            Assert.NotNull(order);
            Assert.Equal("random guid 2", order.Notes);
        }

        #endregion Get_ShouldReturn2Orders_WhenAskFor2IsinAndOneSide

        #region Get_ShouldReturn2Orders_WhenAskForOneIsinAndTimeRange

        [Fact]
        public async Task Get_ShouldReturn2Orders_WhenAskForOneIsinAndTimeRange()
        {
            // Arrange
            IApiClient apiClient = new FakeApiClient(LemonApi.apiRealTradingBaseUrl, "/v1", get: ApiClient_GetWithFilter );
            IOrdersRepo ordersRepo = new OrdersRepo(apiClient);

            List<string> isins = new List<string> { "DE123456" };
            DateTime from = DateTime.Parse("2022-03-10");
            DateTime to = DateTime.Parse("2022-03-14");
            OrderSearchFilter searchFilter = new(isins, from: from, to: to);

            // Act
            LemonResults<Order> result = await ordersRepo.GetAsync(searchFilter);

            // Assert
            Assert.NotNull ( result );
            Assert.True(result.IsSuccess);
            Assert.Equal(200, result.HttpCode);
            Assert.Null(result.Exception);
            Assert.NotNull(result.Results);

            Assert.Equal(2, result.Results.Count);

            Order order = result.Results.Find(t=>t.Isin == "DE123456");
            Assert.NotNull(order);
            Assert.Equal("09ceeff6-c862-45c1-8b5f-49dabe2394b0", order.Id);

            order = result.Results.LastOrDefault(t=>t.Isin == "DE123456");
            Assert.NotNull(order);
            Assert.Equal("random guid 1", order.Notes);
        }

        #endregion Get_ShouldReturn2Orders_WhenAskForOneIsinAndTimeRange

        private Task<FakeApiResponse> ApiClient_GetWithFilter(FakeApiRequest request)
        {
            Regex regex = new Regex ( "(status=(?<status>[a-z]+))|(side=(?<side>[a-z]+))|(isin=(?<isin>[A-Z0-9,]+))|(mic=(?<mic>[A-Z]+))|(to=(?<to>[0-9-T.:]+))|(from=(?<from>[0-9-T.:]+))" );

            Assert.NotNull ( request.Params );
            Assert.Equal("orders", request.Params[0]);

            string httpParmas = request.Params[1].ToString ();
            Assert.NotNull(httpParmas);
            if (httpParmas == "09ceeff6-c862-45c1-8b5f-49dabe2394b0") return Task.FromResult(new FakeApiResponse (HttpStatusCode.OK, new LemonResult<Order>() { Results = new () { Id = httpParmas }, Status = "ok"}));

            MatchCollection matchCollection = regex.Matches ( httpParmas );

            IEnumerable<string> isin = new string[0];
            string mic = null;
            DateTime to = DateTime.MaxValue;
            DateTime from = DateTime.MinValue;
            OrderSide side = OrderSide.All;
            OrderStatus status = OrderStatus.All;
            foreach ( Match match in matchCollection )
            {
                if ( match.Groups.ContainsKey ( "isin" ) && match.Groups["isin"].Success ) isin = match.Groups["isin"].Value.Split(",");
                if ( match.Groups.ContainsKey ( "mic" ) && match.Groups["mic"].Success ) mic = match.Groups["mic"].Value;
                if ( match.Groups.ContainsKey ( "to" ) && match.Groups["to"].Success ) to = DateTime.Parse(match.Groups["to"].Value);
                if ( match.Groups.ContainsKey ( "from" ) && match.Groups["from"].Success ) from = DateTime.Parse(match.Groups["from"].Value);
                if ( match.Groups.ContainsKey ( "side" ) && match.Groups["side"].Success ) side = Enum.Parse<OrderSide>(match.Groups["side"].Value, true);
                if ( match.Groups.ContainsKey ( "status" ) && match.Groups["status"].Success ) status = Enum.Parse<OrderStatus>(match.Groups["status"].Value, true);
            }

            List<Order> quotes = this.orders.Where ( t => (isin.Any() ? isin.Contains ( t.Isin ) : true) && t.Created_at <= to && t.Created_at >= from && (side == OrderSide.All ? true : side == t.Side)&& (status == OrderStatus.All ? true : status == t.Status) ).ToList();

            FakeApiResponse response = new FakeApiResponse (HttpStatusCode.OK, new LemonResults<Order>() { Results = quotes, Status = "ok"});

            return Task.FromResult(response);
        }

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