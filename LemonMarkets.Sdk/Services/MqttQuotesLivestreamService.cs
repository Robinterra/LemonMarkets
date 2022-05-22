using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ApiService;
using LemonMarkets.Interfaces;
using LemonMarkets.Models;
using LemonMarkets.Models.Requests;
using LemonMarkets.Models.Responses;
using MQTTnet;
using MQTTnet.Client;
using static LemonMarkets.Interfaces.ILivestreamService;

namespace LemonMarkets.Services
{

    public class MqttQoutesLivestreamService : ILivestreamService
    {

        #region get/set

        public SubscribeDelegate? Subscribe;

        public DisconnectedDelegate? Disconnected;


        //static Regex regex = new Regex("\"t\":[0-9]*,", RegexOptions.Compiled);

        private MqttClient? mQTTClient;

        private MqttClientOptions? mqttOptions;

        private List<string> subscribeIsins;

        private IApiClient livestreamApi;

        private IApiClient tradingApi;

        private string userId;

        #endregion get/set

        #region ctor

        public MqttQoutesLivestreamService(IApiClient realtimeApi, IApiClient tradingApi)
        {
            this.subscribeIsins = new List<string>();

            this.userId = string.Empty;
            this.livestreamApi = realtimeApi;
            this.tradingApi = tradingApi;
        }

        #endregion ctor

        #region methods

        public async Task SetNewSubscriptionList(List<string> isins)
        {
            this.subscribeIsins = isins;

            if (this.mQTTClient is null) return;
            if (!this.mQTTClient.IsConnected) return;

            await this.mQTTClient.DisconnectAsync();
            await this.mQTTClient.ConnectAsync(this.mqttOptions);
        }

        public async Task AddSubscriptionOnIsin(string isin)
        {
            this.subscribeIsins.Add(isin);

            if (this.mQTTClient is null) return;
            if (!this.mQTTClient.IsConnected) return;

            await this.mQTTClient.DisconnectAsync();
            await this.mQTTClient.ConnectAsync(this.mqttOptions);
        }

        public Task Disconnect()
        {
            if (this.mQTTClient is null) return Task.CompletedTask;
            if (!this.mQTTClient.IsConnected) return Task.CompletedTask;

            return this.mQTTClient.DisconnectAsync();
        }

        public async Task ConnectAndSubscribeOnStream(SubscribeDelegate subscribtion, DisconnectedDelegate disconnected)
        {
            if (this.mQTTClient is not null && this.mQTTClient.IsConnected) return;

            this.Disconnected = disconnected;
            this.Subscribe = subscribtion;

            TokenResponse? response = await this.livestreamApi.PostAsync<TokenResponse>("auth");
            if (response is null) return;

            LemonResult<LemonMarketUser>? user = await this.tradingApi.GetAsync<LemonResult<LemonMarketUser>>("user");
            if (user is null) return;
            if (user.Results is null) return;
            if (string.IsNullOrEmpty(user.Results.User_id)) return;

            this.userId = user.Results.User_id;

            MqttFactory factory = new MqttFactory();
            this.mQTTClient = factory.CreateMqttClient();
            this.mQTTClient.ApplicationMessageReceivedAsync += this.SubscribeOnMqttClient;
            this.mQTTClient.ConnectedAsync += this.OnConnectionInit;
            this.mQTTClient.DisconnectedAsync += this.OnDisconnect;

            MqttClientOptionsBuilder builderOptions = new MqttClientOptionsBuilder();
            builderOptions.WithTcpServer("mqtt.ably.io");
            builderOptions.WithCredentials(response.Token);
            builderOptions.WithTls();
            builderOptions.WithCleanSession();

            this.mqttOptions = builderOptions.Build();

            await this.mQTTClient.ConnectAsync(this.mqttOptions);
        }

        private async Task OnDisconnect(MqttClientDisconnectedEventArgs arg)
        {
            if (this.Disconnected is not null) await this.Disconnected();
        }

        private async Task OnConnectionInit(MqttClientConnectedEventArgs arg)
        {
            if (this.mQTTClient is null) return;

            await this.mQTTClient.SubscribeAsync(userId);

            RequestSubscribeOnLivestream request = new RequestSubscribeOnLivestream(this.subscribeIsins);

            string requestSubscribeIsinsJson = JsonSerializer.Serialize(request);

            await this.mQTTClient.PublishStringAsync($"{userId}.subscriptions", requestSubscribeIsinsJson);
        }

        private async Task SubscribeOnMqttClient(MqttApplicationMessageReceivedEventArgs arg)
        {
            MemoryStream stream = new MemoryStream(arg.ApplicationMessage.Payload);

            Quote? quote = System.Text.Json.JsonSerializer.Deserialize<Quote>(stream, options: new () {PropertyNameCaseInsensitive = true });
            if (quote is null) return;
            quote.Bid /= 10000;
            quote.Ask /= 10000;

            if (this.Subscribe is not null) await this.Subscribe(quote);
        }

        #endregion methods

    }

}