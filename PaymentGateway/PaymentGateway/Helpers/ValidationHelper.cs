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
        public static bool IsValidRequest(IPayment pymt)
        {
            // Checks if the data is complete and card numbers are specified length, and the card has not expired
            if (String.IsNullOrEmpty(pymt.Uid))
            {
                loggerDebug.Log(LogLevel.Error, "UID Not Provided");
                return false;
            }

            if (String.IsNullOrEmpty(pymt.CardNumber))
            {
                loggerDebug.Log(LogLevel.Error, "Card Number Not Provided");
                return false;
            }

            if (String.IsNullOrEmpty(pymt.CardCvv))
            {
                loggerDebug.Log(LogLevel.Error, "Card Cvv Not Provided");
                return false;
            }

            if(String.IsNullOrEmpty(pymt.Currency))
            {
                loggerDebug.Log(LogLevel.Error, "Currency Not Provided");
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
                loggerDebug.Log(LogLevel.Error, "Card expiry format not correct");
                return false;
            }

            if ((year < DateTime.Now.Year)||(year == DateTime.Now.Year && month < DateTime.Now.Month))
            {
                loggerDebug.Log(LogLevel.Error, "Card has expired");
                return false;
            }
                
            if (pymt.Amount <= 0)
            {
                loggerDebug.Log(LogLevel.Error, "Payment Amount should be greater than 0");
                return false;
            }

            if (pymt.CardNumber.Length != 16)
            {
                loggerDebug.Log(LogLevel.Error, "Card number should be 16 digits");
                return false;
            }

            if(pymt.CardCvv.Length != 3)
            {
                loggerDebug.Log(LogLevel.Error, "Card Cvv should be 3 digits");
                return false;
            }

            pymt.Currency.Trim();
            if (pymt.Currency.Length != 3)
            {
                loggerDebug.Log(LogLevel.Error, "Payment Currency should be 3 characters");
                return false;
            }
                
            return true;
        }
        #endregion
    }
}
