using System;
using System.Collections.Generic;
using PaymentGateway.Model;

namespace PaymentGateway.Services
{
    public interface IPaymentService
    {
        bool AddPayment(IPayment pymt);
        Dictionary<string, IPayment> GetPayments();
        IPayment GetPayment(string uid);
        bool ExistsPayment(string uid);
    }
}
