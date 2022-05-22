using System.Collections.Generic;

namespace LemonMarkets.Models.Requests
{

    public class RequestSubscribeOnLivestream
    {

        #region get/set

        public string Name
        {
            get;
        }

        public string Data
        {
            get;
        }

        #endregion get/set

        #region ctor

        public RequestSubscribeOnLivestream(List<string> isins)
        {
            string subscribeisins = $",{string.Join(',', isins)},";

            this.Name = "isins";
            this.Data = subscribeisins;
        }

        #endregion ctor

    }

}