using LemonMarkets.Models.Enums;
using System.Collections.Generic;

namespace LemonMarkets.Models.Filters
{

    public class StatementSearchFilter
    {

        #region get/set

        /// <summary>
        /// Filter for a specific type of Statement. Choose one of the following: order_buy | order_sell | split | import | snx |
        /// </summary>
        public PositionStatementType Type
        {
            get;
        }

        #endregion get/set

        #region ctor

        public StatementSearchFilter ( PositionStatementType type = PositionStatementType.All)
        {
            this.Type = type;
        }

        #endregion ctor

    }

}