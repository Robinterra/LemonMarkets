using System;

namespace LemonMarkets.Models
{
    public class Account
    {

        #region get/set

        /// <summary>
        /// Timestamp for when you created your account
        /// </summary>
        public DateTime Created_at
        {
            get;
            set;
        }

        /// <summary>
        /// Internal Identification number for your account
        /// </summary>
        public string? Account_id
        {
            get;
            set;
        }

        /// <summary>
        /// Your first name
        /// </summary>
        public string? Firstname
        {
            get;
            set;
        }

        /// <summary>
        /// Your last name
        /// </summary>
        public string? Lastname
        {
            get;
            set;
        }

        /// <summary>
        /// Your email address
        /// </summary>
        public string? Email
        {
            get;
            set;
        }

        /// <summary>
        /// Your phone number
        /// </summary>
        public string? Phone
        {
            get;
            set;
        }

        /// <summary>
        /// Your address
        /// </summary>
        public string? Address
        {
            get;
            set;
        }

        /// <summary>
        /// The billing address provided for the account
        /// </summary>
        public string? Billing_address
        {
            get;
            set;
        }

        /// <summary>
        /// The billing email address provided for the account
        /// </summary>
        public string? Billing_email
        {
            get;
            set;
        }

        /// <summary>
        /// The billing name provided for the account
        /// </summary>
        public string? Billing_name
        {
            get;
            set;
        }

        /// <summary>
        /// The billing VAT number provided for the account
        /// </summary>
        public string? Billing_vat
        {
            get;
            set;
        }

        /// <summary>
        /// Identification Number of your securities account
        /// </summary>
        public string? Deposit_id
        {
            get;
            set;
        }

        /// <summary>
        /// The internal client identification number related to the account
        /// </summary>
        public string? Client_id
        {
            get;
            set;
        }

        /// <summary>
        /// The account reference number
        /// </summary>
        public string? Account_number
        {
            get;
            set;
        }

        /// <summary>
        /// IBAN of the brokerage account at our partner bank. This is the IBAN you can transfer money from your referrence account to.
        /// </summary>
        public string? Iban_brokerage
        {
            get;
            set;
        }

        /// <summary>
        /// IBAN of the reference account. You define your reference account as part of the onboarding. You can use your reference account to transfer money to your brokerage acount. You can also withdraw money from your brokerage account to your reference account.
        /// </summary>
        public string? Iban_origin
        {
            get;
            set;
        }

        /// <summary>
        /// Bank name your reference account is located at
        /// </summary>
        public string? Bank_name_origin
        {
            get;
            set;
        }

        /// <summary>
        /// Your balance is the money you transferred to your account + the combined profits or losses from your orders.
        /// </summary>
        public int Balance
        {
            get;
            set;
        }

        /// <summary>
        /// How much cash you have left to invest. Your balance minus the sum of orders that were activated but not executed, yet.
        /// </summary>
        public int Cash_to_invest
        {
            get;
            set;
        }

        /// <summary>
        /// How much cash you have in your account to withdraw to your reference account. Calculated through your last reported balance minus the current sum of buy orders. Please note that orders from the past 2 days cannot be considered for your cash to withdraw.
        /// </summary>
        public int Cash_to_withdraw
        {
            get;
            set;
        }

        /// <summary>
        /// We offer (or will offer) different subscription plans for trading with lemon.markets. This endpoint tells you which plan you are currently on - free, basic or pro.
        /// </summary>
        public string? Trading_plan
        {
            get;
            set;
        }

        /// <summary>
        /// We offer (or will offer) different subscription plans for retrieving market data with lemon.markets. This endpoint tells you which plan you are currently on - free, basic or pro.
        /// </summary>
        public string? Data_plan
        {
            get;
            set;
        }

        /// <summary>
        /// Your tax tax allowance - between 0 and 801 €, as specified in your onboarding process
        /// </summary>
        public int? Tax_allowance
        {
            get;
            set;
        }

        /// <summary>
        /// Relevant start date for your tax allowance (usually 01/01/ of respective year)
        /// </summary>
        public DateTime? Tax_allowance_start
        {
            get;
            set;
        }

        /// <summary>
        /// Relevant end date for your tax allowance (usually 31/12/ of respective year)
        /// </summary>
        public DateTime? Tax_allowance_end
        {
            get;
            set;
        }

        #endregion get/set

    }
}