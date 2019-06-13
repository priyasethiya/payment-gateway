using System;
using System.Collections.Generic;
using PaymentGateway.Model;
using Microsoft.Extensions.Logging.Debug;
using Microsoft.Extensions.Logging;

namespace PaymentGateway.Services
{
    // Payment Service which processes / forwards / stores the payment details in memory, and retrieves the payments processed.
    public class PaymentService : IPaymentService
    {
        #region Fields         
        private Dictionary<string, IPayment> Payments = new Dictionary<string, IPayment>();
        private IBankService _bankService;
        private ILogger<DebugLoggerProvider> _loggerDebug;
        #endregion

        #region Constructor
        public PaymentService(ILogger<DebugLoggerProvider> loggerDebug)
        {
            Payments = new Dictionary<string, IPayment>();
            _bankService = new BankService();
            _loggerDebug = loggerDebug;
        }
         
        #endregion

        #region Methods
        public bool AddPayment(IPayment pymt)
        {
            _loggerDebug.Log(LogLevel.Information, "Add Payment Called");
            if (Payments.ContainsKey(pymt.Uid))
                return false;

            bool success;
            string uid;
            _bankService.MakePayment(pymt, out uid, out success);
            pymt.Success = success;
            pymt.PaymentDate = DateTime.UtcNow;

            // Encrypting the card information using salt and hash technique . The same salt and hash can be used by the merchant to compare and reconciliate his records
            string hashedCardNumber = Helpers.HashingHelper.ComputeHash(pymt.CardNumber);
            string hashedCardCvv = Helpers.HashingHelper.ComputeHash(pymt.CardCvv);

            // While storing in database / memory , we can use these hashed values instead of raw sensitive data
            pymt.CardNumber = hashedCardNumber;
            pymt.CardCvv = hashedCardCvv;

            Payments.Add(pymt.Uid, pymt);
            return success;
        }

        public Dictionary<string, IPayment> GetPayments()
        {
            return Payments;
        }
        public IPayment GetPayment(string uid)
        {
            if (Payments.ContainsKey(uid))
                return Payments[uid];
            return null;
        }
        public bool ExistsPayment( string uid)
        {
            if (Payments.ContainsKey(uid))
                return true;
            return false;
        }
        #endregion
    }
}
