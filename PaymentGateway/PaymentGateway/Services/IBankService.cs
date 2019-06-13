using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentGateway.Model;

namespace PaymentGateway.Services
{
    interface IBankService
    {
        void MakePayment(IPayment payment, out string uid, out bool success);
    }
}
