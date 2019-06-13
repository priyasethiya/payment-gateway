using System;
using PaymentGateway.Model;
using Microsoft.Extensions.Logging.Debug;
using Microsoft.Extensions.Logging;

namespace PaymentGateway.Helpers
{
    public static class ValidationHelper
    {
        #region Properties
        public static ILogger<DebugLoggerProvider> loggerDebug;
        #endregion

        #region Methods

        public static IPayment Parse(IPayment pymt)
        {
            pymt.CardExpiry.Trim();
            //getting year of expiry
            int year;
            Int32.TryParse(pymt.CardExpiry.Substring(0,4), out year);
            //getting month of expiry
            int month;
            Int32.TryParse(pymt.CardExpiry.Substring(5), out month);
            return pymt;
        }
        public static bool IsValidRequest(IPayment pymt, out string msg)
        {
            // Checks if the data is complete and card numbers are specified length, and the card has not expired
            if (String.IsNullOrEmpty(pymt.Uid))
            {
                msg = "UID Not Provided";
                loggerDebug.Log(LogLevel.Error, msg);
                return false;
            }

            if (String.IsNullOrEmpty(pymt.CardNumber))
            {
                msg = "Card Number Not Provided";
                loggerDebug.Log(LogLevel.Error, msg);
                return false;
            }

            if (String.IsNullOrEmpty(pymt.CardCvv))
            {
                msg = "Card Cvv Not Provided";
                loggerDebug.Log(LogLevel.Error, msg);
                return false;
            }

            if(String.IsNullOrEmpty(pymt.Currency))
            {
                msg = "Currency Not Provided";
                loggerDebug.Log(LogLevel.Error, msg);
                return false;
            }

            //getting year of expiry
            int year;
            Int32.TryParse(pymt.CardExpiry.Substring(0, 4), out year);
            //getting month of expiry
            int month;
            Int32.TryParse(pymt.CardExpiry.Substring(5), out month);
            if (month == 0 || year == 0)
            {
                msg = "Card expiry details not valid";
                loggerDebug.Log(LogLevel.Error, msg);
                return false;
            }

            if ((year < DateTime.Now.Year)||(year == DateTime.Now.Year && month < DateTime.Now.Month))
            {
                msg = "Card has expired";
                loggerDebug.Log(LogLevel.Error, msg);
                return false;
            }
                
            if (pymt.Amount <= 0)
            {
                msg = "Payment Amount should be greater than 0";
                loggerDebug.Log(LogLevel.Error, msg);
                return false;
            }

            if (pymt.CardNumber.Length != 16)
            {
                msg = "Card number should be 16 digits";
                loggerDebug.Log(LogLevel.Error, msg);
                return false;
            }

            if(pymt.CardCvv.Length != 3)
            {
                msg = "Card Cvv should be 3 digits";
                loggerDebug.Log(LogLevel.Error, msg);
                return false;
            }

            pymt.Currency.Trim();
            if (pymt.Currency.Length != 3)
            {
                msg = "Payment Currency should be 3 characters";
                loggerDebug.Log(LogLevel.Error, msg);
                return false;
            }
            msg = "Valid Card Details";
            return true;
        }
        #endregion
    }
}
