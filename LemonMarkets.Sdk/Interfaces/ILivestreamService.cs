using System.Collections.Generic;
using System.Threading.Tasks;
using LemonMarkets.Models;

namespace LemonMarkets.Interfaces
{

    public interface ILivestreamService
    {

        Task Disconnect();

        Task AddSubscriptionOnIsin(string isin);

        Task SetNewSubscriptionList(List<string> isins);

        Task ConnectAndSubscribeOnStream(SubscribeDelegate subscribtion, DisconnectedDelegate disonected);

        public delegate Task SubscribeDelegate(Quote quote);

        public delegate Task DisconnectedDelegate();

    }
}