using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentGateway.Model;

namespace PaymentGateway.Services
{
    //This is the mock bank service which can later be replaced with the real bank service component
    public class BankService : IBankService
    {
        #region Methods
        public void MakePayment(IPayment payment, out string uid, out bool success )
        {
            uid = payment.Uid;
            success = true;
        }
        #endregion
    }
}
