using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using LemonMarkets.Helper;

namespace LemonMarkets.Models
{

    public class Venue
    {

        #region get/set

        /// <summary>
        /// Name of Trading Venue
        /// </summary>
        public string? Name
        {
            get;
            set;
        }

        /// <summary>
        /// Title of Trading Venue
        /// </summary>
        public string? Title
        {
            get;
            set;
        }

        /// <summary>
        /// Market Identifier Code of Trading Venue
        /// </summary>
        public string? Mic
        {
            get;
            set;
        }

        /// <summary>
        /// Indicator if Trading Venue is currently open
        /// </summary>
        public bool Is_open
        {
            get;
            set;
        }

        public OpeningHours? Opening_hours
        {
            get;
            set;
        }

        /// <summary>
        /// List of days the Trading Venue is open at
        /// </summary>
        public List<DateTime>? Opening_days
        {
            get;
            set;
        }

        #endregion get/set

    }

    public class OpeningHours
    {
        /// <summary>
        /// Start of Opening Hours for Trading Venue
        /// </summary>
        [JsonConverter(typeof(JsonTimeOnlyConverter))]
        public TimeOnly Start
        {
            get;
            set;
        }

        /// <summary>
        /// End of Opening Hours for Trading Venue
        /// </summary>
        [JsonConverter(typeof(JsonTimeOnlyConverter))]
        public TimeOnly End
        {
            get;
            set;
        }

        /// <summary>
        /// Timezone the Opening Hours are returned in
        /// </summary>
        public string? Timezone
        {
            get;
            set;
        }
    }

}