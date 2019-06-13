using System;
using System.Collections.Generic;
using System.Text;

namespace BankService
{
    public interface IPaymentManager
    {
        bool MakePayment();
    }
}
